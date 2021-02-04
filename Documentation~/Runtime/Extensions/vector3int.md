# Muffin Dev for Unity - `Vector3IntExtension`

Extension methods for `Vector3Int` values.

## Public API

### Methods

#### `Min()`

```cs
public static Vector3Int Min(this Vector3Int _Vector, float _Min)
```

Returns a new `Vector3Int` instance with its values superior or equal to the given minimum value.

- `this Vector3Int _Vector`: The input vector to compute.
- `float _Min`: The minimum value of the given vector.

Returns the computed vector.

#### `Max()`

```cs
public static Vector3Int Max(this Vector3Int _Vector, float _Min)
```

Returns a new `Vector3Int` instance with its values inferior or equal to the given maximum value.

- `this Vector3Int _Vector`: The input vector to compute.
- `float _Min`: The maximum value of the given vector.

Returns the computed vector.