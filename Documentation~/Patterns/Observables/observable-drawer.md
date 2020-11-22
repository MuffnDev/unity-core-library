# Muffin Dev for Unity - Core - `ObservableDrawer<T>` class

Base class for making a PropertyDrawer for an serialized Observable property.

Note that this custom property drawer uses the name of the property to display to get them, using `SerializedProperty`. So the value and the change event of your target observable class must be serialized.

This drawer is more easier to use if your target observable inherits from `ObservableSerialized<T, U>`, which already implement serialized value and event.

[=> See `ObservableSerialized<T, U>` for more informations](./observable-serialized.md)

## Structure

```cs
public abstract class ObservableDrawer<T> : PropertyDrawer
```

Base class for making a PropertyDrawer for an serialized Observable property.

* `T`: Type of the observed property.

## Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws the inspector field for the given property.

---

```cs
protected abstract void OnValueChange(Observable<T> _Observable, SerializedProperty _ValueProperty)
```

Called if value property is changed in the inspector.

* `Observable<T> _Observable`: The Observable shown in the inspector
* `SerializedProperty _ValueProperty`: Property containing the updated value

---

```cs
public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
```

Gets the Observable property height.

## Accessors

```cs
protected virtual string ValuePropertyName { get; }
```

Gets the value property name of the Observable. By default, it returns "`m_Value`", which matches with the `ObservableSerialized<T, U>` class value property.

---

```cs
protected virtual string EventPropertyName { get; }
```

Gets the event property name of the Observable. By default, it returns "`m_OnChange`", which matches with the `ObservableSerialized<T, U>` class event property.

---

```cs
protected virtual bool CanExpand { get; }
```

Checks if the property can be expanded in the editor. By default, returns `false`.

For example, an integer can't expand because it does not have child property. On the other hand, an array can expand, because it contains as many child as indexes.