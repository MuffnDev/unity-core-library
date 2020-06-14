# Muffin Dev for Unity - Core - *ReorderableClassesDrawer* script

Implementations of [`ReorderableDrawer`](./reorderable-drawer.md) for the ready-to-use reorderable types (see [*ReorderableClasses* script](./reorderable-classes) for more informations).

## Structure

```cs
public sealed class ReorderableClassesDrawer : ReorderableDrawer { }
```

## Custom Property Drawer

This class is a custom property drawer for:

- `ReorderableBoolArray` and `ReorderableBoolList`: reorderable `bool` items
- `ReorderableFloatArray` and `ReorderableFloatList`: reorderable `float` items
- `ReorderableIntArray` and `ReorderableIntList`: reorderable `int` items
- `ReorderableStringArray` and `ReorderableStringList`: reorderable `string` items

- `ReorderableVector2Array` and `ReorderableVector2List`: reorderable `Vector2` items
- `ReorderableVector3Array` and `ReorderableVector3List`: reorderable `Vector3` items
- `ReorderableGameObjectArray` and `ReorderableGameObjectList`: reorderable `GameObject` items
- `ReorderableSpriteArray` and `ReorderableSpriteList`: reorderable `Sprite` items