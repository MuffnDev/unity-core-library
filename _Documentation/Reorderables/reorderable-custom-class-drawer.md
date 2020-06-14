# Muffin Dev for Unity - Core - `ReorderableCustomClassDrawer` class

Base class for making a custom property drawer for simple serialized classes reorderables.

Inherits from [`ReoderableDrawer`](./reorderable-drawer.md).

[=> See main Reorderables documentation](./README.md) for a usage example

For classes that have a custom property drawer, prefer inheriting from [`ReorderableDrawer`](./reorderable-drawer.md) directly.

## Methods

```cs
protected override void OnDrawItem(Rect _Rect, int _Index, bool _IsActive, bool _IsFocused)
```

Called when an item is drawn in the reorderable list.

* `Rect _Rect`: Position and size of the drawn item
* `int _Index`: Index of the current item
* `bool _IsActive`: Is this item selected and active in inspector view?
* `bool _IsFocused`: Is this item selected in inspector view?