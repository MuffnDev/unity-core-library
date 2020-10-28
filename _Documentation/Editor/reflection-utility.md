# Muffin Dev for Unity - Core - `Reflection Utility`

Contains some utility methods for C# reflection.

## Public API

### Methods

#### `GetAllTypesImplementingGenericType()`

```cs
public static IEnumerable<Type> GetAllTypesImplementingGenericType(Type _GenericType, Assembly _Assembly)
```

Gets all the types that implement the given generic type in the given assembly.

***NOTE**: This method is inspired by this Stack Overflow answer: [https://stackoverflow.com/a/8645519/6699339](https://stackoverflow.com/a/8645519/6699339)*

- `Type _GenericType`: The type of the generic class you want to find inheritors. Uses the *open generic* syntax. As an example, if your class name is `MyGenericClass<T>`, use `typeof(MyGenericClass<>)`, without any value inside the less-than/greater-than characters.
- `Assembly _Assembly`: The assembly where you want to find the given generic type implementations.

Returns an enumerable that contains all the found types.

#### `GetAssemblyByName()`

```cs
public static Assembly GetAssemblyByName(string _AssemblyName)
```

Gets an Assembly with the given name.

- `string _AssemblyName`: The name of the Assembly you want to get.

Returns the found assembly, otherwise `null`.

#### `GetUnityProjectAssembly()`

```cs
public static Assembly GetUnityProjectAssembly()
```

Gets the Unity's CSharp assembly, which is the assembly that contains your custom scripts.

#### `GetUnityEditorProjectAssembly()`

```cs
public static Assembly GetUnityEditorProjectAssembly()
```

Gets the Unity's CSharp Editor assembly, which is the assembly that contains your custom scripts in /Editor directories.

#### `GetProjectAssemblies()`

```cs
public static Assembly[] GetProjectAssemblies()
```

Gets all the project assemblies, ignoring all native .NET assemblies, `UnityEngine` assemblies, `UnityEditor` assemblies, etc.

Returns the found assemblies.