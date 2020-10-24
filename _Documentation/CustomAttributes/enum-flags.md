# Muffin Dev for Unity - `EnumFlagsAttribute`

Display enum field as a `MaskField` in the inspector, which allow user to select multiple values as enum flags

## Usage

```cs
[System.Flags]
public enum TestEnum
{
    Value1 = 1,
    Value2 = 2,
    Value3 = 4
}

public class AttributeTester : MonoBehaviour
{
    [EnumFlags]
    public TestEnum myProperty;
}
```

## About Enum *Flags*

In C#, you can create an enum that defines *flags*, so a variable of that type can have multiple values at the same time.

To do that, first create an enum and set it as a *flags* list using attribute `[System.Flags]`.

```cs
[System.Flags]
public enum TestEnum
{
    A = 1,
    B = 2,
    C = 4,
    D = 8
}
```

Now, if you want to select both items `A` and `C` in a variable, use the binary operator `|`:

```cs
TestEnum myFlagsVariable = TestEnum.A | TestEnum.B;
```

Note that if you want a variable to contain multiple enum flags, you must set each enum item value manually. And each value must be 2 times the value of the previous item. In fact, a variable that have an enum type is an `int`, and combining enum values are like doing bit shifting. To be clear, if you have 3 items in your enum, the values can be 1, 2 and 4. For example, if you select both first and last item, you can translate it as binary number 101. If you select the second and the last one, it outputs 011, etc..

=> [See MSDN documentation about enum flags for more informations](https://docs.microsoft.com/fr-fr/dotnet/api/system.flagsattribute)

## Classes

### `EnumFlagsAttribute`

```cs
public class EnumFlagsAttribute : PropertyAttribute
```

Decorator for an enum with multiple allowed values.

### `EnumFlagsDrawer`

```cs
[CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
public class EnumFlagsDrawer : PropertyDrawer
```

Custom property drawer for the `EnumFlagsAttribute`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws a `MaskField` instead of a regular enum field, allowing user to select multiple values.