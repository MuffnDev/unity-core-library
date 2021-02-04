# Muffin Dev for Unity - `ScriptableObjectExtension`

Extension methods for `ScriptableObject` objects.

## Public API

### Methods

#### `GetScriptPath()`

```cs
public static string GetScriptPath<TScriptableObject>();
public static string GetScriptPath(this ScriptableObject _Obj);
public static string GetScriptPath(Type _ScriptableObjectType);
```

Gets the path to the source script used by the given ScriptableObject.

**WARNING: This method is meant to be used in the editor. Using it in another context will always return null.**

- `TScriptableObject`: The type of the `ScriptableObject` you want to get the script path
- `Type _ScriptableObjectType`: The type of the `ScriptableObject` you want to get the script path
- `Type _ScriptableObjectType`: The `ScriptableObject` you want to get the script path

Returns script path, or null if the path to the file can't be found.