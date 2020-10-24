# Muffin Dev for Unity - Core - `ObservableStringDrawer`

Property drawer for `ObservableString` properties.

Inherits from [`ObservableDrawer<T>`](./observable-drawer.md).

## Methods

```cs
protected override void OnValueChange(Observable<string> _Observable, SerializedProperty _ValueProperty)
```

Called if value property is changed in the inspector.

* `Observable<string> _Observable`: The Observable shown in the inspector
* `SerializedProperty _ValueProperty`: Property containing the updated value