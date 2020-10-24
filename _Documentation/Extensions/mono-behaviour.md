# Muffin Dev for Unity - `MonoBehaviourExtension`

Extensions for `MonoBehaviour` objects.

## Methods

```cs
public static T GetComponentFromRoot<T>(this MonoBehaviour _MonoBehaviour)
    where T : Component
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.

---

```cs
public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, Type _ComponentType)
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.

---

```cs
public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, string _ComponentName)
```

Gets the component of the named type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.