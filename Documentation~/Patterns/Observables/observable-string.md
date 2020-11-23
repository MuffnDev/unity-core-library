# Muffin Dev for Unity - Core - `ObservableString`

Represents an observable `string` property.

Inherits from [`ObservableSerialized<T, U>`](./observable-serialized.md).

## Usage

```cs
using UnityEngine;
using MuffinDev.Core;
public class ObservableStringExample : MonoBehaviour
{
    public ObservableString observableString = new ObservableString("Example text");
}
```

![`ObservableString` view in inspector](./Images/observable-string-example.jpg)

## Constructors

```cs
public ObservableString() : base() { }
```

Creates an instance of this Observable.

---

```cs
public ObservableString(string _Value) : base(_Value) { }
```

Creates an instance of this Observable, and initializes its value.