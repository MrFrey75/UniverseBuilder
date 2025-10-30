# 🤝 Contributing to WorldBuilder

Thank you for your interest in contributing to WorldBuilder! We welcome contributions from everyone—whether you're fixing bugs, adding features, improving documentation, or suggesting enhancements.

This guide will help you get started and ensure a smooth contribution process.

---

## 📋 Code of Conduct

Please review our [Code of Conduct](CODE_OF_CONDUCT.md) before participating. We are committed to providing a welcoming and inclusive community for all contributors.

---

## 🚀 Getting Started

### Prerequisites

- **.NET 8 SDK or later** (for C# backend development)
- **Node.js LTS** (for React frontend development)
- **Git** (for version control)
- A GitHub account

### Setup

1. **Fork the repository** on GitHub.
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR_USERNAME/WorldBuilder.git
   cd WorldBuilder
   ```
3. **Add upstream remote** (to stay in sync with the main repo):
   ```bash
   git remote add upstream https://github.com/MrFrey75/WorldBuilder.git
   ```
4. **Create a feature branch** (see [Branch Strategy](#branch-strategy) below).

---

## 💡 How to Contribute

### Reporting Bugs

Found a bug? Please open a **GitHub Issue** using the [bug report template](.github/ISSUE_TEMPLATE/bug_report.md).

Include:
- Clear description of the issue
- Steps to reproduce
- Expected vs. actual behavior
- Screenshots/logs if applicable
- Environment (OS, .NET version, Node version, etc.)

### Requesting Features

Have an idea? Open a **GitHub Issue** using the [feature request template](.github/ISSUE_TEMPLATE/feature_request.md).

Include:
- Clear description of the feature
- Motivation and use case
- Proposed implementation (if applicable)
- Examples or mockups (if relevant)

### Improving Documentation

Documentation improvements are always welcome! To contribute:

1. Review the file you'd like to improve
2. Make your changes on a feature branch
3. Submit a Pull Request with a clear description

---

## 🔀 Branch Strategy

We follow a simple branching model:

- **`main`** – Stable, production-ready code. Direct commits are restricted.
- **Feature branches** – Create from `main` for new features, bug fixes, or documentation:
  ```bash
  git checkout -b feature/descriptive-name
  # or for bug fixes:
  git checkout -b fix/descriptive-name
  # or for documentation:
  git checkout -b docs/descriptive-name
  ```

**Branch naming conventions:**
- `feature/add-xyz` – New feature
- `fix/resolve-xyz` – Bug fix
- `docs/update-xyz` – Documentation
- `refactor/improve-xyz` – Code refactoring
- `test/add-xyz` – Test improvements

---

## 💻 Coding Standards

### C# (.NET) Backend

- **Naming conventions:**
  - Classes, methods, properties: `PascalCase`
  - Private fields: `_camelCase`
  - Constants: `UPPER_SNAKE_CASE`
- **Code style:**
  - Follow [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
  - Use meaningful, descriptive names
  - Keep methods focused and concise
  - Add XML documentation comments for public APIs
- **Organization:**
  - Use the Repository Pattern for data access
  - Separate concerns: Models, Services, Controllers
  - Implement validation at the service layer

### React / TypeScript Frontend

- **Naming conventions:**
  - Components: `PascalCase` (e.g., `UserProfile.tsx`)
  - Files (utilities): `camelCase` (e.g., `dateUtils.ts`)
  - Constants: `UPPER_SNAKE_CASE`
  - CSS classes: `kebab-case`
- **Code style:**
  - Use functional components and hooks (no class components)
  - Prefer `const` over `let` or `var`
  - Use TypeScript strict mode
  - Keep components small and reusable
  - Add JSDoc comments for complex logic
- **File structure:**
  - Keep related files in directories
  - Separate components, utilities, and styles

### General

- Use consistent indentation (2 spaces for JSON/YAML, project defaults for code)
- Avoid hard-coded values; use configuration or constants
- Write meaningful commit messages (see [Commit Messages](#commit-messages))
- Keep lines reasonably short (aim for < 120 characters)

---

## ✅ Testing

All contributions should include appropriate tests.

### C# Tests

- Use **xUnit** for unit tests
- Place tests in `tests/UniverseBuilder.Core.Tests` and `tests/UniverseBuilder.Api.Tests`
- Aim for **80%+ code coverage** in the Core library
- Run tests locally before pushing:
  ```bash
  dotnet test WorldBuilder.sln
  ```

### React Tests

- Use **Jest** and **React Testing Library**
- Place tests alongside components (e.g., `Component.test.tsx`)
- Test user interactions, not implementation details
- Run tests before pushing:
  ```bash
  npm test
  ```

### E2E Tests

- Use **Playwright** or **Cypress** for critical user flows
- Automate in CI/CD pipeline

---

## 📝 Commit Messages

Write clear, descriptive commit messages:

```
Short summary (50 characters or less)

More detailed explanation (72 characters per line) if needed.
Explain what changed and why.

Fixes #123        (reference related issues)
Related #456
```

**Examples:**
```
Add hierarchical location filtering
Fix race condition in event timeline
Docs: update quick start prerequisites
Refactor: simplify repository queries
Test: add edge cases for date conversion
```

---

## 🔄 Pull Request Process

1. **Update your branch** with the latest `main`:
   ```bash
   git fetch upstream
   git rebase upstream/main
   ```

2. **Push your branch** to your fork:
   ```bash
   git push origin feature/descriptive-name
   ```

3. **Open a Pull Request** on GitHub:
   - Use the [PR template](.github/PULL_REQUEST_TEMPLATE.md)
   - Link related issues (e.g., `Fixes #123`)
   - Provide a clear description of changes
   - Include screenshots/videos if applicable (UI changes)

4. **Respond to reviews:**
   - Address feedback promptly
   - Request re-review after making changes
   - Keep discussion professional and constructive

5. **Merge:**
   - A maintainer will merge once approved
   - Squash or rebase as needed (maintainer discretion)

---

## 🧪 CI/CD Checks

All PRs must pass automated checks:

- ✅ **Build:** C# solution builds successfully
- ✅ **Tests:** All tests pass (C#, React, E2E)
- ✅ **Code coverage:** Maintains or improves coverage targets
- ✅ **Linting:** Code passes style checks

---

## 📚 Documentation

When making changes that affect functionality:

- Update relevant `.md` files in the repo
- Update code documentation and comments
- Add/update examples if applicable
- Consider updating the Roadmap if it's a major feature

---

## 🎯 Development Roadmap

Check the [Roadmap](ROADMAP.md) for planned phases and current focus areas. We organize work into phases:

- **Phases 1-14:** Core feature development (foundation, entities, visualization, testing)
- **Phase 15:** Beta Testing & Deployment (current focus)
- **Phases 16+:** Post-1.0 features (customization, multi-user, integrations)

Feel free to tackle open issues or propose work aligned with the roadmap!

---

## 🤔 Questions?

- **GitHub Issues:** [Ask in Discussions](https://github.com/MrFrey75/WorldBuilder/discussions)
- **Architecture:** Check the Developer Guide (Developer_Guide.md coming soon)
- **Quick answers:** Look at the README and existing code

---

## ❤️ Thank You!

Your contributions—large or small—help make WorldBuilder better for everyone. We truly appreciate your effort and look forward to collaborating with you!

Happy coding! 🚀
