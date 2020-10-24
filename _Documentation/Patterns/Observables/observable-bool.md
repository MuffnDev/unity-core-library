# Muffin Dev for Unity - Core - `ObservableBool`

Represents an observable boolean property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev;
public class ObservableBoolExample : MonoBehaviour
{
    public ObservableBool observableBool = new ObservableBool(true);
}
```

![`ObservableBool` view in inspector](./Images/observable-bool-example.jpg)

## Constructors

```cs
public ObservableBool() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableBool(bool _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.