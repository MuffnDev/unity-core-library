# Muffin Dev for Unity - `ExtendedObjectFieldButton`

Represents an additional button for an Extended Object Field element. Mostly used by `EditorHelpers.ExtendedObjectField()`.

[=> See the `ExtendedObjectField()` method in `EditorHelpers` documentation](./editor-helpers.md)

![*Extended Object Field* usage preview](./Images/extended-object-field.png)

## Enums

### `EPosition`

Defines the position of the button in the drawn field.

```cs
public enum EPosition
{
    // This button should be drawn on the left of the field, before the label
    BeforeLabel = 0,
    // This button should be drawn between the label and the input field
    BeforeField = 1,
    // This button should be drawn on the right on the field
    AfterField = 2
}
```

### Constructors

```cs
public ExtendedObjectFieldButton(GUIContent _Content, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(GUIContent _Content, EPosition _Position, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, EPosition _Position, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(string _IconName, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true);
```

- `GUIContent _Content`: The content (icon, tooltip and text) to use for this button.
- `Action _OnClick`: The action to perform when the user clicks on the button.
- `bool _Enabled = true`: Defines if this button is enabled.
- `EPosition _Position`: The expected position of the button when drawing the Extended Object Field.
- `Texture _Icon`: The icon to use for this button.
- `string _Tooltip`: The hovering tooltip to use for this button.
- `string _Text`: The text to use for this button.
- `string _IconName`: The name of the icon to use for this button (from built-in resources).
- `EEditorIcon _IconType`: The type of icon to use for this button.

### Methods

#### `GetContent()`

```cs
public GUIContent GetContent();
```

Creates a `GUIContent` instance that contains all this button's data.