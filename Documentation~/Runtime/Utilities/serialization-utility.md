# Muffin Dev for Unity - `SerializationUtility`

Utility class for serializing data not managed by the built-in Unity serialization.

Note that for now, this class is not as magic as you could think: since it uses `JSONUtility`, only the serializable properties of the objects you want to serialize are supported.

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

And here comes the `SerializationUtility`, which allow you to solve these limitations!

## Usage

In Unity, you can customize the serialization and deserialization behavior of a class using the [`ISerializationCallbackReceiver`](https://docs.unity3d.com/ScriptReference/ISerializationCallbackReceiver.html) interface.

As an example, and taking the classes above, here's a way to serialize a list that contain several `Character` inheritors instances:

```cs
using System;
using System.Collections.Generic;
using UnityEngine;
using MuffinDev.Core;

[Serializable]
public class SerializableCharactersList
{
    [Serializable]
    private class SerializedCharacter : ISerializationCallbackReceiver
    {
        public string characterTypeName;
        public string serializedData;
        public Character character;

        // Called before Unity serializes this class
        public void OnBeforeSerialize()
        {
            if (character == null)
            {
                characterTypeName = null;
                serializedData = null;
                return;
            }

            // Get informations about the type name (using Type.AssemblyQualifiedName in order to retrieve the type through reflection on
            // deserialization)
            Type characterType = character.GetType();
            characterTypeName = characterType.AssemblyQualifiedName;
            // Serialize data to string (so, to a serializable data) using SerializationUtility.SerializeToString()
            serializedData = SerializationUtility.SerializeToString(character);
        }

        // Called after Unity deserializes this class
        public void OnAfterDeserialize()
        {
            // Deserialize character data from string using SerializationUtility.DeserializeFromString()
            object deserializedData = SerializationUtility.DeserializeFromString(characterTypeName, serializedData);
            if (deserializedData != null)
                character = deserializedData as Character;
        }
    }

    // The serialized data are in a string format, so it's serializable natively by Unity
    [SerializeField, HideInInspector]
    private List<SerializedCharacter> _charactersList = new List<SerializedCharacter>();

    // Demo method: gets a character by name
    public Character GetCharacter(string name)
    {
        foreach (SerializedCharacter sc in _charactersList)
        {
            if (sc.character.name == name)
                return sc.character;
        }
        return null;
    }

    // Demo method: adds a character to the list
    public void AddCharacter(Character character)
    {
        _charactersList.Add(new SerializedCharacter() { character = character });
    }
}
```

## Public API

### Methods

#### `SerializeToString()`

```cs
public static string SerializeToString<T>(T _Data);
public static string SerializeToString(object _Data);
```

Serializes the given data into a string. This method uses `JSONUtility` to serialize objects, or just uses `object.ToString()` for primitives.

- `<T>`: The type of the data to serialize.
- `T _Data`: The data you want to serialize.
- `object _Data`: The data you want to serialize.

Returns the serialized data. Note that if the data is `null`, it returns an empty string.

#### `DeserializeFromString()`

```cs
public static T DeserializeFromString<T>(string _SerializedData);
public static object DeserializeFromString(string _DataType, string _SerializedData);
public static object DeserializeFromString(Type _DataType, string _SerializedData);
```

Deserializes data from the given string. Note that the string should've been made with `SerializationUtility.SerializeToString()` method in order to work as expected.

- `<T>`: The expected type of the data to deserialize.
- `string _SerializedData`: The serialized data as string.
- `string _DataType`: The expected type of the data to deserialize.
- `Type _DataType`: The expected type of the data to deserialize.

Returns the deserialized data, or `null` if it failed.