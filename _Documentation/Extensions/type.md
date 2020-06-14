# Muffin Dev for Unity - `IntExtension`

Extension methods for `Type`.

## Methods

```cs
public static string GetFullNameWithAssembly(this Type _Type)
```

Gets the full type name string, including the assmebly name. The output string looks like: `"TypeNamespace.TypeName, AssemblyName, Version=0.0.0.0, Culture=neutral, PublicKeyKoken=null"`.

- `this Type _Type`: The type you wan to get the full name string

Returns the computed full name string, or null if the given type is null.