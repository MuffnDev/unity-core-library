# Muffin Dev for Unity - `EditorIcons`

Utility class to get and draw editor icons.

In the editor, you can use `EditorGUIUtility.IconContent()` to get a built-in icon, and draw it as the content of a field for example. But built-in icons are queried by names, so you need to know how *Unity Technologies* teams named the icons before getting one of them.

This utility class is there to simplify this for you, by providing a method to query an icon from a selection of built-in ones (see the `EEditorIcon` enum below), so you don't have to know the exact name of the icons by heart.

You can see that selection in the editor, in `Tools > Muffin Dev > Demos > Editor Icons List`.

![Preview of the *Editor Icons Editor Window*](./Images/editor-icons.png)

[=> See the complete Unity built-in icons list from *Unity List*](https://unitylist.com/p/5c3/Unity-editor-icons).

## Enums

### `EEditorIcon`

This enum contains all available editor icon types, which you can use in `EditorIcons.GetIcon()`.

```cs
public enum EEditorIcon
{
    // Items
    Add,
    AddDropdown,
    Remove,

    // Windows
    Select,
    Focus,
    Open,
    WindowOpen,
    Close,
    WindowClose,
    Minimize,
    WindowMaximize,
    Maximize,
    WindowMinimize,

    // Files
    File,
    Folder,
    FolderOpened,
    Import,
    Refresh,

    // Statement
    Fold,
    Unfold,
    Lock,
    Unlock,
    
    // Menus & options
    Options,
    MoreOptions,
    Settings,
    Tools,
    Infos,
    Help,
    Search,
    VerticalMenu,
    HorizontalMenu,
    BurgerMenu,

    // Directions
    Left,
    Right,
    Up,
    Down,
    Previous,
    Next,
    Play,
    Pause,
    Stop,

    // Log
    Log,
    Warning,
    Error,

    // Others
    Unity,
    Paint,
    Pencil,
    Screen,
    View,
    Cloud,
    Favorite,
    Label,
    Checkmark,
    Shapes,
    Snapshot,
    Link,
    Unlink,
    Camera,
}
```

## Public API

### Methods

#### `FindIcon()`

```cs
public static Texture FindIcon(string _IconName);
public static Texture FindIcon(EEditorIcon _IconType);
```

Gets an editor icon by name (from built-in resources and /Editor Default Resources/Icons directory), or by its type.

- `string _IconName`: The name of the icon you want to get ([see Unity built-in icons list on *Unity List*](https://unitylist.com/p/5c3/Unity-editor-icons)).
- `EEditorIcon _IconType`: The type of the icon you want to get (see the `EEditorIcon` enum above).

#### `IconContent()`

```cs
public static GUIContent IconContent(string _IconName, string _Tooltip);
public static GUIContent IconContent(string _IconName, string _Text, string _Tooltip);
public static GUIContent IconContent(EEditorIcon _IconType, string _Tooltip);
public static GUIContent IconContent(EEditorIcon _IconType, string _Text, string _Tooltip);
public static GUIContent IconContent(Texture _Icon, string _Tooltip);
public static GUIContent IconContent(Texture _Icon, string _Text, string _Tooltip);
```

Creates a GUIContent instance that contains the given icon.

- `string _IconName`: The name of the icon you want to get ([see Unity built-in icons list on *Unity List*](https://unitylist.com/p/5c3/Unity-editor-icons)).
- `string _Text = null`: The optional text of the given content.
- `string _Tooltip = null`: The optional hovering tooltip of your content.
- `EEditorIcon _IconType`: The type of the icon you want to get (see the `EEditorIcon` enum above).
- `Texture _Icon`: The icon to use for this content.

### Accessors

#### `LoadingSpinnerIcons`

```cs
public static Texture[] LoadingSpinnerIcons { get; }
```

Gets an array containing all the icons to animate a loading spinner.