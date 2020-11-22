# Muffin Dev for Unity - `MonoBehaviourExtension`

Extensions for `MonoBehaviour` objects.

## Methods

```cs
public static T GetComponentFromRoot<T>(this MonoBehaviour _MonoBehaviour)
    where T : Component;
public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, Type _ComponentType);
public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, string _ComponentTypeName);
```

Gets the component of the given type from the root GameObject of the hierarchy.

Returns the found component, otherwise `null`.