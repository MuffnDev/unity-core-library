# Muffin Dev for Unity - `StringExtension`

Extension methods for `string` values.

## Public API

### Methods

#### `ToDisplayName()`

```cs
public static string ToDisplayName(this string _String)
```

Formats the string for user display purposes, by adding a space between words using camel case.

#### `IsUnique()`

```cs
public static bool IsUnique(this string _String, string[] _Array)
```

Checks if the given string exists, and exists only once in the given array.