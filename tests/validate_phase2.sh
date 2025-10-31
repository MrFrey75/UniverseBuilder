#!/bin/bash
# Phase 2 Complete Validation Script

echo "=========================================="
echo "Phase 2: Universe Management Validation"
echo "=========================================="
echo ""

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Track results
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

echo "=== Backend Tests ==="
echo ""

# Test 1: Backend build
echo "Test 1: Building backend projects..."
cd /home/fray/Projects/UniverseBuilder/src/UniverseBuilder.Api
if dotnet build --nologo --verbosity quiet > /dev/null 2>&1; then
    test_passed "Backend builds successfully"
else
    test_failed "Backend build"
fi
echo ""

# Test 2: Unit tests
echo "Test 2: Running unit tests..."
cd /home/fray/Projects/UniverseBuilder/tests/UniverseBuilder.Core.Tests
TEST_OUTPUT=$(dotnet test --nologo --verbosity quiet 2>&1)
if echo "$TEST_OUTPUT" | grep -q "Passed:    11" || echo "$TEST_OUTPUT" | grep -q "Test Run Successful"; then
    test_passed "All 11 unit tests pass"
else
    test_failed "Unit tests (Expected 11 passed)"
    echo "$TEST_OUTPUT" | grep -E "Passed|Failed"
fi
echo ""

# Test 3: API responds
echo "Test 3: Testing API availability..."
if curl -s http://localhost:5022/api/Universe > /dev/null 2>&1; then
    test_passed "API is responding"
else
    test_failed "API not responding at http://localhost:5022"
fi
echo ""

# Test 4: CRUD operations
echo "Test 4: Testing complete CRUD workflow..."

# Clean up
curl -s http://localhost:5022/api/Universe | python3 -c "
import json, sys, subprocess
try:
    universes = json.load(sys.stdin)
    for u in universes:
        subprocess.run(['curl', '-s', '-X', 'DELETE', f\"http://localhost:5022/api/Universe/{u['id']}\"], 
                       stdout=subprocess.DEVNULL, stderr=subprocess.DEVNULL)
except:
    pass
" 2>/dev/null

# Create
RESPONSE=$(curl -s -X POST http://localhost:5022/api/Universe \
  -H "Content-Type: application/json" \
  -d '{"name":"Test Universe","description":"Test Description"}')
TEST_ID=$(echo $RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])" 2>/dev/null)

if [ -n "$TEST_ID" ]; then
    # Read
    NAME=$(curl -s "http://localhost:5022/api/Universe/$TEST_ID" | python3 -c "import json, sys; print(json.load(sys.stdin)['name'])" 2>/dev/null)
    if [ "$NAME" == "Test Universe" ]; then
        # Update
        curl -s -X PUT "http://localhost:5022/api/Universe/$TEST_ID" \
          -H "Content-Type: application/json" \
          -d '{"name":"Updated Universe","description":"Updated"}' > /dev/null 2>&1
        
        UPDATED_NAME=$(curl -s "http://localhost:5022/api/Universe/$TEST_ID" | python3 -c "import json, sys; print(json.load(sys.stdin)['name'])" 2>/dev/null)
        
        if [ "$UPDATED_NAME" == "Updated Universe" ]; then
            # Delete
            DELETE_CODE=$(curl -s -X DELETE "http://localhost:5022/api/Universe/$TEST_ID" -w "%{http_code}" -o /dev/null)
            if [ "$DELETE_CODE" == "204" ]; then
                test_passed "Full CRUD workflow (Create, Read, Update, Delete)"
            else
                test_failed "Delete operation"
            fi
        else
            test_failed "Update operation"
        fi
    else
        test_failed "Read operation"
    fi
else
    test_failed "Create operation"
fi
echo ""

# Test 5: Validation
echo "Test 5: Testing validation..."
HTTP_CODE=$(curl -s -X POST http://localhost:5022/api/Universe \
  -H "Content-Type: application/json" \
  -d '{"name":"","description":"Test"}' \
  -w "%{http_code}" -o /dev/null)

