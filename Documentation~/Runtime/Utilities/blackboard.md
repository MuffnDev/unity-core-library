# Muffin Dev for Unity - `Blackboard`

Represents a serializable ensemble of data which can have different types of serializable values.

![`Blackboard` usage demo](./Images/blackboard-demo.gif)

## Why?

This feature is a valuable workarond for Unity serialization limitations. Let's say you have this class structure:

```cs
[System.Serializable]
public class Character
{
    public string name;
    public int hp;
}

[System.Serializable]
public class Warrior : Character
{
    public int force;
}

[System.Serializable]
public class Mage : Character
{
    public int mana;
}
```

Now, if you want a serialized list of `Character` which must be saved on disk, Unity serialization will only take account of the list's type:

```cs
using System.Collections.Generic;
using UnityEngine;
public class CharactersManager : MonoBehaviour
{
    public List<Character> characters;

    private void Start()
    {
        characters.Add(new Character());
        characters.Add(new Warrior());
        characters.Add(new Mage());
    }
}
```

There's several issues here:

-  In the inspector, you'll see the array items at runtime, but you will only see the `Character` class properties (`name` and `hp`). The inheritor classes properties won't be displayed.
- If you add warriors and mages to the list in edit mode using a custom inspector, the serialized list is of type `Character`. So when Unity will deserialize this data, the list will only contain `Character` instances, ignoring totally the `Warrior` and `Mage` instances.
- Depending on your game, this structure may not be appropriate: you might want to make the `Character` class abstract because a plain `Character` instance doesn't make sense in your game. But since Unity serialization can't deal with abstract types, the list won't be serialized at all, so you won't be able to see the items in the inspector.

That being said, all these problems can be solved using `Blackboard` property. The `Blackboard` uses boxing and unboxing mechanisms to collect any type of data, and uses the [`SerializationUtility`](./serialization-utility.md) to serialize/deserialize them.

[=> More informations about Unity serialization](https://docs.unity3d.com/Manual/script-Serialization.html)

## Usage

Example usage:

```cs
using UnityEngine;
using MuffinDev.Core;
public class BlackboardDemo : MonoBehaviour
{
    public Blackboard blackboard;
}
```

![`Blackboard` property example](./Images/blackboard-preview.png)

A property drawer is used to provide a custom editor. Click on the *Add Entry* button, select the type of data you want to add to the list, and here you go!

### Blackboard Asset

You can create a `BlackboardAsset` by clicking on *Assets > Create > Muffin Dev > Blackboard Asset* in the editor. This will create an asset which only contains a `Blackboard` property exposed in the inspector, and shortcuts for working with the blackboard properties.

![`BlackboardAsset` inspector preview](./Images/blackboard-example.png)

### Allow custom data types from inspector

Note that you can set any type of variable through code in the `Blackboard`, but only the types that defines a drawer are available in the editor.

[=> More informations about custom type editors](../../Editor/Utilities/blackboard-value-editor.md)

## Public API

### Methods

#### `TryGetValue()`

```cs
public bool TryGetValue<T>(string _Key, out T _Value);
public bool TryGetValue<T>(string _Key, out T _Value, T _DefaultValue);
public bool TryGetValue(string _Key, out object _Value, object _DefaultValue = null);
```

Try to get the value by its key.

- `<T>`: The expected type of the value.
- `string _Key`: The key of the value you want to get.
- `out T _Value`: The found value, or the default value if the expected type doesn't match, or if the key doesn't exist.
- `T _DefaultValue`: The default value to return if the expected type doesn't match or if the key doesn't exist.
- `out object _Value`: The found value, or null if the key doesn't exist.
- `object _DefaultValue = null`: The default value to return if the key doesn't exist.

Returns `true` if the value has been found, or `false` if the expected type doesn't match or if the key doesn't exist.

#### `GetValue()`

```cs
public T GetValue<T>(string _Key);
public T GetValue<T>(string _Key, T _DefaultValue);
public object GetValue(string _Key, object _DefaultValue = null);
```

Gets a value by its key.

- `<T>`: The expected type of the value.
- `string _Key`: The key of the value you want to get.
- `T _DefaultValue`: The default value to use if the expected value can't be found on the blackboard.
- `object _DefaultValue = null`: The default value to use if the expected value can't be found on the blackboard.

Returns the found value, or the default value if the expected type doesn't match, or if the key doesn't exist.

#### `SetValue()`

```cs
public void SetValue<T>(string _Key, T _Value);
public void SetValue(string _Key, object _Value);
```

Updates the value with the given key, or create it on the blackboard.

- `<T>`: The type of the value you want to set.
- `string _Key`: The key of the value you want to create or update.
- `T _Value`: The value you want to set.
- `object _Value`: The value you want to set.

### Accessors

#### `Count`

```cs
public int Count { get; }
```

Gets the number of entries on this `Blackboard`.