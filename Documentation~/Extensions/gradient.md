# Muffin Dev for Unity - Core - `GradientExtension`

Extensions for `Gradient` class.

## Public API

### Methods

#### `Clone()`

```cs
public static Gradient Clone(this Gradient _Gradient)
```

Clones the gradient.

- `this Gradient _Gradient`: The gradient to clone.

Returns the cloned `Gradient` instance.

#### `Reverse()`

```cs
public static Gradient Reverse(this Gradient _Gradient)
```

Reverses the gradient color and alpha keys.

- `this Gradient _Gradient`: The original gradient you want to reverse.

Returns a new `Gradient` instance with the reversed keys of the input `Gradient`.