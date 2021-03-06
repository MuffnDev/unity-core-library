# Muffin Dev for Unity - `GUIStyleExtension`

Extension methods for `GUIStyle` instances.

## Public API

### Methods

#### `RichText()`

```cs
public static GUIStyle RichText(this GUIStyle _Style, bool _Enable)
```

Copies the input `GUIStyle`, and enables/disables rich text mode.

#### `WordWrap()`

```cs
public static GUIStyle WordWrap(this GUIStyle _Style, bool _Enable)
```

Copies the input `GUIStyle`, and enables/disables word wrapping.

#### `StretchWidth()`

```cs
public static GUIStyle StretchWidth(this GUIStyle _Style, bool _Enable)
```

Copies the input `GUIStyle`, and enables/disables width stretching.

#### `StretchHeight()`

```cs
public static GUIStyle StretchHeight(this GUIStyle _Style, bool _Enable)
```

Copies the input `GUIStyle`, and enables/disables height stretching.

#### `FontSize()`

```cs
public static GUIStyle FontSize(this GUIStyle _Style, int _FontSize)
```

Copies the input `GUIStyle`, and sets the given font size.

#### `FontColor()`

```cs
public static GUIStyle FontColor(this GUIStyle _Style, Color _Color)
```

Copies the input `GUIStyle`, and sets the given font color on normal state.

#### `TextAlignment()`

```cs
public static GUIStyle TextAlignment(this GUIStyle _Style, TextAnchor _Alignment
```

Copies the input `GUIStyle`, and sets the text alignment.

#### `FontStyle()`

```cs
public static GUIStyle FontStyle(this GUIStyle _Style, FontStyle _FontStyle)
```

Copies the input `GUIStyle`, and sets the font style.

#### `Margin()`

```cs
public static GUIStyle Margin(this GUIStyle _Style, RectOffset _Offset);
public static GUIStyle Margin(this GUIStyle _Style, int _HorizontalOffset, int _VerticalOffset);
public static GUIStyle Margin(this GUIStyle _Style, int _Left, int _Right, int _Top, int _Bottom);
```

Copies the input GUIStyle, and sets the margins.

#### `Padding()`

```cs
public static GUIStyle Padding(this GUIStyle _Style, RectOffset _Offset);
public static GUIStyle Padding(this GUIStyle _Style, int _HorizontalOffset, int _VerticalOffset);
public static GUIStyle Padding(this GUIStyle _Style, int _Left, int _Right, int _Top, int _Bottom);
```

Copies the input GUIStyle, and sets the padding.