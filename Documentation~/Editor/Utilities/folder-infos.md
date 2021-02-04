# Muffin Dev for Unity - `FolderInfos`

Contains the name and the path of a folder in the current Unity project.

## Properties

```cs
public string name
```

The name of the folder.

---

```cs
public string path
```

The absolute path to the folder.

## Methods

```cs
public override string ToString()
```

Converts this `FolderInfos` instance into a string with the following format:

```txt
{name} ({path})
```