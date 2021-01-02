# Muffin Dev for Unity - `MuffinDevGUI`

Bundle of methods for custom GUI fields.

## Public API

### Constants

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

#### `BackButton()`

```cs
public static bool BackButton(string _Label = null, string _Tooltip = null, float _Width = 64f, GUIStyle _ButtonStyle = null);
public static bool BackButton(Rect _Rect, string _Label = null, string _Tooltip = null, GUIStyle _ButtonStyle = null, float _Width = 64f);
```

Draws a "Back" button.

- `string _Label = null`: The content of the button.
- `string _Tooltip = null`: The tooltip of the button.
- `float _Width = 64f`: The optional width of the button.
- `GUIStyle _ButtonStyle = null`: The GUIStyle of the button.
- `Rect _Rect`: The Rect container of the button.

Returns `true` if the button has been clicked this frame, otherwise `false`.

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

#### `PaginationBar()`

```cs
public static Pagination PaginationBar(int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
public static Pagination PaginationBar<T>(IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
public static Pagination PaginationBar<T>(Rect _Rect, IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
public static Pagination PaginationBar(Rect _Rect, int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
```

Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.

![`PaginationBar` preview](./Images/pagination-bar.png)

- `<T>`: The type of the elements in the list.
- `int _NbElements`: The total number of elements in your paginated list.
- `int _Page`: The current page.
- `int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE`: The number of elements to display per page.
- `IList<T> _List`: The list that is paginated.
- `Rect _Rect`: The position and size of the element to draw.

Returns the pagination data.

#### `FolderPathField()`

```cs
public static void FolderPathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true);
public static string FolderPathField(string _Path, bool _Editable = false, bool _InProject = true);
public static string FolderPathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true);
public static void FolderPathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true);
public static string FolderPathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true);
public static string FolderPathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true);
```

Draws a text field with a *Browse...* button which allow the user to select a directory.

![Preview of `FolderPathField()` and `FilePathField()`](./Images/path-fields.png)

- `SerializedProperty _Property`: The string property to set.
- `bool _Editable = false`: If `true`, the text field of the path can be edited manually. Otherwise, the path can only be set using the *Browse...* button.
- `bool _InProject = true`: If `true`, forces the path to target a directory inside the current project.
- `string _Path`: The current path value.
- `Rect _Rect`: The position and size of the field to draw.

Returns the selected path.

#### `FilePathField()`

```cs
public static void FilePathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true);
public static string FilePathField(string _Path, bool _Editable = false, bool _InProject = true);
public static string FilePathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true);
public static void FilePathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true);
public static string FilePathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true);
public static string FilePathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true);
```

Draws a text field with a *Browse...* button which allow the user to select a file.

![Preview of `FolderPathField()` and `FilePathField()`](./Images/path-fields.png)

- `SerializedProperty _Property`: The string property to set.
- `bool _Editable = false`: If `true`, the text field of the path can be edited manually. Otherwise, the path can only be set using the *Browse...* button.
- `bool _InProject = true`: If `true`, forces the path to target a file inside the current project.
- `string _Path`: The current path value.
- `Rect _Rect`: The position and size of the field to draw.

Returns the selected path.

#### `ExtendedObjectField()`

```cs
public static T ExtendedObjectField<T>(T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
    where T : Object;
public static T ExtendedObjectField<T>(string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
    where T : Object;
public static T ExtendedObjectField<T>(Rect _Rect, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
    where T : Object;
public static T ExtendedObjectField<T>(Rect _Rect, string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
    where T : Object;
public static Object ExtendedObjectField(Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static Object ExtendedObjectField(string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static Object ExtendedObjectField(Rect _Rect, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static Object ExtendedObjectField(Rect _Rect, string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static void ExtendedObjectField(SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static void ExtendedObjectField(SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons);
```

![Preview of `ExtendedObjectField()` usage](./Images/extended-object-field.png)

Draws an Object Field with additional controls.

See demo usage in `Tools > Muffin Dev > Demos > Extended Object Field`.

- `<T>`: The type of object that can be selected.
- `T _Object`: The currently selected object.
- `bool _AllowSceneObjects`: If `true`, allow user to select scene objects. Otherwise, only assets can be selected.
- `ExtendedObjectFieldButton[] _Buttons`: The list of all controls you want to add to this field.
- `string _Label`: The label of the field to draw.
- `Rect _Rect`: The position and size of the field to draw.
- `Object _Object`: The currently selected object.
- `Type _ObjectType`: The type of object that can be selected.
- `SerializedProperty _Property`: The serialized property to use and assign that contains the selected object data.

[=> See `ExtendedObjectFieldButton` documentation](./extended-object-field-button.md)

##### Usage example

This script will draw a *Focus* button next to any property field that contains a `GameObject`:

```cs
using UnityEngine;
using MuffinDev.Core.EditorOnly;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ExtendedObjectFieldExample : MonoBehaviour
{
    public GameObject prefab;
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(GameObject))]
public class ExtendedObjectFieldExamplePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MuffinDevGUI.ExtendedObjectField(position, property, typeof(GameObject), false, new ExtendedObjectFieldButton[]
        {
            new ExtendedObjectFieldButton(EEditorIcon.Focus, () =>
            {
                Object prefab = property.objectReferenceValue;
                if(prefab != null)
                    MuffinDevGUI.FocusAsset(prefab, false, true);
            }, property.objectReferenceValue != null)
        });
    }
}
#endif
```

Place the `ExtendedObjectFieldExample` component on an object in your scene to see the result.

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