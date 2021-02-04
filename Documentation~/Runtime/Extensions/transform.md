# Muffin Dev for Unity - `Transform`

Extensions for `Transform` component.

## Public API

### Methods

#### `ClearHierarchy()`

```cs
public static void ClearHierarchy(this Transform _Transform)
```

Destroys all child `GameObject`s of this `Transform`.

#### `Find()`

```cs
public static Transform Find(this Transform _Obj, string _Name, bool _IncludeSelf = true, bool _IncludeInactive = false);
```

Find an object by name, recursively in the source's hierarchy.

- `string _Name`: The name of the object you want to find.
- `bool _IncludeSelf = true`: If enabled, the input object is included in the research.
- `bool _IncludeInactive = false`: If enabled, this method will also search if the name of disabled objects match.

Returns the found object, or `null` if the source doesn't contain any object with the given name in its hierarchy.