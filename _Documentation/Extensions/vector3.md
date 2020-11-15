# Muffin Dev for Unity - `Vector3Extension`

Extension methods for `Vector3` values.

## Public API

### Methods

#### `Min()`

```cs
public static Vector3 Min(this Vector3 _Vector, float _Min)
```

Returns a new `Vector3` instance with its values superior or equal to the given minimum value.

- `this Vector3 _Vector`: The input vector to compute.
- `float _Min`: The minimum value of the given vector.

Returns the computed vector.

#### `Max()`

```cs
public static Vector3 Max(this Vector3 _Vector, float _Min)
```

Returns a new `Vector3` instance with its values inferior or equal to the given maximum value.

- `this Vector3 _Vector`: The input vector to compute.
- `float _Min`: The maximum value of the given vector.

Returns the computed vector.