# Muffin Dev for Unity - `IfCheckedAttribute`

Enables or disables a property field, depending of the value of another given `bool` serialized property.

**Note that the named property to depend must be a serialized boolean property.**

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    public bool enableField = true;
    
    // In this example, myField property is enabled only if "enableField" property is set to true.
    [IfChecked("enableField")]
    public float myProperty;
}
```

## Classes

### `IfCheckedAttribute`

```cs
public class IfCheckedAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public IfCheckedAttribute(string _PropertyName, bool _EnableIfChecked = true)
```

Creates an `IfCheckedAttribute` instance with the given settings:

* `string _PropertyName`: The name of the serialized property that enables this field.
* `bool _EnableIfChecked = true`: If this parameter is set to `false`, the inverse behavior occurs: the field is enabled only if the depending property is set to `false`.

#### Accessors

```cs
public string PropertyName { get; }
```

Gets the name of the property this field depends on.

---

```cs
public bool EnabledIfChecked { get; }
```

Checks if the user wants the inverse behavior, so enable the field if the depending property is set to `false`.

### `IfCheckedDrawer`

```cs
[CustomPropertyDrawer(typeof(IfCheckedAttribute))]
public class IfCheckedDrawer : PropertyDrawer
```

Custom property drawer for the `IfCheckedAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws the property field, enabled or disabled depending on the settings of the linked `IfCheckedAttribute` instance.