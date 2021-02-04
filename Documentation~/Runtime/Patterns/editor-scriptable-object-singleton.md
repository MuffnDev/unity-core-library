# Muffin Dev for Unity - `EditorScriptableObjectSingleton`

Implements singleton pattern for assets used in the Editor (tool settings for example).

**WARNING: This type of asset is meant to be used only in the editor. Anyway, this script is not placed in an Editor folder, in order to let the possibility for non-editor objects (like MonoBehaviour) to interact with it if needed. but out of the editor context, you won't be able to get its instance.**

## Usage

```cs
using System.Collections.Generic;
using MuffinDev.Core;

public class MyEditorAsset : EditorScriptableObjectSingleton<MyEditorAsset>
{
    public List<string> editorBlackgoard;
}
```

## Structure

```cs
public abstract class EditorScriptableObjectSingleton<TSingletonType> : ScriptableObject
    where TSingletonType : ScriptableObject
```

- `TSingletonType`: The type of the instance to return. Should be the same type of your inheriting class.

## Methods

```cs
public static TSingletonType Instance { get; }
```

Gets the unique instance of this asset, or creates it if it doesn't exist yet.

If no instance of this asset have been loaded yet, this accessor will first get the asset using `AssetDatabase.FindAssets()`. If there's no asset of that type in your project, an asset is created and saved at the same path of your inheriting class' script.

**WARNING: This method is meant to be used in the editor. Using it out of this context will always return null.**