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