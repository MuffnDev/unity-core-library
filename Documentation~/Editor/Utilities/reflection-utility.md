# Muffin Dev for Unity - Core - `Reflection Utility`

Contains some utility methods for C# reflection.

## Public API

### Constants

#### `INSTANCE`

```cs
public const BindingFlags INSTANCE = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
```

Defines bindings flags for getting an item through reflection that is member of an instance.

#### `STATIC`

```cs
public const BindingFlags STATIC = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
```

Defines bindings flags for getting an item through reflection that is static, member of a class.

#### `UNITY_CSHARP_ASSEMBLY_NAME`

```cs
public const string UNITY_CSHARP_ASSEMBLY_NAME = "Assembly-CSharp";
```

#### `UNITY_EDITOR_CSHARP_ASSEMBLY_NAME`

```cs
public const string UNITY_EDITOR_CSHARP_ASSEMBLY_NAME = "Assembly-CSharp-Editor";
```

### Methods

#### `GetAllTypesImplementingGenericType()`

```cs
public static IEnumerable<Type> GetAllTypesAssignableFrom(Type _ParentType, Assembly _Assembly);
public static IEnumerable<Type> GetAllTypesAssignableFrom(Type _ParentType, IEnumerable<Assembly> _Assemblies);
public static IEnumerable<Type> GetAllTypesAssignableFrom(Type _ParentType);
```

Gets all the types that implement the given type in the given assemblies, or in the project assemblies. It also works for generic types.

***NOTE**: This method is inspired by this Stack Overflow answer: [https://stackoverflow.com/a/8645519/6699339](https://stackoverflow.com/a/8645519/6699339)*

- `Type _ParentType`: The type of the class you want to find inheritors. You can pass a generic type in this paramater, by using the "open generic" syntax. As an example, if the parent class is `MyGenericClass<T>`, use `typeof(MyGenericClass<>)`, without any value inside the less-than/greater-than characters.
- `Assembly _Assembly`: The assembly where you want to find the given type implementations.
- `IEnumerable<Assembly> _Assemblies`: The assemblies where you want to find the given type implementations.

Returns an enumerable that contains all the found types.

---

```cs
public static IEnumerable<Type> GetAllTypesAssignableFrom<T>(Assembly _Assembly);
public static IEnumerable<Type> GetAllTypesAssignableFrom<T>(IEnumerable<Assembly> _Assemblies);
public static IEnumerable<Type> GetAllTypesAssignableFrom<T>();
```

Gets all the types that implement the given type in the given assemblies, or in the project assemblies.

***NOTE**: This method is inspired by this Stack Overflow answer: [https://stackoverflow.com/a/8645519/6699339](https://stackoverflow.com/a/8645519/6699339)*

- `<T>`: The type of the class you want to find inheritors.
- `Assembly _Assembly`: The assembly where you want to find the given type implementations.
- `IEnumerable<Assembly> _Assemblies`: The assemblies where you want to find the given type implementations.

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

#### `GetFieldValue()`

```cs
public static TFieldType GetFieldValue<TFieldType>(string _FieldName, object _Target, BindingFlags _BindingFlags = INSTANCE);
public static object GetFieldValue(string _FieldName, object _Target, BindingFlags _BindingFlags = INSTANCE);
```

Gets the value of the named field (member variable) from the given target.

- `TFieldType`: The expected type of the field.
- `string _FieldName`: The name of the field you want to get.
- `object _Target`: The object from which you want to get the field.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

Returns the found field value, or the `default` type value if the field doesn't exist on the target.

#### `SetFieldValue()`

```cs
public static void SetFieldValue<TFieldType>(string _FieldName, TFieldType _Value, object _Target, BindingFlags _BindingFlags = INSTANCE)
```

Sets the value of the named field on the given target.

- `TFieldType`: The type of the field to set.
- `string _FieldName`: The name of the field you want to set.
- `TFieldType _Value`: The value you want to set on the field.
- `object _Target`: The object of which you want to set the field.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

#### `GetPropertyValue()`

```cs
public static TPropertyType GetPropertyValue<TPropertyType>(string _PropertyName, object _Target, BindingFlags _BindingFlags = INSTANCE);
public static object GetPropertyValue(string _PropertyName, object _Target, BindingFlags _BindingFlags = INSTANCE);
```

Gets the value of the named property (the "get" accessor) from the given target.

- `TPropertyType`: The expected type of the property.
- `string _PropertyName`: The name of the property you want to get.
- `object _Target`: The object from which you want to get the property.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

Returns the found property value, or `null` if the property doesn't exist on the target.

#### `SetPropertyValue()`

```cs
public static void SetPropertyValue<TPropertyType>(string _PropertyName, TPropertyType _Value, object _Target, BindingFlags _BindingFlags = INSTANCE);
```

Sets the value of the named property on the given target.

- `TPropertyType`: The type of the property to set.
- `string _PropertyName`: The name of the property you want to set.
- `TPropertyType _Value`: The value you want to set on the property.
- `object _Target`: The object of which you want to set the property.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

#### `GetMethod()`

```cs
public static MethodInfo GetMethod(string _MethodName, object _Target, BindingFlags _BindingFlags = INSTANCE);
```

Gets the named method from the given target.

- `string _MethodName`: The name of the method to get.
- `object _Target`: The object from which you want to get the method.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

Returns the found method, or the `default` return type value if the method doesn't exist on the target.

#### `CallMethod()`

```cs
public static void CallMethod(string _MethodName, object _Target, BindingFlags _BindingFlags = INSTANCE);
public static void CallMethod(string _MethodName, object _Target, object[] _Parameters, BindingFlags _BindingFlags = INSTANCE);
```

Calls the named method from the given target.

- `string _MethodName`: The name of the method to call.
- `object _Target`: The object from which you want to call the method.
- `object[] _Parameters`: The eventual parameters to pass for the method call.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

#### `CallMethod<T>()`

```cs
public static TReturnType CallMethod<TReturnType>(string _MethodName, object _Target, BindingFlags _BindingFlags = INSTANCE);
public static TReturnType CallMethod<TReturnType>(string _MethodName, object _Target, object[] _Parameters, BindingFlags _BindingFlags = INSTANCE)
```

Calls the named method from the given target, and get its returned value.

- `TReturnType`: The expected return value type of the method.
- `string _MethodName`: The name of the method to call.
- `object _Target`: The object from which you want to call the method.
- `object[] _Parameters`: The eventual parameters to pass for the method call.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

Returns the method's returned value, or the `default` return type value if the method doesn't exist on the target.

#### `CallMethod<T>()`

```cs
public static Type GetNestedType(string _NestedTypeName, object _Target, BindingFlags _BindingFlags = BindingFlags.NonPublic | BindingFlags.Public)
```

Gets the named nested type from the given target.

- `string _NestedTypeName`: The name of the nested type you're searching for.
- `object _Target`: The object from which you want to get the nested type.
- `BindingFlags _BindingFlags = INSTANCE`: The `BindingFlags` to use for the reflection operation. You can combine flags by using the *bitwise OR* operator ( `|` ) (e.g. `BindingFlags.Public | BindingFlags.NonPublic`).

Returns the found nested type, or `null` if the nested type doesn't exist on the target.