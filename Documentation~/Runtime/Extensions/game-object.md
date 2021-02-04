# Muffin Dev for Unity - `GameObjectExtension`

Extensions for `GameObject` objects.

## Methods

### Public API

#### `GetComponentInHierarchy()`

```cs
public static T GetComponentInHierarchy<T>(this Component _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
    where T : Component;
public static Component GetComponentInHierarchy(this Component _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false);
```

Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as `GetComponentsInChildren()` would do.

- `<T>`: The type of the component you want to get.
- `bool _IncludeSelf = true`: If enabled, try get component on the source object.
- `bool _IncludeInactive = false`: If enabled, try get component on disabled objects.
- `Type _ComponentType`: The type of the component you want to get.

Returns the first component of the given type found in the hierarchy.

#### `GetComponentsInHierarchy()`

```cs
public static T GetComponentsInHierarchy<T>(this Component _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
    where T : Component;
public static Component GetComponentsInHierarchy(this Component _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false);
```

Gets all components in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as `GetComponentsInChildren()` would do.

- `<T>`: The type of the components you want to get.
- `bool _IncludeSelf = true`: If enabled, try get components on the source object.
- `bool _IncludeInactive = false`: If enabled, try get components on disabled objects.
- `Type _ComponentType`: The type of the components you want to get.

Returns all the components of the given type found in the hierarchy.

#### `Find()`

```cs
public static GameObject Find(this GameObject _Obj, string _Name, bool _IncludeSelf = true, bool _IncludeInactive = false);
```

Find an object by name, recursively in the source's hierarchy.

- `string _Name`: The name of the object you want to find.
- `bool _IncludeSelf = true`: If enabled, the input object is included in the research.
- `bool _IncludeInactive = false`: If enabled, this method will also search if the name of disabled objects match.

Returns the found object, or `null` if the source doesn't contain any object with the given name in its hierarchy.