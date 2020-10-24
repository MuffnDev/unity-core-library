# Muffin Dev for Unity - `IndentAttribute`

Indents the property field for the given indentation level.

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    public float myProperty;

    [Indent(1)]
    public float indentedProperty;
}
```

## Classes

### `IndentAttribute`

```cs
public class IndentAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public IndentAttribute()
```

Creates an `IndentAttribute` instance that indents the property for 1 level.

---

```cs
public IndentAttribute(int _IndentLevel)
```

Creates an `IndentAttribute` instance that indents the property for the given level.

#### Accessors

```cs
public int IndentLevel { get; }
```

Gets the indentation level for this property.

### `IndentDrawer`

```cs
[CustomPropertyDrawer(typeof(IndentAttribute))]
public class IndentDrawer : PropertyDrawer
```

Custom property drawer for the `IndentAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws the property field, using the settings of the linked `IndentAttribute` instance.