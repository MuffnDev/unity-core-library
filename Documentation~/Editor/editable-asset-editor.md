# Muffin Dev for Unity - `EditableAssetEditor`

Shortcut for making editable assets custom inspector editor.

[=> See `EditableAssetEditorGUI` documentation for usage examples](./ediable-asset-editor-gui.md)

## Structure

```cs
public class EditableAssetEditor<TAsset, TEditorGUI> : TEditor<TAsset>
    where TAsset : ScriptableObject
    where TEditorGUI : EditableAssetEditorGUI<TAsset>, new()
```

- `<TAsset>`: The type of the editable assets.
- `<TEditorGUI>`: The type of the `EditableAssetEditorGUI` that represents GUI for the given editable asset type.

## Protected API

### Methods

#### `AfterInitEditorGUI()`

```cs
protected virtual void AfterInitEditorGUI()
```

Called after the Editor GUI has been created. Override this method to customize the editor GUI settings.

### Accessors

#### `AssetGUI`

```cs
protected TEditorGUI AssetGUI { get; }
```

Gets the editor GUI for the inspected asset.