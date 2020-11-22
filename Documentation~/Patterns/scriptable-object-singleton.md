# Muffin Dev for Unity - `ScriptableObjectSingleton`

Implementation of singleton pattern for `ScriptableObject` objects.

Note that retrieving a loaded `ScriptableObject` can be done using `Resources.FindObjectsOfTypeAll()`. It means that this pattern is "reload-proof", and avoids troubles with hot reload and statics.

**IMPORTANT NOTE:** To make `ScriptableObjectSingleton` work correctly, you must add the assets to "preloaded assets". To do that, got to *Edit > Project Settings > Player > Other Settings > Preloaded Assets*, and put the singleton asset in that array.

## Purpose

The singleton pattern is made to guarantee that you have only one instance of a certain type in all your project, accessible from anywhere.

With this `ScriptableObject` version, you can make an asset (that contains settings for example) accessible from anywhere, without using Asset Bundles or resources loading, and also without knowing the path to it.

## Usage

```cs
public class PlayerData : ScriptableObjectSingleton<PlayerData>
{
    public float hp;
    public float attack;
    public float defense;
}
```

## Behaviors

If there's a single asset of this type in your project, that instance is defined as the singleton instance.

If there's no asset of this type in your project, you'll be aware of that by a message in Unity console. You have to create that asset manually. You'll get the default asset instance anyway, avoiding to break your game if no instance exists in your project.

If there's multiple assets of this type in your project, you'll be aware of that by a message in Unity console. But other assets won't be destroyed automatically, so you have to delete them manually.

## Methods

```cs
public static bool HasInstance()
```

Checks if this singleton has an instance.

---

```cs
public static T Instance { get; }
```

Gets this singleton's instance. If the instance has not been set already, find all assets of that type using `Resources.FindObjectsOfTypeAll()`. If there's only one asset found, sets it as singleton instance.