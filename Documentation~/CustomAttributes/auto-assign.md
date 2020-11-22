# Muffin Dev for Unity - `AutoAssignAttribute`

Automaticaly get a component to fill this property field.

## Usage

```cs
public class AttributeTester : MonoBehaviour
{
    [AutoAssign]
    public Transform target;

    [AutoAssign(EAutoAssignMethod.GetComponentFromRoot)]
    public Transform targetFromRoot;   
}
```

## Classes

### `AutoAssignAttribute`

```cs
public class AutoAssignAttribute : PropertyAttribute
```

Contains all settings for the custom property drawer (see below).

#### Constructors

```cs
public AutoAssignAttribute()
```

Creates an `AutoAssignAttribute` instance that uses EAutoAssignMethod.GetComponent method.

---

```cs
public AutoAssignAttribute(EAutoAssignMethod _AutoAssignMethod)
```

Creates an `AutoAssignAttribute` instance that uses the given method.

#### Accessors

```cs
public EAutoAssignMethod AutoAssignMethod { get; }
```

Gets the defined component searching method.

### `AutoAssignDrawer`

```cs
[CustomPropertyDrawer(typeof(AutoAssignAttribute))]
public class AutoAssignDrawer : PropertyDrawer
```

Custom property drawer for the `AutoAssignDrawer`.

**This is an Editor class, and should not be called out of the Unity editor.**

#### Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Draws the property field, and assign the `objectReferenceValue` if possible, using the defined method.

## Enumerations

### `EAutoAssignMethod`

```cs
public enum EAutoAssignMethod
{
    GetComponent,
    GetComponentInChildren,
    GetComponentInParent,
    GetComponentFromRoot,
    FindObjectOfType
}
```

Defines the component searching method.

* `GetComponent`: use `GetComponent()`
* `GetComponentInChildren`: use `GetComponentInChildren()`
* `GetComponentInParent`: use `GetComponentInParent()`
* `GetComponentFromRoot`: use `GetComponentFromRoot()` extension
* `FindObjectOfType`: use `GameObject.FindObjectOfType()`