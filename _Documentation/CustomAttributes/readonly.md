# Muffin Dev for Unity - `ReadonlyAttribute`

Disables the field of a serialized property.

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    [Readonly]
    public float myProperty;
}
```

## Classes

### `ReadonlyAttribute`

```cs
public class ReadonlyAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public ReadonlyAttribute()
```

Creates a `ReadonlyAttribute` instance.

---

### `ReadonlyDrawer`

```cs
[CustomPropertyDrawer(typeof(ReadonlyAttribute))]
public class ReadonlyDrawer : PropertyDrawer
```

Custom property drawer for the `ReadonlyAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Overrides the property's value if necessary, and draws the property field.