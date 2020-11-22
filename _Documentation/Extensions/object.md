# Muffin Dev for Unity - `ObjectExtension`

Extensions for `UnityEngine.Object` objects.

## Public API

### Methods

#### `GetGUID()`

```cs
public static string GetGUID(this Object _Obj)
```

Gets the GUID string of this `Object`.

**NOTE**: that GUIDs are assigned only to assets, not to scene objects. So if this `Object` is a scene object, this method will return `null`.

**WARNING: This method is meant to be used in Editor. So in build, this method will always return null.**

Returns the GUID string of this `Object`, or `null` if this `Object` is not an asset (meaning it's a scene object).

#### `IsAsset()`

```cs
public static bool IsAsset(this Object _Obj)
```

Checks if this `Object` is an asset (and not a scene object).

**WARNING: This method is meant to be used in Editor. So in build, this method will always return false.**

Returns `true` if the given `Object` is an asset, otherwise false.