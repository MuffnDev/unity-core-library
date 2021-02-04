# Muffin Dev for Unity - `EditorHelpers`

Bundle of utility methods for Editor operations.

## Public API

### Constants

#### `ASSETS_FOLDER`

```cs
public const string ASSETS_FOLDER = "Assets"
```

#### `RESOURCES_FOLDER`

```cs
public const string RESOURCES_FOLDER = "Resources"
```

#### `DEFAULT_ASSET_EXTENSION`

```cs
public const string DEFAULT_ASSET_EXTENSION = "asset"
```

### Generic helpers

#### `FocusAsset()`

```cs
public static void FocusAsset(Object _Object, bool _SelectAsset = true, bool _PingAsset = true)
```

Focuses the given object in the project view, by selecting it and/or highlight it in the Project view.

#### `CreateProjectFolder()`

```cs
public static void CreateProjectFolder(string _AssetsRelativePath)
```

Creates a folder in this project if it doesn't exist.

#### `FindEditorWindow()`

```cs
public static EditorWindow FindEditorWindow(string _WindowTitle)
```

Gets the EditorWindow instance of the named window.

* `string _WindowTitle`: The name of the window to get. For example, you can get the Inspector view by passing "Inspector" as parameter

#### `GetPropertyType()`

```cs
public static Type GetPropertyType(SerializedProperty _Property)
```

Gets the C# type of the given SerializedProperty.

Returns the found property type, or null if the property path can't be used to get final type.

#### `IsPropertyAnArrayEntry()`

```cs
public static bool IsPropertyAnArrayEntry(SerializedProperty _Property)
```

Checks if the given `SerializedProperty` is contained in an array.

- `SerializedProperty _Property`: The property you want to check.

### Assets helpers`

#### `CreateAsset()`

```cs
public static AssetCreationResult CreateAsset<TAssetType>(string _FileName = "", string _RelativePath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
public static AssetCreationResult CreateAsset(Type _AssetType, string _FileName = "", string _RelativePath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
```

Creates an asset of the given type.

* `<TAssetType>`: The type of the asset to create
* `Type _AssetType`: The type of the asset to create
* `string _FileName = ""`: Default name of the asset file
* `string _RelativePath = ""`: Default path to the folder of the asset file
* `string _FileExtension = "asset"`: Default extension of the asset file
* `bool _FocusAsset = true`: Defines if the asset should be selected and highlighted in the Project view after its creation

#### `CreateAssetPanel()`

```cs
public static AssetCreationResult CreateAssetPanel(Type _AssetType, string _PanelTitle = "Save new asset", string _FileName = "", string _DefaultPath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
public static AssetCreationResult CreateAssetPanel(Type _AssetType, out Object _CreatedAsset, string _PanelTitle = "Save new asset", string _FileName = "", string _DefaultPath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
public static AssetCreationResult CreateAssetPanel<TAssetType>(string _PanelTitle, string _FileName = "", string _DefaultPath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
public static AssetCreationResult CreateAssetPanel<TAssetType>(out TAssetType _CreatedAsset, string _PanelTitle, string _FileName = "", string _DefaultPath = "", string _FileExtension = DEFAULT_ASSET_EXTENSION, bool _FocusAsset = true);
```

Creates an asset of the given type.

* `Type _AssetType`: The type of the Scriptable Object to create
* `<TAssetType>`: The type of the asset to create
* `string _PanelTitle_ = ""`: Title of the panel window
* `string _FileName = ""`: Default name of the asset file
* `string _DefaultPath = ""`: Default extension of the asset file
* `string _FileExtension = "asset"`: Default extension of the asset file
* `bool _FocusAsset = true`: Defines if the asset should be selected and highlighted in the Project view after its creation
* `out Object _CreatedAsset`: The created asset

#### `FindAllAssetsOfType()`

```cs
public static T[] FindAllAssetsOfType<T>()
    where T : Object;
public static Object[] FindAllAssetsOfType(Type _Type);
```

Finds all assets of the given type in the project.

- `<T>`: The type of the asset you want to find.
- `Type _Type`: The type of the asset you want to find.

Returns the cast assets you want to find.

### Path helpers

#### `GetAssetAbsolutePath()`

```cs
public static string GetAssetAbsolutePath(Object _Asset);
public static string GetAssetAbsolutePath(int _InstanceID);
public static string GetAssetAbsolutePath(string _RelativePath);
```

Gets the absolute path to the given asset.

- `Object _Asset`: The asset of which you want to get the absolute path.
- `int _InstanceID`: The InstanceID of the asset which you want the absolute path.
- `string _RelativePath`: The relative path (from  `/Assets` directory) of the asset you want the absolute path

Returns the absolute path, or string.Empty if the asset doesn't exist

#### `IsPathRelativeToCurrentProjectFolder()`

```cs
public static bool IsPathRelativeToCurrentProjectFolder(string _AbsolutePath)
```

Checks if the given path points to a folder of the current Unity project.

#### `GetPathRelativeToCurrentProjectFolder()`

```cs
public static string GetPathRelativeToCurrentProjectFolder(string _AbsolutePath, bool _IncludeAssetsFolder = true)
```

Gets a path relative to this project's Assets folder from a given absolute path. Note that this method will write a log if the given path is not valid.

#### `GetResourcesPath()`

```cs
public static string GetResourcesPath(string _AbsolutePath, string _ExpectedExtension, bool _IncludeResourcesFolder = false)
```

Gets the path to the asset pointed by the given absolute path, from Resources folder of this project.