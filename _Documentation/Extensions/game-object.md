# Muffin Dev for Unity - `GameObjectExtension`

Extensions for `GameObject` objects.

## Methods

```cs
public static T GetComponentFromRoot<T>(this GameObject _Obj)
    where T : Component
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.

---

```cs
public static Component GetComponentFromRoot(this GameObject _Obj, Type _ComponentType)
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.

---

```cs
public static Component GetComponentFromRoot(this GameObject _Obj, string _ComponentName)
```

Gets the component of the named type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.