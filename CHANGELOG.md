# Changelog
All notable changes to this project will be documented in this file.

The format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [1.2.0] - 2026-03-25
### Added
- README.md in /Modules/Statemachines
- CHANGELOG.md
- /Utils/DI and my basic Dependency Injection interfaces that I prefer using
- /Tools/blender_exporter.py as a script to export models from .blend to .fbx automatically
- BootStrappers to /Utils/Core to help bootstrap modules of a game

### Changed
- Restructured the whole repo to work with my Uinit tool that now uses this repo to pull code from

### Known Issues
- There are no meta files includes, which can cause Unity to freak out during imports. It should however auto-generate
any .meta requires upon a reimport