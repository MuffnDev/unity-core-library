# Muffin Dev for Unity - `ColorExtension`

Extension methods for `Camera` instances.

## Public API

### Methods

#### `ToHexRGB()`

```cs
public static string ToHexRGB(this Color _Color)
```

Converts the given Color into an hexadecimal value as string, and returns it with format "RRGGBB".

#### `ToHexRGBA()`

```cs
public static string ToHexRGBA(this Color _Color)
```

Converts the given Color into an hexadecimal value as string, and returns it with format "RRGGBBAA".

#### `FromHex()`

```cs
public static Color FromHex(string _HexadecimalString);
public static Color FromHex(this Color _Color, string _HexadecimalString);
```

Gets a `Color` value from an hexadecimal string.

- `string _HexadecimalString`: The hexadecimal string (like F9F9F9, F9F9F9F9, #F9F9F9, #F9F9F9F9).