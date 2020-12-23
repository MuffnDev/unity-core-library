# Muffin Dev for Unity - `ComponentExtension`

Extensions for `Component` objects.

## Public API

### Methods

#### `GetComponentFromRoot()`

```cs
public static T GetComponentFromRoot<T>(this Component _Obj)
    where T : Component;
public static Component GetComponentFromRoot(this Component _Obj, Type _ComponentType);
public static Component GetComponentFromRoot(this Component _Obj, string _ComponentTypeName);
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.

#### `CopyTo<T>()`

```cs
public static void CopyTo<T>(this T _Original, T _Target, bool _IgnoreTargetProperties, params string[] _TargetProperties)
    where T : Component;
public static void CopyTo<T>(this T _Original, T _Target, string[] _TargetProperties = null, bool _IgnoreTargetProperties = false)
    where T : Component;
```

Copy the values of the original component into the given target one.

- `<T>`: The type of the component to copy.
- `this T _Original`: The original component.
- `T _Target`: The target component, on which to set the original values.
- `bool _IgnoreTargetProperties`: If `true`, all properties and fields will be copied ***but*** the target properties list.
- `params string[] _TargetProperties`: Defines the names of the properties or fields you want to copy.
- `string[] _TargetProperties = null`: Defines the names of the properties or fields you want to copy.

#### `CopyTo()`

```cs
public static void CopyTo(this Component _Original, Component _Target, bool _IgnoreTargetProperties, params string[] _TargetProperties);
public static void CopyTo(this Component _Original, Component _Target, string[] _TargetProperties = null, bool _IgnoreTargetProperties = false);
```

Copy the values of the original component into the given target one. Note that if the types doesn't match, only the matching properties will be copied.

- `this Component _Original`: The original component.
- `Component _Target`: The target component, on which to set the original values.
- `bool _IgnoreTargetProperties`: If `true`, all properties and fields will be copied ***but*** the target properties list.
- `params string[] _TargetProperties`: Defines the names of the properties or fields you want to copy.
- `string[] _TargetProperties = null`: Defines the names of the properties or fields you want to copy.