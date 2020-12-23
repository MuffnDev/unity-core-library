# Muffin Dev for Unity - Core - `ObservableFloat`

Represents an observable `float` property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev.Core;
public class ObservableFloatExample : MonoBehaviour
{
    public ObservableFloat observableFloat = new ObservableFloat(12f);
}
```

![`ObservableFloat` view in inspector](./Images/observable-float-example.jpg)

## Constructors

```cs
public ObservableFloat() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableFloat(float _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.