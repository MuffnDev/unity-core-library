# Muffin Dev for Unity - Core - `ReorderableList` class

Base class for making a reorderable list (using `System.Collections.Generic.List<T>`).

## Constructors

```cs
public ReorderableList()
```

Creates an instance of ReorderableList.

---

```cs
public ReorderableList(IEnumerable<T> _Items)
```

Creates an instance of ReorderableList, and initialize the list with the given items.

---

```cs
public ReorderableList(List<T> _Items)
```

Creates an instance of ReorderableList, and initialize the list with the given items.

## Accessors

```cs
public List<T> Items { get; }
```

Gets the array.