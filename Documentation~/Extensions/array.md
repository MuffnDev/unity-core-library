# Muffin Dev for Unity - `AnimationCurveExtension`

Extensions for arrays.

## Public API

### `IsInRange()`

```cs
public static bool IsInRange(this Array _Array, int _Index)
```

Checks if the given index is in the array's range (so greater than or equal to 0, and less than or equal to the array's length.

- `int _Index`: The index you want to check

Returns `true` if the given index is in the array's range, otherwise `false`.
