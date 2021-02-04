# Muffin Dev for Unity - `TypeExtension`

Extension methods for `Type` objects.

## Public API

### Methods

#### `GetFullNameWithAssembly()`

```cs
public static string GetFullNameWithAssembly(this Type _Type)
```

Gets the full type name string, including the assmebly name. The output string looks like: `"TypeNamespace.TypeName, AssemblyName, Version=0.0.0.0, Culture=neutral, PublicKeyKoken=null"`.

- `this Type _Type`: The type you wan to get the full name string

Returns the computed full name string, or null if the given type is null.

#### `IsReallyPrimitive()`

```cs
public static bool IsReallyPrimitive(this Type _Type);
```

Checks if the type is "really" a primitive. This method is meant to replace the `Type.IsPrimitive` property, which will return `false` even if the type is a `string`, `decimal`, `long`, `ulong`, `short` or `ushort`.

Returns `true` if the type is "really" a primitive, otherwise `false`.