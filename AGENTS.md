# AI Contributor Guidelines for Backgammon Xamarin App

Welcome! This repository hosts a Visual Studio .NET (C#) Xamarin.Android backgammon game. Follow the guidance below whenever you make changes.

## Project Expectations
- Target platform: Android via Xamarin.Android (C#).
- Keep gameplay approachable: prioritize correctness of backgammon rules and clear UX over overly complex features.
- Favor maintainable, readable code over micro-optimizations.

## Code Style & Structure
- Use C# conventions: PascalCase for classes/methods, camelCase for local variables and parameters, ALL_CAPS for constants when appropriate.
- Organize source files under folders that mirror namespaces (e.g., `Activities/`, `Models/`, `Helpers/`).
- Avoid adding unused abstractions; keep classes focused on single responsibilities as outlined in the README.
- When editing XML layouts, maintain consistent indentation of two spaces.

## Testing & Validation
- Provide relevant unit or instrumentation tests when introducing new game logic.
- Manually exercise UI flows impacted by your changes when automated tests are unavailable, and document the manual steps in your PR message.

## Documentation & Communication
- Update the README and in-code XML/summary comments when adding or modifying features that change expected behavior.
- PR descriptions must include:
  - A concise summary of functional changes.
  - Notes on testing performed (automated or manual).
  - Any follow-up work that remains (if applicable).

## Asset Handling
- Store Android resources in the appropriate `Resources` subfolders (Layouts, Drawable, Values, etc.).
- Use vector drawables where possible; if raster images are required, ensure they are optimized for mobile.

Thank you for helping build a polished but accessible backgammon experience!
