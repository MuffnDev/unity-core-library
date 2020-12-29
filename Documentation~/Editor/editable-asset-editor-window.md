# Muffin Dev for Unity - `EditableAssetEditorWindow`

Shortcut for making editable assets editor windows, which updates the GUI depending on the selection.

[=> See `EditableAssetEditorGUI` documentation for usage examples](./ediable-asset-editor-gui.md)

## Structure

```cs
public abstract class EditableAssetEditorWindow<TAsset, TEditorGUI> : EditorWindow
    where TAsset : ScriptableObject
    where TEditorGUI : EditableAssetEditorGUI<TAsset>, new()
```

- `<TAsset>`: The type of the editable assets.
- `<TEditorGUI>`: The type of the `EditableAssetEditorGUI` that represents GUI for the given editable asset type.

## Protected API

### Methods

#### `DrawDefaultToolbar()`

```cs
protected void DrawDefaultToolbar()
```

Draws a toolbar, which contains a "Back" button that goes back to the assets list.

### Accessors

#### `AssetGUI`

```cs
protected TEditorGUI AssetGUI { get; }
```

Gets the editor GUI to draw in this window.

#### `AssetGUIScrollPosition`

```cs
protected Vector2 AssetGUIScrollPosition { get; }
```

Gets the scroll position of the editor GUI.