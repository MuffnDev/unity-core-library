# Muffin Dev for Unity - `MaxAttribute`

Locks the value of the property to a given maximum value.

This attribute can be used on these following property types:

- `float`
- `int`
- `Vector2`: locks both x and y to maximum value
- `Vector3`: locks x, y and z to maximum value

***Note**: Unity 2018 added a new `Min` attribute, working only with `int` and `float` values. It can conflicts with Muffin Dev' `Min` attribute, so you can use `Minimum` attribute instead (see below). We reproduced that scheme by creating `Maximum` attribute, to avoid incoherences.*

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    // The value can't be more than 10.
    [Max(10f)]
    public float myProperty;
}
```

```cs
public class AttributeTester : MonoBehaviour
{
    // The value can't be more than 0, for all the axis.
    [Maximum(0)]
    public Vector3 myProperty;
}
```

## Classes

### `MaxAttribute`

```cs
public class MaxAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public MaxAttribute(float _Max)
public MaxAttribute(int _Max)
```

```cs
public MaximumAttribute(float _Max)
public MaximumAttribute(int _Max)
```

Creates a `MaxAttribute` or `MaximumAttribute` instance that locks the value of the target property to given value.

---

#### Accessors

```cs
public float Max { get; }
```

Gets the maximum value.

### `MaxDrawer`

```cs
[CustomPropertyDrawer(typeof(MaxAttribute))]
[CustomPropertyDrawer(typeof(MaximumAttribute))]
public class MaxDrawer : PropertyDrawer
```

Custom property drawer for `MaxAttribute` or `MaximumAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Overrides the property's value if necessary, and draws the property field.