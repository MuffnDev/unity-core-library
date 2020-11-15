# Muffin Dev for Unity - `ListExtension`

Extension methods for `List` or `IList` objects.

## Methods

### `AddOnce()`

```cs
public static bool AddOnce<T>(this IList<T> _List, T _Item)
```

Adds the given item to the list only if it's not already in.

- `T _Item`: The item to add

Returns `true` if the item has been added, or `false` if the item is already in the list.

### `Join()`

```cs
public static string Join<T>(this IList<T> _List, string _Separator)
```

A shortcut for using string.Join() method on lists.

- `string _Separator)`: The character that separates each elements in the output text.

### `Shuffle()`


```cs
public static void Shuffle<T>(this IList<T> list)
```

Shuffles the list in-place, using UnityEngine.Random().

Original version at [https://stackoverflow.com/questions/273313/randomize-a-listt](https://stackoverflow.com/questions/273313/randomize-a-listt).

### `ShuffleCrypto()`

```cs
public static void ShuffleCrypto<T>(this IList<T> list)
```

Shuffles the list in-place, using Cryptography random number generators. This method is slower than Shuffle(), but provides a better randomness quality.

Original version at [https://stackoverflow.com/questions/273313/randomize-a-listt](https://stackoverflow.com/questions/273313/randomize-a-listt).