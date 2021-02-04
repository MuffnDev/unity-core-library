# Muffin Dev for Unity - `AnimCurveAttribute`

Draws a custom AnimationCurve editor in the inspector view, with defined minimum and maximum time and value.

Note that in the AnimationCurve editor, the X axis is "time", and Y axis is "value".

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    // Custom curve editor, with time and value between 0 and 1.
    [AnimCurve]
    public AnimationCurve animCurve;

    // Custom curve editor, with time between 0 and 1, and value between 0 and 2.
    [AnimCurve(1f, 2f)]
    public AnimationCurve animCurve2;

    // Custom curve editor, with time between -1 and 1, and value between 0 and 2.
    [AnimCurve(-1f, 1f, 0f, 2f)]
    public AnimationCurve animCurve3;
}
```

## Classes

### `AnimCurveAttribute`

```cs
public class AnimCurveAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public AnimCurveAttribute()
```

Creates an AnimCurveAttribute instance with time and value clamped between 0 and 1.

---

```cs
public AnimCurveAttribute(EColor _CurveColor)
```

Creates an AnimCurveAttribute instance with the given color and no bounds.

---

```cs
public AnimCurveAttribute(float _MaxTime, float _MaxValue)
```

Creates an AnimCurveAttribute instance with time clamped between 0 and the given max time, and value clamped between 0 and the given max value.

---

```cs
public AnimCurveAttribute(float _MaxTime, float _MaxValue, EColor _CurveColor)
```

Creates an AnimCurveAttribute instance with time clamped between 0 and the given max time, and value clamped between 0 and the given max value. Sets the curve color using the given `EColor` value.

---

```cs
public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue)
```

Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min and max value.

---

```cs
public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue, EColor _CurveColor)
```

Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min and max value. Sets the curve color using the given `EColor` value.

---

```cs
public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue, float _ColorR, float _ColorG, float _ColorB)
```

Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min and max value. Sets the curve color using the given color components:

* `float _ColorR`: Red component of the curve's color (must be between 0 and 1)
* `float _ColorG`: Green component of the curve's color (must be between 0 and 1)
* `float _ColorB`: Blue component of the curve's color (must be between 0 and 1)

#### Accessors

```cs
public float MinTime { get; }
```

Gets the min time (X axis) of the AnimationCurve editor.

---

```cs
public float MaxTime { get; }
```

Gets the max time (X axis) of the AnimationCurve editor.

---

```cs
public float MinValue { get; }
```

Gets the min value (Y axis) of the AnimationCurve editor.

---

```cs
public float MaxValue { get; }
```

Gets the max value (Y axis) of the AnimationCurve editor.

---

```cs
public Color GetCurveColor(EColor _Color)
```

Gets the `Color` instance relative to the given `EColor` value.

---

```cs
public Rect GetRanges()
```

Gets the `Rect` instance that should be used for drawing the AnimationCurve editor, based on the attribute values:

- x is min time
- y is min value
- width is max time
- height is max value

### `AnimCurveDrawer`

```cs
[CustomPropertyDrawer(typeof(AnimCurveAttribute))]
public class AnimCurveDrawer : PropertyDrawer
```

Custom property drawer for the `AnimCurveAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws the AnimationCurve editor for the given property, using the linked `AnimCurveAttribute` values.

## Colors

[=> See `Colors` and `EColor` documentation](../Others/colors.md)