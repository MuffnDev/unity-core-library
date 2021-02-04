# Muffin Dev for Unity - `Vector2Extension`

Extension methods for `Vector2` values.

## Public API

### Methods

#### `Min()`

```cs
public static Vector2 Min(this Vector2 _Vector, float _Min)
```

Returns a new `Vector2` instance with its values superior or equal to the given minimum value.

- `this Vector2 _Vector`: The input vector to compute.
- `float _Min`: The minimum value of the given vector.

Returns the computed vector.

#### `Max()`

```cs
public static Vector2 Max(this Vector2 _Vector, float _Min)
```

Returns a new `Vector2` instance with its values inferior or equal to the given maximum value.

- `this Vector2 _Vector`: The input vector to compute.
- `float _Min`: The maximum value of the given vector.

Returns the computed vector.