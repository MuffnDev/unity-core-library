# Muffin Dev for Unity - `AssetCreationResult`

Represents the result of the creation of an asset.

## Constructor

```cs
public AssetCreationResult(string _AbsolutePath, Object _Asset)
```

Creates an instance of `AssetCreationResult`.

* `string _AbsolutePath`: path to the folder that will contain the created asset. The path relative to the current project is deduced from it
* `Object _Asset`: the asset to be created in project's assets

## Methods

```cs
public override string ToString()
```

Converts the `AssetCreationResult` instance into a string, with the following format:

```txt
AssetCreationResult: created asset = {created asset's name}, relative path = "{relative path to the asset}", absolute path = "{absolute path to the asset}"
```

## Accessors

```cs
public TAssetType GetAsset<TAssetType>()
    where TAssetType : Object
```

Gets the created asset converted to the given type.

---

```cs
public Object AssetObject
```

Gets the created asset as Object instance.

---

```cs
public string AbsolutePath
```

Gets the absolute path to the created asset.

---

```cs
public string RelativePath
```

Gets the relative path to the created asset.

---

```cs
public bool HasBeenSuccessfullyCreated
```

Checks if the asset has successfully been created.

## Operators

```cs
public static implicit operator bool(AssetCreationResult _AssetCreationResult)
```

Returns true if the asset has successfully been created, false if not.