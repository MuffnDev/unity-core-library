# Muffin Dev for Unity - Core - `ObservableFloatDrawer`

Property drawer for `ObservableFloat` properties.

Inherits from [`ObservableDrawer<T>`](./observable-drawer.md).

## Methods

```cs
protected override void OnValueChange(Observable<float> _Observable, SerializedProperty _ValueProperty)
```

Called if value property is changed in the inspector.

* `Observable<float> _Observable`: The Observable shown in the inspector
* `SerializedProperty _ValueProperty`: Property containing the updated value