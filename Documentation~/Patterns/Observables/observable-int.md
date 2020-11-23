# Muffin Dev for Unity - Core - `ObservableInt`

Represents an observable integer property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev.Core;
public class ObservableIntExample : MonoBehaviour
{
    public ObservableInt observableInt = new ObservableInt(12);
}
```

![`ObservableInt` view in inspector](./Images/observable-int-example.jpg)

## Constructors

```cs
public ObservableInt() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableInt(int _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.