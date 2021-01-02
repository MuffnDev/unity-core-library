# Muffin Dev for Unity - `Pagination`

Utility class for pagination system.

## Usage

You can use this utility by two ways: using the `Pagination` utility, or with the extension methods.

### Usage with the `Pagination` utility

```cs
using UnityEngine;
using MuffinDev.Core;

public class PaginationTester : MonoBehaviour
{
    public string[] values = { "A", "B", "C", "D", "E", "F", "G", "H" };
    public Pagination pagination = new Pagination(1, 3);

    private void Update()
    {
        string[] paginatedValues = pagination.Paginate(values);
        // If you're using the default values, outputs "DEF"
        Debug.Log(paginatedValues.Join(""));
    }
}
```

![Preview of `PaginationTester` component](./Images/pagination-demo.png)

### Usage with extension methods

```cs
using UnityEngine;
using MuffinDev.Core;

public class PaginationTester : MonoBehaviour
{
    public string[] values = { "A", "B", "C", "D", "E", "F", "G", "H" };

    private void Start()
    {
        string[] paginatedValues = values.Paginate(2, 3);
        // Outputs "GH"
        Debug.Log(paginatedValues.Join(""));
    }
}
```

## Public API

### Constants

#### `DEFAULT_NB_ELEMENTS_PER_PAGE`

```cs
public const int DEFAULT_NB_ELEMENTS_PER_PAGE = 25;
```

### Constructors

```cs
public Pagination(int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
public Pagination(int _NbElements, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
```

- `int _Page`: The current page.
- `int _NbElements`: The number of elements in your paginated ensemble.
- `int _NbElementsPerPage`: The number of elements displayed per page.

### Methods

#### `Paginate()`

```cs
public T[] Paginate<T>(IList<T> _List);
public static T[] Paginate<T>(IList<T> _List, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
public static T[] Paginate<T>(IList<T> _List, out Pagination _Pagination, int _Page, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
```

Creates a sub-list of the given list that contains only the elements that should be displayed using the given pagination settings.

- `<T>`: The type of elements in the given list.
- `IList<T> _List`: The list that is paginated.
- `int _Page`: The current page.
- `int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE`: The number of elements displayed per page.
- `out Pagination _Pagination`: The Pagination infos of the operation.

#### `PagesCount()`

```cs
public static int PagesCount<T>(IList<T> _List, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
public static int PagesCount(int _NbElements, int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE);
```

Computes the number of pages, given the total number of elements and the number of elements to show per page.

- `<T>`: The type of elements in the given list.
- `IList<T> _List`: The list that is paginated.
- `int _NbElementsPerPage = DEFAULT_NB_ELEMENTS_PER_PAGE`: The number of elements displayed per page.
- `int _NbElements`: The number of elements in your paginated ensemble.

### Accessors

#### `Page`

```cs
public int Page { get; set; }
```

Gets/sets the current page index (0-based).

#### `NbPages`

```cs
public int NbPages { get; }
```

Gets the number of pages.

#### `NbElements`

```cs
public int NbElements { get; set; }
```

Gets/sets the current page index (0-based).

#### `NbElementsPerPage`

```cs
public int NbElementsPerPage { get; set; }
```

Gets/sets the number of elements displayed per page.

#### `FirstIndex`

```cs
public int FirstIndex { get; }
```

Gets the first index (inclusive) of your paginated list that should be displayed.

#### `LastIndex`

```cs
public int LastIndex { get; }
```

Gets the last index (inclusive) of your paginated list that should be displayed.

### Operators

```cs
public static bool operator == (Pagination _A, Pagination _B)
```

Checks if the given paginations are equal.

---

```cs
public static bool operator !=(Pagination _A, Pagination _B)
```

Checks if the given paginations are different.

---

```cs
public static Pagination operator ++(Pagination _Pagination)
```

Increment the current page number.

---

```cs
public static Pagination operator --(Pagination _Pagination)
```

Decrement the current page number.

---

```cs
public static Pagination operator +(Pagination _Pagination, int _NbPagesNext)
```

Adds the given number of pages to the current one.

---

```cs
public static Pagination operator -(Pagination _Pagination, int _NbPagesPrevious)
```

Substracts the given number of pages to the current one.

---

```cs
public override bool Equals(object _Other)
```

Checks if the given object is equal to this Pagination object.

---

```cs
public override int GetHashCode()
```

Gets the Hash Code of this Pagination object.

---

```cs
public override string ToString()
```

Converts this Pagination object into a string.