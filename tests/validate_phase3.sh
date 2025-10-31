#!/bin/bash

echo "=========================================="
echo "Phase 3: Location System Complete Test"
echo "=========================================="
echo ""

# Colors
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m'

PASSED=0
FAILED=0

test_passed() {
    echo -e "${GREEN}✓ PASSED:${NC} $1"
    ((PASSED++))
}

test_failed() {
    echo -e "${RED}✗ FAILED:${NC} $1"
    ((FAILED++))
}

# Create test universe
echo "Setting up test data..."
UNIVERSE_RESPONSE=$(curl -s -X POST http://localhost:5022/api/Universe \
  -H "Content-Type: application/json" \
  -d '{"name":"Phase 3 Test Universe","description":"Testing hierarchical locations"}')
UNIVERSE_ID=$(echo $UNIVERSE_RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")
echo "Test universe created: $UNIVERSE_ID"
echo ""

echo "=== Test 1: Create root location ==="
PLANET_RESPONSE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"name\":\"Arda\",\"type\":\"Planet\",\"description\":\"The world\"}")
PLANET_ID=$(echo $PLANET_RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")

if [ -n "$PLANET_ID" ]; then
    test_passed "Created root location (Planet)"
else
    test_failed "Failed to create root location"
fi
echo ""

echo "=== Test 2: Create child location ==="
CONTINENT_RESPONSE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"parentLocationId\":\"$PLANET_ID\",\"name\":\"Middle-earth\",\"type\":\"Continent\"}")
CONTINENT_ID=$(echo $CONTINENT_RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")

if [ -n "$CONTINENT_ID" ]; then
    test_passed "Created child location (Continent)"
else
    test_failed "Failed to create child location"
fi
echo ""

echo "=== Test 3: Create grandchild location ==="
REGION_RESPONSE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"parentLocationId\":\"$CONTINENT_ID\",\"name\":\"The Shire\",\"type\":\"Region\",\"population\":10000,\"climate\":\"Temperate\"}")
REGION_ID=$(echo $REGION_RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")

if [ -n "$REGION_ID" ]; then
    test_passed "Created grandchild location (Region)"
else
    test_failed "Failed to create grandchild location"
fi
echo ""

echo "=== Test 4: Get children ==="
CHILDREN_COUNT=$(curl -s "http://localhost:5022/api/Location/$CONTINENT_ID/children" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$CHILDREN_COUNT" == "1" ]; then
    test_passed "Get children returns correct count"
else
    test_failed "Get children (expected 1, got $CHILDREN_COUNT)"
fi
echo ""

echo "=== Test 5: Get ancestors ==="
ANCESTORS_COUNT=$(curl -s "http://localhost:5022/api/Location/$REGION_ID/ancestors" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$ANCESTORS_COUNT" == "2" ]; then
    test_passed "Get ancestors returns correct count"
else
    test_failed "Get ancestors (expected 2, got $ANCESTORS_COUNT)"
fi
echo ""

echo "=== Test 6: Get path (breadcrumb) ==="
PATH_COUNT=$(curl -s "http://localhost:5022/api/Location/$REGION_ID/path" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$PATH_COUNT" == "3" ]; then
    test_passed "Get path returns full hierarchy"
else
    test_failed "Get path (expected 3, got $PATH_COUNT)"
fi
echo ""

echo "=== Test 7: Get root locations ==="
ROOT_COUNT=$(curl -s "http://localhost:5022/api/Location/universe/$UNIVERSE_ID/roots" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$ROOT_COUNT" == "1" ]; then
    test_passed "Get root locations"
else
    test_failed "Get root locations (expected 1, got $ROOT_COUNT)"
fi
echo ""

echo "=== Test 8: Create sibling ==="
SIBLING_RESPONSE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"parentLocationId\":\"$CONTINENT_ID\",\"name\":\"Mordor\",\"type\":\"Region\"}")
SIBLING_ID=$(echo $SIBLING_RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")

if [ -n "$SIBLING_ID" ]; then
    test_passed "Created sibling location"
else
    test_failed "Failed to create sibling"
fi
echo ""

echo "=== Test 9: Get siblings ==="
SIBLINGS_COUNT=$(curl -s "http://localhost:5022/api/Location/$REGION_ID/siblings" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$SIBLINGS_COUNT" == "1" ]; then
    test_passed "Get siblings returns correct count"
else
    test_failed "Get siblings (expected 1, got $SIBLINGS_COUNT)"
fi
echo ""

echo "=== Test 10: Update location ==="
HTTP_CODE=$(curl -s -X PUT "http://localhost:5022/api/Location/$REGION_ID" \
  -H "Content-Type: application/json" \
  -d "{\"name\":\"The Shire\",\"type\":\"Region\",\"description\":\"Peaceful hobbit homeland\",\"population\":12000,\"climate\":\"Temperate\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "204" ]; then
    test_passed "Update location"
else
    test_failed "Update location (HTTP $HTTP_CODE)"
fi
echo ""

echo "=== Test 11: Move location (reparent) ==="
HTTP_CODE=$(curl -s -X PUT "http://localhost:5022/api/Location/$SIBLING_ID/move" \
  -H "Content-Type: application/json" \
  -d "{\"newParentId\":\"$PLANET_ID\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "204" ]; then
    test_passed "Move location"
else
    test_failed "Move location (HTTP $HTTP_CODE)"
fi
echo ""

echo "=== Test 12: Verify move ==="
PLANET_CHILDREN=$(curl -s "http://localhost:5022/api/Location/$PLANET_ID/children" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$PLANET_CHILDREN" == "2" ]; then
    test_passed "Move verified - parent has correct children"
else
    test_failed "Move verification (expected 2 children, got $PLANET_CHILDREN)"
fi
echo ""

echo "=== Test 13: Test circular reference prevention ==="
HTTP_CODE=$(curl -s -X PUT "http://localhost:5022/api/Location/$PLANET_ID/move" \
  -H "Content-Type: application/json" \
  -d "{\"newParentId\":\"$REGION_ID\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "400" ]; then
    test_passed "Circular reference prevented"
else
    test_failed "Circular reference (expected 400, got $HTTP_CODE)"
fi
echo ""

echo "=== Test 14: Test delete with children (should fail) ==="
HTTP_CODE=$(curl -s -X DELETE "http://localhost:5022/api/Location/$CONTINENT_ID" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "400" ]; then
    test_passed "Delete with children prevented"
else
    test_failed "Delete with children (expected 400, got $HTTP_CODE)"
fi
echo ""

echo "=== Test 15: Test descendants ==="
DESCENDANTS=$(curl -s "http://localhost:5022/api/Location/$PLANET_ID/descendants" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$DESCENDANTS" -ge "2" ]; then
    test_passed "Get descendants"
else
    test_failed "Get descendants (expected >= 2, got $DESCENDANTS)"
fi
echo ""

echo "=== Test 16: Delete leaf location ==="
HTTP_CODE=$(curl -s -X DELETE "http://localhost:5022/api/Location/$REGION_ID" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "204" ]; then
    test_passed "Delete leaf location"
else
    test_failed "Delete leaf location (HTTP $HTTP_CODE)"
fi
echo ""

echo "=== Test 17: Validation - empty name ==="
HTTP_CODE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"name\":\"\",\"type\":\"City\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "400" ]; then
    test_passed "Validation - empty name rejected"
else
    test_failed "Validation empty name (expected 400, got $HTTP_CODE)"
fi
echo ""

echo "=== Test 18: Validation - empty type ==="
HTTP_CODE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"$UNIVERSE_ID\",\"name\":\"Test\",\"type\":\"\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "400" ]; then
    test_passed "Validation - empty type rejected"
else
    test_failed "Validation empty type (expected 400, got $HTTP_CODE)"
fi
echo ""

echo "=== Test 19: Validation - non-existent universe ==="
HTTP_CODE=$(curl -s -X POST http://localhost:5022/api/Location \
  -H "Content-Type: application/json" \
  -d "{\"universeId\":\"00000000-0000-0000-0000-000000000000\",\"name\":\"Test\",\"type\":\"City\"}" \
  -w "%{http_code}" -o /dev/null)
if [ "$HTTP_CODE" == "400" ] || [ "$HTTP_CODE" == "404" ]; then
    test_passed "Validation - non-existent universe rejected"
else
    test_failed "Validation non-existent universe (expected 400/404, got $HTTP_CODE)"
fi
echo ""

echo "=== Test 20: Get all locations in universe ==="
ALL_LOCATIONS=$(curl -s "http://localhost:5022/api/Location/universe/$UNIVERSE_ID" | python3 -c "import json, sys; print(len(json.load(sys.stdin)))")
if [ "$ALL_LOCATIONS" -ge "2" ]; then
    test_passed "Get all locations in universe"
else
    test_failed "Get all locations (expected >= 2, got $ALL_LOCATIONS)"
fi
echo ""

# Cleanup
echo "Cleaning up..."
curl -s -X DELETE "http://localhost:5022/api/Universe/$UNIVERSE_ID" > /dev/null
echo "Test universe deleted"
echo ""

echo "=========================================="
echo "Test Results Summary"
echo "=========================================="
echo ""
echo "Passed: $PASSED"
echo "Failed: $FAILED"
echo "Total:  $((PASSED + FAILED))"
echo ""

if [ $FAILED -eq 0 ]; then
    echo -e "${GREEN}✅ ALL PHASE 3 TESTS PASSED!${NC}"
    echo ""
    echo "Phase 3: Location System is COMPLETE"
    exit 0
else
    echo -e "${RED}❌ SOME TESTS FAILED${NC}"
    exit 1
fi
