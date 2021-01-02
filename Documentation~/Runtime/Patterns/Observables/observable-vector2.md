# Muffin Dev for Unity - Core - `ObservableVector2`

Represents an observable `Vector2` property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev.Core;
public class ObservableVector2Example : MonoBehaviour
{
    public ObservableVector2 observableVector2 = new ObservableVector2(Vector2.right);
}
```

![`ObservableVector2` view in inspector](./Images/observable-vector2-example.jpg)

## Constructors

```cs
public ObservableVector2() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableVector2(Vector2 _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.