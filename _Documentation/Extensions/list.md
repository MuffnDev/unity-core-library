# Muffin Dev for Unity - `ListExtension`

Extension methods for `List` or `IList` objects.

## Methods

```cs
public static void Shuffle<T>(this IList<T> list)
```

Shuffles the list in-place, using UnityEngine.Random().

Original version at [https://stackoverflow.com/questions/273313/randomize-a-listt](https://stackoverflow.com/questions/273313/randomize-a-listt).

---

```cs
public static void ShuffleCrypto<T>(this IList<T> list)
```

Shuffles the list in-place, using Cryptography random number generators. This method is slower than Shuffle(), but provides a better randomness quality.

Original version at [https://stackoverflow.com/questions/273313/randomize-a-listt](https://stackoverflow.com/questions/273313/randomize-a-listt).