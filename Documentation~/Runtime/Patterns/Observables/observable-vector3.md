# Muffin Dev for Unity - Core - `ObservableVector3`

Represents an observable `Vector3` property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev.Core;
public class ObservableVector3Example : MonoBehaviour
{
    public ObservableVector3 observableVector3 = new ObservableVector3(Vector3.right);
}
```

![`ObservableVector3` view in inspector](./Images/observable-vector3-example.jpg)

## Constructors

```cs
public ObservableVector3() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableVector3(Vector3 _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.