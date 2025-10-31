# 🗺️ Development Roadmap: UniverseBuilder

**Current Status: Feature Complete (Beta Candidate)** ✓

The core functionality of UniverseBuilder, built on the C#/.NET 8+ and React stack, has been implemented across the first 14 planned phases. We are currently focused on optimization, bug resolution, and preparing for initial release.

This roadmap details the planned phases and specific, actionable tasks organized into sub-phases for full transparency and contribution guidance.

## Core Development Phases (Completed)

These 14 phases established the full functionality described in the `README.md`.

| Phase | Description | Completion Status |
|:---|:---|:---|
| **Phase 1** | Foundation & Setup | ✅ Completed |
| **Phase 2** | Universe Management (C# Core & API) | [✅ Completed](./docs/PHASE_2_COMPLETION.md) |
| **Phase 3** | Location System with Hierarchical Structure | Not Started |
| **Phase 4** | Species & Races System | Not Started |
| **Phase 5** | Notable Figures System | Not Started |
| **Phase 6** | Relationships & Connections | Not Started |
| **Phase 7** | Events & Timeline System | Not Started |
| **Phase 8** | Search & Filter Functionality | Not Started |
| **Phase 9** | Additional Entity Types (Organizations, Artifacts, Lore) | Not Started |
| **Phase 10**| Visualization (Mapping & Graphing) | Not Started |
| **Phase 11**| Rich Content & Entity Linking | Not Started |
| **Phase 12**| Polish & UX Refinement | Not Started |
| **Phase 13**| Data Management (Import/Export, Backup) | Not Started |
| **Phase 14**| Unit & Integration Testing | Not Started |

-----

## 📋 Detailed Phase Breakdown

### Phase 1: Foundation & Setup

**Goal:** Establish the project structure and core technology stack.

#### 1.1 Project Initialization

  * [x] Create **Visual Studio Solution** file (`UniverseBuilder.sln`).
  * [x] Set up **UniverseBuilder.Core** C# Class Library project (.NET 8+).
  * [x] Set up **UniverseBuilder.Api** ASP.NET Core Web API project.
  * [x] Initialize **React/TypeScript** frontend project (`universe-builder-ui`).
  * [x] Configure **.gitignore** for C#, Node.js, and common IDE files.
  * [x] Create initial **README.md** and **LICENSE** files.

#### 1.2 Database Configuration

  * [x] Integrate **MongoDB** into `UniverseBuilder.Core`.
  * [x] Set up **MongoDB driver** for .NET.
  * [x] Create initial database context class with basic configuration.
  * [x] Implement database migration strategy.

#### 1.3 Development Environment

  * [x] Configure **dependency injection** in ASP.NET Core API.
  * [x] Set up **CORS policy** for React frontend communication.
  * [x] Configure **development environment variables** and settings.
  * [x] Establish **logging infrastructure** (Serilog or default .NET logging).

-----

### Phase 2: Universe Management (C# Core & API)

**Goal:** Create the foundational Universe entity that serves as the container for all worldbuilding data.

**Status:** ✅ COMPLETED (October 31, 2025)

#### 2.1 Core Universe Model

  * [x] Define **Universe entity** model in `UniverseBuilder.Core/Models/`.
  * [x] Implement Universe properties: Name, Description, CreatedDate, ModifiedDate, Metadata.
  * [x] Create **UniverseRepository** following the Repository Pattern.
  * [x] Implement CRUD operations in repository layer.

#### 2.2 Universe Service Layer

  * [x] Create **UniverseService** in `UniverseBuilder.Core/Services/`.
  * [x] Implement business logic for Universe creation, updates, and deletion.
  * [x] Add validation rules for Universe entities.
  * [x] Implement Universe listing and retrieval methods.

#### 2.3 API Endpoints

  * [x] Create **UniverseController** in `UniverseBuilder.Api/Controllers/`.
  * [x] Implement RESTful endpoints: GET, POST, PUT, DELETE.
  * [x] Add API request/response DTOs for clean data transfer.
  * [x] Configure API routing and error handling.

#### 2.4 React UI Components

  * [x] Create **Universe management pages** in React.
  * [x] Implement Universe listing view with grid/card layout.
  * [x] Build Universe creation/edit forms with validation.
  * [x] Add Universe selection/switching functionality.
  * [x] Implement API service layer in TypeScript for Universe endpoints.

**Testing:** 11/11 unit tests passing, 9/9 integration tests passing.  
**Documentation:** See [PHASE_2_COMPLETION.md](docs/PHASE_2_COMPLETION.md)

-----

### Phase 3: Location System with Hierarchical Structure

**Goal:** Build a flexible, hierarchical location system supporting nested relationships (Planet → Continent → City, etc.).

#### 3.1 Location Data Model

  * [ ] Define **Location entity** model with hierarchical support.
  * [ ] Implement self-referencing relationship (ParentLocationId).
  * [ ] Add properties: Name, Type, Description, Coordinates, Climate, Population, etc.
  * [ ] Create **LocationRepository** with hierarchy-aware queries.

#### 3.2 Hierarchical Data Management

  * [ ] Implement **tree traversal methods** (get ancestors, descendants, siblings).
  * [ ] Create methods to retrieve full location paths (breadcrumb navigation).
  * [ ] Add validation to prevent circular hierarchies.
  * [ ] Implement reordering and reparenting logic.

#### 3.3 Location Service & API

  * [ ] Create **LocationService** with business logic.
  * [ ] Implement **LocationController** with RESTful endpoints.
  * [ ] Add specialized endpoints for hierarchy operations.
  * [ ] Support bulk operations for efficiency.

#### 3.4 React UI Implementation

  * [ ] Build **interactive tree-view component** for location hierarchy.
  * [ ] Implement **split-panel view** (tree on left, details on right).
  * [ ] Add **drag-and-drop functionality** for hierarchy management.
  * [ ] Create **breadcrumb navigation component** showing full path.
  * [ ] Implement location type filtering and search.
  * [ ] Add placeholder for future **map integration**.

-----

### Phase 4: Species & Races System

**Goal:** Create a comprehensive system for managing species, races, and their characteristics.

#### 4.1 Species Data Model

  * [ ] Define **Species entity** model with detailed properties.
  * [ ] Add fields: Name, Description, PhysicalTraits, LifeSpan, Culture, Abilities, etc.
  * [ ] Support for **sub-races** or variants (optional hierarchy).
  * [ ] Create **SpeciesRepository** with filtering capabilities.

#### 4.2 Template System

  * [ ] Implement **species template feature** for quick creation.
  * [ ] Create predefined templates (Humanoid, Dragon, Elf, etc.).
  * [ ] Allow users to save custom templates.
  * [ ] Support template cloning and modification.

#### 4.3 Service & API Layer

  * [ ] Create **SpeciesService** with validation logic.
  * [ ] Implement **SpeciesController** with full CRUD operations.
  * [ ] Add endpoints for template management.
  * [ ] Support advanced filtering and search.

#### 4.4 React UI Components

  * [ ] Build **master-detail interface** for species management.
  * [ ] Implement **dynamic form generation** based on C# models.
  * [ ] Create species template selector and customization UI.
  * [ ] Add image/icon support for species visualization.
  * [ ] Implement species comparison view.

-----

### Phase 5: Notable Figures System

**Goal:** Develop a robust system for managing characters and notable figures with rich detail.

#### 5.1 Figure Data Model

  * [ ] Define **NotableFigure entity** model.
  * [ ] Add core properties: Name, Species, BirthDate, DeathDate, Biography, Traits, etc.
  * [ ] Link to **Species** and **Location** entities.
  * [ ] Support for custom attributes and tags.
  * [ ] Create **FigureRepository** with complex queries.

#### 5.2 Character Attributes

  * [ ] Implement **physical characteristics** system.
  * [ ] Add **personality traits** and alignment system.
  * [ ] Support **skills, abilities, and powers** tracking.
  * [ ] Implement **status tracking** (alive, deceased, unknown, etc.).

#### 5.3 Service & API Layer

  * [ ] Create **FigureService** with business rules.
  * [ ] Implement **FigureController** with RESTful endpoints.
  * [ ] Add filtering by species, location, status, etc.
  * [ ] Support bulk operations and advanced search.

#### 5.4 React UI Implementation

  * [ ] Build **Kanban-style grid view** for figure browsing.
  * [ ] Implement **collapsible, tabbed sidebar** for detailed information.
  * [ ] Create comprehensive character creation/edit forms.
  * [ ] Add **multi-select component** for relationship management.
  * [ ] Implement character image/avatar support.
  * [ ] Add real-time validation against C# data models.

-----

### Phase 6: Relationships & Connections

**Goal:** Enable complex relationship mapping between all entity types.

#### 6.1 Relationship Data Model

  * [ ] Define **Relationship entity** with polymorphic support.
  * [ ] Support relationships between any entity types (Figure-Figure, Figure-Location, etc.).
  * [ ] Implement relationship types: Family, Political, Professional, Romantic, Rivalry, etc.
  * [ ] Add relationship strength/importance levels.
  * [ ] Support bidirectional and unidirectional relationships.

#### 6.2 Relationship Management

  * [ ] Create **RelationshipRepository** with graph-like queries.
  * [ ] Implement methods to retrieve entity connection networks.
  * [ ] Add validation to prevent duplicate or conflicting relationships.
  * [ ] Support relationship history and status changes.

#### 6.3 Service & API Layer

  * [ ] Create **RelationshipService** with complex business logic.
  * [ ] Implement **RelationshipController** with specialized endpoints.
  * [ ] Add endpoints for relationship discovery and analysis.
  * [ ] Support filtering by relationship type and entity.

#### 6.4 React UI Components

  * [ ] Build relationship management interfaces in entity detail views.
  * [ ] Create **quick-add relationship widgets**.
  * [ ] Implement relationship type selector with icons.
  * [ ] Add relationship timeline view (when relationships formed/ended).
  * [ ] Prepare foundation for graph visualization (Phase 10).

-----

### Phase 7: Events & Timeline System

**Goal:** Create a comprehensive event and timeline management system supporting custom calendars.

#### 7.1 Event Data Model

  * [ ] Define **Event entity** model with temporal properties.
  * [ ] Support both exact dates and approximate/era-based timing.
  * [ ] Implement event types: Political, Battle, Birth, Death, Discovery, etc.
  * [ ] Link events to locations, figures, and other entities.
  * [ ] Add importance/impact levels.

#### 7.2 Custom Calendar System

  * [ ] Create **Calendar/Dating System** configuration model.
  * [ ] Support custom months, days, eras, and epochs.
  * [ ] Implement date conversion utilities.
  * [ ] Add support for multiple concurrent calendar systems.

#### 7.3 Timeline Management

  * [ ] Create **EventRepository** with temporal queries.
  * [ ] Implement timeline retrieval methods (by date range, by entity, by type).
  * [ ] Add support for alternate timelines/branching histories.
  * [ ] Implement event ordering and conflict detection.

#### 7.4 Service & API Layer

  * [ ] Create **EventService** with temporal business logic.
  * [ ] Implement **EventController** with timeline endpoints.
  * [ ] Add calendar configuration endpoints.
  * [ ] Support complex temporal queries and filtering.

#### 7.5 React UI Implementation

  * [ ] Build **dynamic calendar/Gantt chart component**.
  * [ ] Implement **drag-and-drop event manipulation**.
  * [ ] Create **custom date picker** for configured calendars.
  * [ ] Add **timeline view modes**: Linear, Branching, Chronological Cards.
  * [ ] Implement era/age grouping functionality.
  * [ ] Add event filtering by type, importance, and entities.

-----

### Phase 8: Search & Filter Functionality

**Goal:** Implement powerful, context-aware search and advanced filtering across all entities.

#### 8.1 Search Infrastructure

  * [ ] Implement **full-text search** in MongoDB database.
  * [ ] Create unified search service searching across all entity types.
  * [ ] Add search result ranking and relevance scoring.
  * [ ] Support search within specific entity types or globally.

#### 8.2 Advanced Filtering

  * [ ] Build **complex query builder** using LINQ expressions.
  * [ ] Support multiple filter criteria combinations (AND/OR logic).
  * [ ] Implement filtering by entity properties, relationships, and tags.
  * [ ] Add saved filter/query templates.

#### 8.3 Service & API Layer

  * [ ] Create **SearchService** with optimized query logic.
  * [ ] Implement **SearchController** with flexible endpoints.
  * [ ] Add pagination and result limiting for performance.
  * [ ] Support export of search results.

#### 8.4 React UI Implementation

  * [ ] Build **persistent, context-aware search bar**.
  * [ ] Implement **type-ahead/autocomplete** functionality.
  * [ ] Create **Advanced Filter modal** with dynamic filter builder.
  * [ ] Add search result highlighting and preview.
  * [ ] Implement **recent searches** and **saved searches** features.
  * [ ] Add global keyboard shortcut for search (Ctrl/Cmd+K).

-----

### Phase 9: Additional Entity Types (Organizations, Artifacts, Lore)

**Goal:** Expand the entity system with Organizations, Artifacts, and Lore entries.

#### 9.1 Organization System

  * [ ] Define **Organization entity** model.
  * [ ] Add properties: Name, Type, Purpose, Hierarchy, Founded/Dissolved dates, etc.
  * [ ] Implement **organizational hierarchy** (similar to locations).
  * [ ] Link organizations to figures (members, leaders) and locations.
  * [ ] Create **OrganizationRepository** and service layer.

#### 9.2 Artifact System

  * [ ] Define **Artifact entity** model.
  * [ ] Add properties: Name, Type, Description, Powers, History, CurrentLocation, etc.
  * [ ] Support artifact ownership history.
  * [ ] Link artifacts to figures, locations, and events.
  * [ ] Create **ArtifactRepository** and service layer.

#### 9.3 Lore System

  * [ ] Define **Lore entity** model for world knowledge, myths, and legends.
  * [ ] Add properties: Title, Content, Type (Myth, Legend, History, Religion, etc.).
  * [ ] Support lore categorization and tagging.
  * [ ] Link lore entries to relevant entities.
  * [ ] Create **LoreRepository** and service layer.

#### 9.4 API & Controllers

  * [ ] Implement **OrganizationController** with full CRUD and hierarchy operations.
  * [ ] Implement **ArtifactController** with ownership tracking endpoints.
  * [ ] Implement **LoreController** with categorization endpoints.
  * [ ] Add specialized search endpoints for each entity type.

#### 9.5 React UI Components

  * [ ] Build **card-based layout** for Organizations with hierarchy visualization.
  * [ ] Create **organization chart component** for command structure.
  * [ ] Implement **member management widget** linked to Figures.
  * [ ] Build **inventory-style list** for Artifacts.
  * [ ] Create Artifact detail view with ownership timeline.
  * [ ] Implement **searchable Lore library** with categorization.
  * [ ] Add Rich Text Editor for lore content with entity linking.

-----

### Phase 10: Visualization (Mapping & Graphing)

**Goal:** Create interactive visualizations for relationships, timelines, and location mapping.

#### 10.1 Relationship Graph Visualization

  * [ ] Integrate **graph visualization library** (e.g., D3.js, Vis.js, or React Flow).
  * [ ] Build **interactive graph component** rendering entities as nodes.
  * [ ] Implement relationship edges with directional arrows and labels.
  * [ ] Add node filtering by entity type.
  * [ ] Support relationship type filtering and highlighting.
  * [ ] Implement zoom, pan, and node selection interactions.
  * [ ] Add **double-click to view entity details** functionality.

#### 10.2 Timeline Visualization Enhancements

  * [ ] Enhance timeline component with **visual event markers**.
  * [ ] Implement **branching visualization** for alternate timelines.
  * [ ] Add color-coding by event type or importance.
  * [ ] Support timeline filtering and search integration.
  * [ ] Create **timeline export** functionality (image/PDF).

#### 10.3 Location Mapping (Foundation)

  * [ ] Create **map canvas component** for location visualization.
  * [ ] Implement basic map creation and editing tools.
  * [ ] Add support for **custom map images** as backgrounds.
  * [ ] Enable **location pin placement** on maps.
  * [ ] Link map pins to location entities with click-through navigation.
  * [ ] Support multiple map levels for hierarchical locations.

#### 10.4 Data Visualization Dashboard

  * [ ] Create **dashboard page** with key metrics and statistics.
  * [ ] Implement entity count widgets (Figures, Locations, Events, etc.).
  * [ ] Add **recent activity feed** showing latest changes.
  * [ ] Create visual charts for entity distribution and relationships.

-----

### Phase 11: Rich Content & Entity Linking

**Goal:** Implement a powerful rich text editor with seamless entity cross-referencing.

#### 11.1 Rich Text Editor Integration

  * [ ] Integrate **WYSIWYG Markdown editor** (e.g., TipTap, Slate, or similar).
  * [ ] Configure editor with formatting toolbar (bold, italic, headers, lists, etc.).
  * [ ] Add support for **inline images** with upload/embedding.
  * [ ] Implement **code block** support for technical world details.
  * [ ] Add support for tables and other advanced formatting.

#### 11.2 Entity Linking System

  * [ ] Implement **`@` mention system** for entity linking.
  * [ ] Create **entity search popup** triggered by typing `@`.
  * [ ] Support searching across all entity types during linking.
  * [ ] Convert entity mentions to **active hyperlinks** in rendered content.
  * [ ] Add **hover tooltips** showing entity preview on linked entities.
  * [ ] Implement click navigation to linked entity details.

#### 11.3 Backlink Management

  * [ ] Track **backlinks** (reverse entity references) in database.
  * [ ] Create **backlink service** for managing entity connections.
  * [ ] Add **backlink display component** showing where entities are referenced.
  * [ ] Implement backlink updates when content changes.

#### 11.4 Content Preview & Rendering

  * [ ] Create **consistent content rendering** component for all entity descriptions.
  * [ ] Implement **Markdown to HTML conversion** with security sanitization.
  * [ ] Add support for entity link rendering in preview mode.
  * [ ] Implement **print-friendly** content views.

-----

### Phase 12: Polish & UX Refinement

**Goal:** Enhance the user experience with polish, responsiveness, and usability improvements.

#### 12.1 UI/UX Consistency

  * [ ] Establish **design system** with consistent colors, spacing, and typography.
  * [ ] Create reusable **component library** for common UI patterns.
  * [ ] Implement **consistent loading states** and spinners.
  * [ ] Add **empty state designs** for lists and collections.
  * [ ] Ensure **consistent error messaging** and validation feedback.

#### 12.2 Responsive Design

  * [ ] Ensure all components are **mobile-responsive**.
  * [ ] Optimize layouts for tablet and smaller screens.
  * [ ] Implement **responsive navigation** (hamburger menu, etc.).
  * [ ] Test and fix layout issues across different screen sizes.

#### 12.3 Performance Optimization

  * [ ] Implement **React code splitting** for faster initial loads.
  * [ ] Add **lazy loading** for heavy components.
  * [ ] Optimize **render performance** with React.memo and useMemo.
  * [ ] Implement **virtual scrolling** for large lists.
  * [ ] Optimize API calls with debouncing and caching.

#### 12.4 Usability Enhancements

  * [ ] Add **keyboard shortcuts** for common actions.
  * [ ] Implement **undo/redo functionality** for entity edits.
  * [ ] Create **confirmation dialogs** for destructive actions.
  * [ ] Add **tooltips** for complex features and icons.
  * [ ] Implement **context menus** (right-click) for quick actions.
  * [ ] Add **breadcrumb navigation** across the application.

#### 12.5 Onboarding & Help

  * [ ] Create **welcome screen** for first-time users.
  * [ ] Implement **interactive tutorial** or guided tour.
  * [ ] Add **help documentation** accessible from the UI.
  * [ ] Create **example universe template** for quick start.

-----

### Phase 13: Data Management (Import/Export, Backup)

**Goal:** Provide comprehensive data management features for backup, export, and import.

#### 13.1 Export Functionality

  * [ ] Implement **full universe export** to JSON format.
  * [ ] Add **selective export** (choose specific entities/categories).
  * [ ] Support **export to PDF** for printable world documentation.
  * [ ] Implement **export to Markdown** for portable documentation.
  * [ ] Add metadata and version information to exports.

#### 13.2 Import Functionality

  * [ ] Implement **JSON import** with validation.
  * [ ] Add **conflict resolution** for importing into existing universes.
  * [ ] Support **merge strategies** (skip, overwrite, create new).
  * [ ] Implement **import preview** before committing changes.
  * [ ] Add progress tracking for large imports.

#### 13.3 Backup & Restore

  * [ ] Create **automatic backup system** with configurable intervals.
  * [ ] Implement **manual backup** on-demand.
  * [ ] Add **backup file management** (list, delete old backups).
  * [ ] Implement **restore functionality** with backup validation.
  * [ ] Support backup compression to save space.

#### 13.4 Service & API Layer

  * [ ] Create **DataManagementService** handling all import/export logic.
  * [ ] Implement **DataManagementController** with secure endpoints.
  * [ ] Add background job support for long-running operations.
  * [ ] Implement progress reporting via WebSockets or polling.

#### 13.5 React UI Implementation

  * [ ] Build **data management settings panel**.
  * [ ] Create export wizard with format selection and filtering.
  * [ ] Implement import wizard with validation and conflict resolution.
  * [ ] Add **backup management UI** with list and restore options.
  * [ ] Implement **real-time progress bars** for operations.
  * [ ] Add success/error notifications for all operations.

-----

### Phase 14: Unit & Integration Testing

**Goal:** Establish comprehensive test coverage for reliability and maintainability.

#### 14.1 C# Core Unit Tests

  * [ ] Set up **xUnit test project** (`UniverseBuilder.Core.Tests`).
  * [ ] Write unit tests for all **repository classes**.
  * [ ] Write unit tests for all **service layer** business logic.
  * [ ] Test **data validation** rules and constraints.
  * [ ] Test **complex queries** and hierarchy operations.
  * [ ] Achieve **minimum 80% code coverage** for Core library.

#### 14.2 API Integration Tests

  * [ ] Set up **API integration test project** (`UniverseBuilder.Api.Tests`).
  * [ ] Write tests for all **controller endpoints**.
  * [ ] Test **request/response DTOs** serialization.
  * [ ] Test **error handling** and validation responses.
  * [ ] Test **authentication and authorization** (if implemented).
  * [ ] Use **in-memory database** for test isolation.

#### 14.3 React Component Tests

  * [ ] Set up **Jest and React Testing Library**.
  * [ ] Write unit tests for **reusable components**.
  * [ ] Test **form validation** and user interactions.
  * [ ] Test **API service layer** with mocked responses.
  * [ ] Write tests for **state management** logic.

#### 14.4 End-to-End Testing

  * [ ] Set up **Playwright or Cypress** for E2E tests.
  * [ ] Write E2E tests for **critical user flows** (create universe, add character, etc.).
  * [ ] Test **full CRUD operations** across entities.
  * [ ] Test **search and filter functionality** end-to-end.
  * [ ] Test **data import/export workflows**.

#### 14.5 Performance Testing

  * [ ] Create **performance test suite** for API endpoints.
  * [ ] Test database performance with **large datasets**.
  * [ ] Identify and optimize **slow queries**.
  * [ ] Test React UI performance with **large data volumes**.
  * [ ] Implement **load testing** for API scalability.

#### 14.6 CI/CD Integration

  * [ ] Set up **GitHub Actions** or similar CI/CD pipeline.
  * [ ] Configure **automated test runs** on pull requests.
  * [ ] Add **code coverage reporting** to CI pipeline.
  * [ ] Implement **automated build verification**.
  * [ ] Set up **automated deployment** (for future releases).

-----

## 🚧 Phase 15: Beta Testing & Deployment (Current Focus)

**Goal:** Achieve a stable, performant version 1.0 release candidate and establish a clear deployment strategy.

### 15.1 Stability & Performance

  * [ ] Conduct **performance profiling** on the C# API endpoints.
  * [ ] Optimize **MongoDB queries** within the `UniverseBuilder.Core` Data layer for large datasets.
  * [ ] Implement **React code splitting** and lazy loading for faster UI initial load times.
  * [ ] Resolve all **known P1/P2 bugs** identified during internal testing.

### 15.2 Quality Assurance & Feedback

  * [ ] Establish a **public beta testing program** and feedback mechanism (GitHub Discussions).
  * [ ] Finalize and lock the **database schema** in C# Core for version 1.0.
  * [ ] Review and confirm **cross-browser compatibility** for the React UI.

### 15.3 Deployment Strategy (Key Focus for C# and React)

  * [ ] Define final **packaging strategy** for standalone desktop use (e.g., bundling the C# API and React UI using Electron, or a simple self-contained .NET deployment).
  * [ ] Create automated **build scripts** for Windows, macOS, and Linux targets.
  * [ ] Publish **initial standalone installers** for beta testers.

-----

## 📈 Future Development (Post 1.0 Release)

These phases will focus on highly requested features, external integrations, and scaling the application with MongoDB.

### Phase 16: User Experience & Customization

  * [ ] **Customizable UI Themes** (dark mode, custom color palettes).
  * [ ] **React Accessibility (A11Y)** review and enhancements.
  * [ ] **Keyboard Shortcut mapping** for common tasks (e.g., New Character, Save).
  * [ ] User-defined **custom fields** on all core entities (driven by C# configurations).

### Phase 17: Multi-User & Synchronization

  * [ ] **Authentication System** (IdentityServer or simple C# Identity for multi-user support).
  * [ ] Ability to switch the C# Core to connect to a **full-scale SQL database** (PostgreSQL/SQL Server) for true hosting/collaboration.
  * [ ] Implement **real-time synchronization** for collaborative worldbuilding.
  * [ ] **Version Control** for Universe data, allowing rollback of major changes.

### Phase 18: External Integration

  * [ ] **Import/Export support** for common worldbuilding file formats (e.g., Fountain, Markdown structures).
  * [ ] **API Key support** for external tools to read/write Universe data.
  * [ ] Integration with **image hosting/storage services** (e.g., S3, Azure Blob) for asset management.
  * [ ] **Webhooks** support for triggering actions based on data changes (e.g., notify a Discord channel).

-----

## 🤝 Contribution Guide Summary

We welcome contributions to any of the above phases\!

1.  Review the **[Developer Guide](docs/DEVELOPER_GUIDE.md)** for setup details.
2.  Check the **[GitHub Issues](https://www.google.com/search?q=https://github.com/MrFrey75/UniverseBuilder/issues)** for current bug priorities and feature requests.
3.  Ensure your changes align with the **C#/.NET Core** and **React** stack.

-----

This roadmap is designed to guide both users and contributors through the project's lifecycle.

Would you like to refine any of the specific technical tasks in the current **Phase 15: Beta Testing & Deployment**?