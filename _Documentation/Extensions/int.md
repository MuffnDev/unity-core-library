# Muffin Dev for Unity - `IntExtension`

Extension methods for `int` values.

## Methods

```cs
public static string AddLeading0(this int _Number, int _Pow)
```

Adds leading 0 before the given number.

**Note that it works only with positive numbers.** If you give a negative number, its absolute value will be used.

* `int _Number`: The number you want to add leading 0.
* `int _Pow`: Number of 0 to eventually add.

Examples:

```cs
private void Test()
{
    Debug.Log(12.AddLeading0(1)); // Logs 12
    Debug.Log(12.AddLeading0(2)); // Logs 12
    Debug.Log(12.AddLeading0(4)); // Logs 0012
}
```

---

```cs
public static int CompareTo(this int _Number, int _Other, bool _Desc)
```

Compares a number to another.

- `this int _Number`: The number to compare
- `int _Other`: The number to compare with
- `bool _Desc`: If true, compares the number in a descending order

Returns -1 if the number should be placed before the other, 1 if it should be placed after, or 0 if the numbers are even.