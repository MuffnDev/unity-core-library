# Muffin Dev for Unity - Core - `ObservableVector2Drawer`

Property drawer for `ObservableVector2` properties.

Inherits from [`ObservableDrawer<T>`](./observable-drawer.md).

## Methods

```cs
protected override void OnValueChange(Observable<Vector2> _Observable, SerializedProperty _ValueProperty)
```

Called if value property is changed in the inspector.

* `Observable<Vector2> _Observable`: The Observable shown in the inspector
* `SerializedProperty _ValueProperty`: Property containing the updated value