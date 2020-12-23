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

#### `HORIZONTAL_MARGIN`

```cs
public static readonly float HORIZONTAL_MARGIN = EditorGUIUtility.standardVerticalSpacing
```

Default Muffin Dev' editor tools horizontal margin.

#### `VERTICAL_MARGIN`

```cs
public static readonly float VERTICAL_MARGIN = EditorGUIUtility.standardVerticalSpacing
```

Default Muffin Dev' editor tools vertical margin.

#### `LINE_HEIGHT`

```cs
public static readonly float LINE_HEIGHT = EditorGUIUtility.singleLineHeight
```

Default Muffin Dev' editor tools property line height.

#### `EDITOR_WINDOW_PADDING`

```cs
public const float EDITOR_WINDOW_PADDING = 2f
```

Default Muffin Dev' editor windows padding.

#### `INSPECTOR_FOLDOUT_LEFT_OFFSET`

```cs
public const float INSPECTOR_FOLDOUT_LEFT_OFFSET = 14f
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

### GUI helpers

#### `FloatField()`

```cs
public static float FloatField(Rect _Position, Rect _DragHotZone, float _Value)
```

Makes a text field for entering floats (just as `EditorGUI.FloatField()`), but allows you to define the "drag hot zone" of the control.

#### `HorizontalLine()`

```cs
public static void HorizontalLine(Rect _Rect);
public static void HorizontalLine(Rect _Rect, Color _Color)
```

Draws an horizontal line.

- `Rect _Rect`: The position and size of the line.
- `Color _Color`: The color of the line.

#### `HorizontalLineLayout()`

```cs
public static void HorizontalLineLayout(bool _Wide = false);
public static void HorizontalLineLayout(float _Height, bool _Wide = false);
public static void HorizontalLineLayout(float _Height, Color _Color, bool _Wide = false);
```

Draws an horizontal line using layout GUI.

- `float _Height`: The height of the line.
- `Color _Color`: The color of the line.
- `bool _Wide = false`: If true, the line will have the exact same with as the window where it's drawn.

#### `SearchBar()`

```cs
public static string SearchBar(string _Text, Action _OnCancel = null);
public static string SearchBar(string _Label, string _Text, Action _OnCancel = null);
public static string SearchBar(string _Tex, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null);
public static string SearchBar(string _Label, string _Text, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null);
```

Draws a search bar field using GUI Layout.

- `string _Text`: The content of the search bar
- `Action _OnCancel`: The action to call aftern clicking on the *cancel* button. If `null`, the button won't appear
- `string _Label`: The label of the search bar field
- `GUIStyle _SearchBarStyle`: The `GUIStyle` to apply on the search bar field
- `GUIStyle _CancelButtonStyle`: The `GUIStyle` to apply on the cancel button

#### `SwitchField()`

```cs
public static bool SwitchField(bool _Value);
public static bool SwitchField(string _Label, bool _Value);
```

Draws a "On/Off" switch field.

- `bool _Value`: The current property value.
- `string _Label`: The label of the property.

Returns `true` if *On* is selected, otherwise `false`.

#### `ObjectField()`

```cs
public static void ObjectField<TObjectType>(Rect _Position, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true)
    where TObjectType : Object;
public static void ObjectField(Rect _Position, Type _ObjectType, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true);
```

Draws an `Object` field with a *Create new* button on the right.

- `<TObjectType>`: The type of the object that can be passed to the Object field.
- `Type _ObjectType`: The type of the object that can be passed to the Object field.
- `Rect _Position`: The position and size of the field.
- `GUIContent _Label`: The label to display on the left of the field.
- `SerializedProperty _Property`: The property on which you want to set the field value.
- `string _PanelTitle = null`: The title of the SavePanel utility.
- `bool _AllowSceneObjects = true`: If true, allow user to pass scene object in the object field.

#### `DrawDefaultInspector()`

```cs
public static void DrawDefaultInspector(Object _Asset, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f);
public static void DrawDefaultInspector(SerializedObject _Object, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f);
public static void DrawDefaultInspector(SerializedProperty _Property, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f);
```

Draws the default inspector of the given object.

- `Object _Asset`: The asset of which you want to draw the inspector.
- `bool _IncludeScriptProperty = false`: If enabled, skip the first "Script" property of the asset.
- `float _CustomLabelWidth = -1f`: If more than 0 given, set the Editor's label width for all the object properties.
- `SerializedObject _Object`: The `SerializedObject` of which you want to draw the custom inspector.
- `SerializedProperty _Property`: The `SerializedProperty` of which you want to draw the custom inspector.

### Editor styles helpers

#### `HelpBoxStyle`

```cs
public static GUIStyle HelpBoxStyle { get; }
```

#### `ReorderableListHeaderStyle`

```cs
public static GUIStyle ReorderableListHeaderStyle { get; }
```

Returns the Unity's reorderable lists style.

#### `ReorderableListBoxStyle`

```cs
public static GUIStyle ReorderableListBoxStyle { get; }
```

#### `PropertyFieldButtonStyle`

```cs
public static GUIStyle PropertyFieldButtonStyle { get; }
```