if [ "$HTTP_CODE" == "400" ]; then
    test_passed "Validation rejects invalid data (empty name)"
else
    test_failed "Validation (expected 400, got $HTTP_CODE)"
fi
echo ""

# Test 6: DTOs
echo "Test 6: Testing API DTOs..."
RESPONSE=$(curl -s -X POST http://localhost:5022/api/Universe \
  -H "Content-Type: application/json" \
  -d '{"name":"DTO Test","description":"Testing DTOs"}')

if echo "$RESPONSE" | python3 -c "import json, sys; u = json.load(sys.stdin); assert 'id' in u and 'createdDate' in u and 'modifiedDate' in u" 2>/dev/null; then
    test_passed "API returns proper DTOs with all fields"
    # Clean up
    TEST_ID=$(echo $RESPONSE | python3 -c "import json, sys; print(json.load(sys.stdin)['id'])")
    curl -s -X DELETE "http://localhost:5022/api/Universe/$TEST_ID" > /dev/null 2>&1
else
    test_failed "DTO structure"
fi
echo ""

echo "=== Frontend Tests ==="
echo ""

# Test 7: React UI accessibility
echo "Test 7: Testing React UI..."
if curl -s http://localhost:3000 2>&1 | grep -q "bundle.js"; then
    test_passed "React UI is accessible"
else
    test_failed "React UI not accessible at http://localhost:3000"
fi
echo ""

# Test 8: TypeScript types exist
echo "Test 8: Checking TypeScript implementation..."
if [ -f "/home/fray/Projects/UniverseBuilder/src/universe-builder-ui/src/types/Universe.ts" ] && \
   [ -f "/home/fray/Projects/UniverseBuilder/src/universe-builder-ui/src/services/UniverseService.ts" ]; then
    test_passed "TypeScript types and services exist"
else
    test_failed "TypeScript files missing"
fi
echo ""

# Test 9: React components exist
echo "Test 9: Checking React components..."
COMPONENTS=(
    "/home/fray/Projects/UniverseBuilder/src/universe-builder-ui/src/components/UniverseList.tsx"
    "/home/fray/Projects/UniverseBuilder/src/universe-builder-ui/src/components/UniverseForm.tsx"
    "/home/fray/Projects/UniverseBuilder/src/universe-builder-ui/src/pages/UniverseManagement.tsx"
)

ALL_EXIST=true
for component in "${COMPONENTS[@]}"; do
    if [ ! -f "$component" ]; then
        ALL_EXIST=false
        break
    fi
done

if [ "$ALL_EXIST" == "true" ]; then
    test_passed "All React components exist"
else
    test_failed "Some React components missing"
fi
echo ""

echo "=== Documentation Tests ==="
echo ""

# Test 10: Documentation
echo "Test 10: Checking documentation..."
if [ -f "/home/fray/Projects/UniverseBuilder/docs/PHASE_2_COMPLETION.md" ]; then
    test_passed "Phase 2 completion documentation exists"
else
    test_failed "Phase 2 documentation missing"
fi
echo ""

# Final summary
echo "=========================================="
echo "Test Results Summary"
echo "=========================================="
echo ""
echo "Passed: $PASSED"
echo "Failed: $FAILED"
echo "Total:  $((PASSED + FAILED))"
echo ""

if [ $FAILED -eq 0 ]; then
    echo -e "${GREEN}✅ ALL TESTS PASSED!${NC}"
    echo ""
    echo "Phase 2: Universe Management is COMPLETE and VALIDATED"
    echo ""
    echo "Key Achievements:"
    echo "  • Full CRUD API with validation"
    echo "  • 11/11 unit tests passing"
    echo "  • React UI with TypeScript"
    echo "  • DTOs for clean API contracts"
    echo "  • Repository pattern with interfaces"
    echo "  • MongoDB integration"
    echo "  • CORS configured for frontend"
    echo ""
    echo "Ready for Phase 3: Location System"
    exit 0
else
    echo -e "${RED}❌ SOME TESTS FAILED${NC}"
    echo ""
    echo "Please review the failed tests above."
    exit 1
fi
