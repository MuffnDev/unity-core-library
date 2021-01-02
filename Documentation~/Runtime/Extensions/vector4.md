# Muffin Dev for Unity - `Vector4Extension`

Extension methods for `Vector4` values.

## Public API

### Methods

#### `Min()`

```cs
public static Vector4 Min(this Vector4 _Vector, float _Min)
```

Returns a new `Vector4` instance with its values superior or equal to the given minimum value.

- `this Vector4 _Vector`: The input vector to compute.
- `float _Min`: The minimum value of the given vector.

Returns the computed vector.

#### `Max()`

```cs
public static Vector4 Max(this Vector4 _Vector, float _Min)
```

Returns a new `Vector4` instance with its values inferior or equal to the given maximum value.

- `this Vector4 _Vector`: The input vector to compute.
- `float _Min`: The maximum value of the given vector.

Returns the computed vector.