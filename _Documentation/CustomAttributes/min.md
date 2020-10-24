# Muffin Dev for Unity - `MinAttribute`

Locks the value of the property to a given minimum value.

This attribute can be used on these following property types:

- `float`
- `int`
- `Vector2`: locks both x and y to minimum value
- `Vector3`: locks x, y and z to minimum value

***Note**: Unity 2018 added a new `Min` attribute, working only with `int` and `float` values. It can conflicts with Muffin Dev' `Min` attribute, so you can use `Minimum` attribute instead (see below).*

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    // The value can't be less than -1.
    [Min(-1)]
    public float myProperty;
}
```

Since Unity 2018, there's a `Min` attribute in Unity's libraries that can conflicts with MuffinDev' `Min` attribute. So you can use the `Minimum` attribute instead:

```cs
public class AttributeTester : MonoBehaviour
{
    // The value can't be less than 0, for all the axis.
    [Minimum(0)]
    public Vector3 myProperty;
}
```

## Classes

### `MinAttribute`

```cs
public class MinAttribute : PropertyAttribute
```

```cs
public class MinimumAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public MinAttribute(float _Min)
public MinAttribute(int _Min)
```

```cs
public MinimumAttribute(float _Min)
public MinimumAttribute(int _Min)
```

Creates a `MinAttribute` or `MinimumAttribute` instance that locks the value of the target property to given value.

---

#### Accessors

```cs
public float Min { get; }
```

Gets the minimum value.

### `MinDrawer`

```cs
[CustomPropertyDrawer(typeof(MinAttribute))]
[CustomPropertyDrawer(typeof(MinimumAttribute))]
public class MinDrawer : PropertyDrawer
```

Custom property drawer for `MinAttribute` or `MinimumAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Overrides the property's value if necessary, and draws the property field.