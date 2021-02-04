# Muffin Dev for Unity - `ComponentRefAttribute`

This attribute is meant to be used with the `Component` extension method `InitComponentRefs()`, which automatically get components or `GameObject`s references on a `GameObject`, its children or even globally in the scene. Note that if properties are already set, their value is never replaced.

When used on serialized properties, the reference is automatically initialized in the editor.

Also note that this attribue won't work with lists or arrays for now.

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    [ComponentRef]
    public Transform myTransform;

    private void Awake()
    {
        this.InitComponentRefs();
    }
}
```

## Enums

### `EComponentRefScope`

```cs
[Flags]
public enum EComponentRefScope
{
    Local = 0,
    Children = 1,
    World = 2
}
```

Defines how the reference should be searched.

- `Local`: Gets the reference on the decorated component's GameObject only.
- `Children`: Gets the reference on the decorated component's GameObject or in its children.
- `World`: Gets the reference from an object in the scene.

Since this enumeration uses flags, you can use several behaviors at one:

```cs
[ComponentRef(EComponentRefScope.Local | EComponentRefScope.Children)]
```

## Public API

### Constructors

```cs
public ComponentRefAttribute(bool _AllowChildren = false, bool _AllowScene = false);
```

- `bool _AllowChildren = false`: If enabled, the reference will be searched on the GameObject and in its hierarchy.
- `bool _AllowScene = false`: If enabled, the reference will be searched in the scene (using FindObjectOfType()).

---

```cs
public ComponentRefAttribute(string _ChildName);
```

Using this constructor, the scope is `EComponentRefScope.Local | EComponentRefScope.Children`.

- `string _ChildName`: The name of the child object from which you want to get the reference.

---

```cs
public ComponentRefAttribute(string _ReferenceObjectName, bool _WorldOnly);
```

- `string _ReferenceObjectName`: The name of the object from which you want to get the reference.
- `bool _WorldOnly`: If enabled, the reference will be get from the world, so not from this `GameObject` or one of its child.

---

```cs
public ComponentRefAttribute(EComponentRefScope _Scope);
```

- `EComponentRefScope _Scope`: Defines if you want to get references from this object (Local), its children (Children), in the scene (World), or several scopes using flags (for example `EComponentRefScope.Local | EComponentRefScope.Children`).

---

```cs
public ComponentRefAttribute(string _ReferenceObjectName, EComponentRefScope _Scope);
```

- `string _ReferenceObjectName`: The name of the object from which you want to get the reference.
- `EComponentRefScope _Scope`: Defines if you want to get references from this object (Local), its children (Children), in the scene (World), or several scopes using flags (for example `EComponentRefScope.Local | EComponentRefScope.Children`).