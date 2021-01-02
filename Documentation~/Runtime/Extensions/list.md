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

### `IsInRange()`

```cs
public static bool IsInRange<T>(this IList<T> _List, int _Index)
```

Checks if the given index is in this list's range.

### `Join()`

```cs
public static string Join<T>(this IList<T> _List, string _Separator)
```

A shortcut for using string.Join() method on lists.

- `string _Separator)`: The character that separates each elements in the output text.

```cs
public static T[] Paginate<T>(this IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
public static T[] Paginate<T>(this IList<T> _List, out Pagination _Pagination, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE);
```

Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination settings.

- `this IList<T> _List`: The list that is paginated.
- `int _Page`: The current page.
- `int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE`: The number of elements displayed per page.
- `out Pagination _Pagination`: The Pagination infos of the operation.

Returns the sub-list of the elements to display.

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