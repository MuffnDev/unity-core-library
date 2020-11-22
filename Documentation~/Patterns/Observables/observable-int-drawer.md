# Muffin Dev for Unity - Core - `ObservableIntDrawer`

Property drawer for `ObservableInt` properties.

Inherits from [`ObservableDrawer<T>`](./observable-drawer.md).

## Methods

```cs
protected override void OnValueChange(Observable<int> _Observable, SerializedProperty _ValueProperty)
```

Called if value property is changed in the inspector.

* `Observable<int> _Observable`: The Observable shown in the inspector
* `SerializedProperty _ValueProperty`: Property containing the updated value