# Changelog

## [1.0.0] - 2020-10-24
### Added
- First release! :)

## [1.1.0] - 2020-10-29
### Added
- Add Prompt utility dialog
- Add SelectionList field utility
- Add ReflectionUtility helper
### Changed
- Fix Assembly Definitions for build

## [1.2.0] - 2020-10-30
### Added
- Add ObjectField
- Add Gradient extensions
### Changed
- Improve Color extensions

## [1.2.1] - 2020-10-30
### Changed
- Update docs

## [1.2.2] - 2020-11-22
### Changed
- Minor code refactoring
- Update docs
- Fixed GameObject custom editor extension

## [1.2.3] - 2020-11-22
### Changed
- Move `/_Documentation` in `/Documentation~`

## [1.2.4] - 2020-11-23
### Changed
- Add online documentation shortcut
- Add the [*Buy Me A Coffee*](https://www.buymeacoffee.com/muffindev) link in docs

## [1.3.0] - 2020-12-23
### Added
- `Editable Asset` utilities
- `Moddable Component` utility
### Changed
- Various improvements

## [2.0.0] - 2020-12-23
### Changed
- Major namespaces refactoring

## [2.1.0] - 2020-12-28
### Added
- `EditorIcons` utility
- `Spinner` utility
- `EditableAsset` utilities
- `EditorHelpers.ExtendedObjectField()`
- `EditorHelpers.BackButton()`
# Changed
- `ReflectionUtility` improvements (also renamed `GetAllTypesImplementingGenericType()` to `GetAllTypesAssignableFrom()`)
- Update docs

## [2.1.1] - 2020-01-12 15:10
### Added
- Pagination utilities (`Pagination` class and editor GUI)
- Added editor GUI for drawing "path fields" (text field with *Browse* button)
### Changed
- Removed `ArrayExtension`, useless since `ListExtensions` also applies to arrays

## [2.2.0] - 2020-01-12 16:51
### Added
- Added `MuffinDevGUI` utility class for drawing custom editor GUI
### Changed
- Moved `EditorHelpers` methods for drawing editor GUI into `MuffinDevGUI` (making it obsolete)
- Classes that used `EditorHelpers` now use `MuffinDevGUI` utility
- Refactored documentation structure (separated `/Editor` and `/Runtime` elements), as Unity recommends

## [2.2.1] - 2020-02-02
### Added
- Added `GetComponentInHierarchy()` and `Find()` methods for `ComponentExtension` and `GameObjectExtension`
- Added `GameObject.GetComponentInHierarchy()` and `GameObject.Find()` methods
- Added `ComponentRef` attribute, making `AutoAssign` attribute obsolete
### Changed
- Removed `MonoBehaviourExtension` extensions, useless since `ComponentExtension` also applies to `Monobehaviour`s

## [2.3.0] - 2020-02-04
### Added
- Added `SerializationUtility` utility class
- Added `Blackboard` system and custom editor
- Added `TypeExtension.IsReallyPrimitive()` method
- Added `MuffinDevGUI.ComputeLabelledFieldRect()` method