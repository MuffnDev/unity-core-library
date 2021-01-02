# Muffin Dev for Unity - Core - `ObservableChanges<T>` struct

Represents an observable value change.

## Structure

```cs
public struct ObservableChanges<T>
```

Represents an observable value change.

* `T`: Type of the observed property

## Constructor

```cs
public ObservableChanges(T _PreviousValue, T _NewValue)
```

Creates an instance of `ObservableChanges`.

* `T _PreviousValue`: The previous value of the observable property
* `T _NewValue`: The new value of the observable property

## Accessors

```cs
public T PreviousValue { get; }
```

Gets the previous value of the observable property.

---

```cs
public T NewValue { get; }
```

Gets the new value of the observable property.