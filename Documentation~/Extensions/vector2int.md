# Muffin Dev for Unity - `Vector2IntExtension`

Extension methods for `Vector2Int` values.

## Public API

### Methods

#### `Min()`

```cs
public static Vector2Int Min(this Vector2Int _Vector, float _Min)
```

Returns a new `Vector2Int` instance with its values superior or equal to the given minimum value.

- `this Vector2Int _Vector`: The input vector to compute.
- `float _Min`: The minimum value of the given vector.

Returns the computed vector.

#### `Max()`

```cs
public static Vector2Int Max(this Vector2Int _Vector, float _Min)
```

Returns a new `Vector2Int` instance with its values inferior or equal to the given maximum value.

- `this Vector2Int _Vector`: The input vector to compute.
- `float _Min`: The maximum value of the given vector.

Returns the computed vector.