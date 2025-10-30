<div align="center">

# 🌍 WorldBuilder

### A Comprehensive Worldbuilding & Universe Creation Tool (C#/.NET Core & React)

[](https://dotnet.microsoft.com/)
[](https://reactjs.org/)
[](https://www.mongodb.com/)
[](LICENSE)

*Empower your storytelling with a powerful, performant application built on a **robust C# backend** and a modern, cross-platform **React frontend**.*

[Features](#-features-built-on-net-core--react) • [Quick Start](QUICKSTART.md) • [User Guide](USER_GUIDE.md) • [Developer Guide](DEVELOPER_GUIDE.md) • [Roadmap](ROADMAP.md)

</div>

-----

## 📖 Overview

**WorldBuilder** is a powerful cross-platform application that helps you create, organize, and maintain consistency across your fictional worlds. It is built on a **C#/.NET Core backend** and a modern, responsive **React frontend**.

By leveraging a **highly structured C# core library (`UniverseBuilder.Core`)**, WorldBuilder ensures data integrity and scalability, allowing authors, storytellers, and creators to efficiently keep track of every detail in their universe. The embedded **MongoDB** database makes local development and deployment simple and fast.

### 🎯 Perfect For

  - 📚 **Authors** - Maintain consistency and detail across large book series.
  - ✍️ **Storytellers** - Organize complex narratives, plot lines, and character arcs.
  - 🎮 **Game Masters** - Build rich, searchable, and consistent RPG campaign worlds.
  - 🎬 **Screenwriters** - Track characters, locations, and multi-timeline events.

-----

## ✨ Features: Built on .NET Core & React

The C# `UniverseBuilder.Core` library provides high-integrity data, which the responsive React UI presents in a powerful and engaging way.

### Core Entity Management

| Feature Area | React UI Design & User Experience (UX) |
|:---|:---|
| **👥 Notable Figures (Characters)** | Uses a **Kanban-style grid** for quick viewing, transitioning to a **collapsible, tabbed sidebar** for details. Data validation is performed in real-time against the C# data models. Relationships are managed via a dedicated **multi-select component**. |
| **🗺️ Hierarchical Locations** | Implemented as a **split-panel view**. The left features an **interactive tree-view** for drag-and-drop hierarchy adjustments, while the right includes **map integration** and a persistent **breadcrumb navigation** to show the full path (e.g., Planet \> Continent \> City). |
| **🧬 Species & Races** | A **master-detail interface** utilizing **dynamic form generation** based on the C# models. Includes a **template feature** for quickly cloning common species types to speed up world creation. |
| **📅 Events & Timeline** | The primary view is a **dynamic calendar/gantt chart component** that allows drag-and-drop event manipulation. Supports a dedicated **date picker that works with custom calendars** configured in the backend. |
| **🏛️ Organizations & Factions** | A **card-based layout** for primary viewing. The detail view includes a **dynamic hierarchy chart** (org chart) to visualize command structure and a **member management widget** connected to the Figures dataset. |
| **🏺 Artifacts & Lore** | A searchable, inventory-style list. The detail view leverages a Rich Text Editor with an **inline entity-linking feature**, allowing users to type `@` followed by any entity name to create an active hyperlink, ensuring deep lore cross-referencing. |

### Advanced Features

  * **🔗 Relationship Mapping:** An **interactive graph visualization component** that renders entities as nodes and relationships as directed edges. Users can filter by relationship type and double-click any node to instantly view details.
  * **⏳ Timeline Visualization:** Provides multiple React view components: **Linear Scrolling View**, **Branching View** (for alternate history/canon splits), and a **Chronological Card View** grouped by user-defined eras/ages.
  * **🔍 Advanced Search & Filtering:** A persistent, **context-aware search bar** that supports type-ahead search. The "Advanced Filter" modal allows users to build complex queries that are translated into efficient C# LINQ/SQL database operations.
  * **📝 Rich Text Editor:** A fully integrated, **Wysiwyg Markdown editor** that supports inline images, code blocks, and the crucial **entity-linking system**.
  * **📊 Data Management:** A dedicated settings panel for **Export, Import, Backup, and Restore**. The C# API handles the serialization and MongoDB operations, providing the React UI with real-time progress bars.

-----

## 🛠️ Technology Stack

WorldBuilder is a full-stack application leveraging modern, cross-platform technologies. The core logic is structured to support strong engineering practices (similar to **MVVM**) using the **Repository Pattern** for clean data access.

| Layer | Technology | Purpose |
|:---|:---|:---|
| **Backend Core** | **C# 12 / .NET 8+** | Core business logic, data models, and services (Structured to support MVVM principles). |
| **API** | **ASP.NET Core** | Provides RESTful endpoints for secure communication with the frontend. |
| **Data Access** | **UniverseBuilder.Core** | Class Library implementing the Repository Pattern. |
| **Database** | **MongoDB** | Flexible, document-based data storage for development and production. |
| **Frontend UI** | **React / TypeScript** | Modern, responsive, cross-platform user interface. |
| **Architecture** | **Layered Architecture** | Clear separation between UI, API, and Core business logic for maintainability. |
| **Platform** | Windows / macOS / Linux | Cross-platform desktop (via hosting) and web application. |

-----

## 📁 Project Structure

The solution is organized into a standard .NET Core structure, separating the core logic, API, and frontend.

```
WorldBuilder/
├── README.md
├── docs/                             # User Guides and Architecture documentation
├── src/
│   ├── UniverseBuilder.sln             # Main Visual Studio Solution file
│   │
│   ├── UniverseBuilder.Core/           # C# Class Library Project (Business Logic)
│   │   ├── Models/                   # Domain Entities and Data Transfer Objects (DTOs)
│   │   ├── Services/                 # Business logic implementation
│   │   └── Data/                     # Repository Pattern implementation (MongoDB access)
│   │
│   ├── UniverseBuilder.Api/            # ASP.NET Core Web API Project
│   │   ├── Controllers/              # RESTful API endpoints
│   │   └── Program.cs                # API setup and dependency injection
│   │
│   └── universe-builder-ui/            # React Frontend Project (TypeScript)
│       ├── src/                      # React source code
│       ├── public/                   # Static assets
│       └── package.json              # Node dependencies
└── tests/
    ├── UniverseBuilder.Core.Tests/       # Unit and integration tests for Core logic
    └── UniverseBuilder.Api.Tests/        # API integration tests
```

-----

## 🚀 Getting Started

### Prerequisites

Before you begin, ensure you have the following installed:

  - **[.NET 8 SDK or later]** - Required for the C# backend.
  - **[Node.js (LTS)]** - Required for the React frontend and `npm`.

### Installation & Quick Start

📚 **[See Quick Start Guide →](QUICKSTART.md)** for detailed installation and getting started instructions.

**Quick Steps**:

1.  Clone repository.
2.  Restore and run the C# API.
3.  Install dependencies and start the React UI.

### Quick Development Setup

Use the command line to get both the API and the UI running.

```bash
# Clone repository
git clone https://github.com/MrFrey75/WorldBuilder.git
cd WorldBuilder/src

# 1. Build & Run the C# Backend API (in first terminal)
dotnet restore
dotnet run --project UniverseBuilder.Api/

# 2. Start the React Frontend UI (in second terminal)
cd universe-builder-ui
npm install
npm start
```

-----

## 💻 Development

📖 **[See Developer Guide →](DEVELOPER_GUIDE.md)** for comprehensive technical documentation.

WorldBuilder is built for performance and maintainability using modern **C#/.NET standards**. The use of **MongoDB** simplifies the local setup, allowing developers to get the application running and start contributing with minimal overhead. The project structure is intentionally designed to support architectural patterns that align with your experience in **WPF MVVM**.

### Running Tests

Unit and integration tests are available for the C# projects.

```bash
# Run all C# tests
dotnet test WorldBuilder.sln
```

-----

## 🗺️ Development Roadmap

### Current Status: **Feature Complete (Beta Candidate)** ✓

The core feature set is complete, and the project is now entering the **Beta Testing and Polish Phase** (Phase 15). We are focused on stability, performance, and preparing the standalone installers.

**[📋 View Complete Roadmap →](ROADMAP.md)**

-----

## 🤝 Contributing

Contributions are welcome\! Whether you're fixing bugs in the C# core, enhancing the React UI, or improving documentation, your help is appreciated.

### Contribution Guidelines

  - Follow standard **C# and .NET naming conventions** and best practices.
  - Follow **PEP 8 style guidelines** where applicable in scripts/tools.
  - Write clear, descriptive commit messages.
  - Add unit tests for new features in the C# Core library.
  - Ensure all tests pass before submitting a Pull Request.

-----

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

-----

## 📞 Contact & Support

  - **Quick Start**: [QUICKSTART.md](QUICKSTART.md)
  - **Issues**: [GitHub Issues](https://github.com/MrFrey75/WorldBuilder/issues)
  - **Discussions**: [GitHub Discussions](https://github.com/MrFrey75/WorldBuilder/discussions)

-----

<div align="center">

**[⬆ Back to Top](#-worldbuilder)**

Made with ❤️ for storytellers and worldbuilders everywhere

</div>

-----

This looks ready to publish\! Would you like me to help you draft the content for one of the linked documents, such as the **Developer Guide** or the **Quick Start Guide**?