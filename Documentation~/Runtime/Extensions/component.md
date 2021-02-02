# Muffin Dev for Unity - `ComponentExtension`

Extensions for `Component` objects.

## Public API

### Methods

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

#### `InitComponentRefs()`

```cs
public static void InitComponentRefs(this Component _Component);
```

Initializes reference to components or `GameObject`s on fields that use the [`[ComponentRef]`](../CustomAttributes/component-ref.md) attribute. Note that if the properties are already initialized, their value is never replaced.

The following example shows you how to use this method in order to initialize all components and `GameObject`s reference at once when a component is initialized:

```cs
using UnityEngine;
using MuffinDev.Core;

public class ComponentRefDemo : MonoBehaviour
{
    [ComponentRef]
    public Transform myTranform;

    [ComponentRef]
    public Rigidbody rb;

    private void Awake()
    {
        this.InitComponentRefs();
    }
}
```

#### `FindComponentRef()`

```cs
public static Object FindComponentRef(Component _Component, Type _PropertyType, EComponentRefScope _Scope = EComponentRefScope.Local, string _RefObjectName = null);
```

Find a component reference depending on the given settings. Used internally by `InitComponentRefs()`.

- `Component _Component`: The component from which you want to get a reference.
- `Type _PropertyType`: The type of the component you want to get. THis can also be GameObject.
- `EComponentRefScope _Scope = EComponentRefScope.Local`: The scope of the research to use for getting the reference.
- `string _RefObjectName = null`: The name of the object on which you want to get the reference.

Returns the found reference, or `null` if it failed.