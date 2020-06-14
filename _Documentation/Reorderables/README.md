# Muffin Dev for Unity - Core - Reorderables

Base classes and implementations for easily making reorderable lists in the editor.

This folder provides reorderable types that are "ready-to-use", like system serialized objects (int, string, ...) and basic Unity classes (Vector3, GameObject, ...).

You can also inherit from base classes [`ReorderableList`](./reorderable-list.md) (or [`ReorderableArray`](./reorderable-array.md)) and [`ReorderableDrawer`](./reorderable-drawer.md) to make your own reoderable list types, and their editor layout.

## Usage

### Use "ready-to-use" reorderables

Reorderable lists or arrays are serialized classes, so you can just make a serialized property of a class with one of the implemented reorderables.

```cs
using UnityEngine;
using MuffinDev;
public class ReorderablesExample : MonoBehaviour
{
    public ReorderableStringArray m_Strings = new ReorderableStringArray();
}
```

Using this property type, you'll get this layout in the inspector:

![`ReorderableStringArray` property example](./Images/reorderable-string-example.jpg)

You can drag and drop the elements of the list by clicking on the icon on the left, hold the mouse button down and move the element to its target position.

See the *Summary* below to check all the existing implemented types.

### Make your own reorderable type

Making your own reorderable needs two steps:

- create the reorderable list itself
- create its custom inspector layout

Don't worry, there's base classes to make them both that will make it easier to do.

For this example, we'll make a reorderable highscores list. But first, you need to create the `Highscore` class:

```cs
[System.Serialized]
public class Highscore
{
    public string username;
    public int score;
}
```

#### Create a reorderable list or array

For the array or list container, you can inherit from 2 base classes depending of your needs:

- [`ReorderableArray`]: stores the items in an array property
- [`ReorderableList`]: stores the items in a `List<T>` property

You just have to inherit one of these types (or both) to make a reorderable object. **Don't forget to make that new class serializable**, or it won't be displayed in the inspector:

```cs
using MuffinDev;
[System.Serialized]
public ReorderableHighscoreList : ReorderableList<Highscore> { }
```

That's it!

#### Create its property drawer

In this example, you just have a serialized class that doesn't need a custom property drawer. So, we can inherit directly from `ReorderableCustomClassDrawer` class.

If you want to make a property drawer for a list of a type that have a property drawer, you need to inherit from `ReorderableDrawer`, and maybe override drawing methods.

[=> See `ReorderableDrawer` docs for more informations](./reorderable-drawer.md)

Both of these class must be used only in the editor. They are placed in the `MuffinDev.EditorUtils` namespace, which is places in an *Editor/* folder. So **you must place your new custom property drawers in an *Editor/* folder too**, or you won't be able to access to that namespace.

```cs
using UnityEditor
using MuffinDev.EditorUtils;
[CustomPropertyDrawer(typeof(ReorderableHighscoreList))]
public class ReorderableHighscoreDrawer : ReorderableCustomClassDrawer { }
```

#### Test the reorderable class

Make a `MonoBehaviour` test class:

```cs
using UnityEngine;
public class ReorderableHighscoreExample : MonoBehaviour
{
    public ReorderableHighscoreList highscores;
}
```

Place this component in a scene, and you will get this layout in the inspector:

![Reorderable `Highscore` class example](./Images/reorderable-highscore-example.jpg)

## Demo

Check demo implementations in *MuffinDev/Core/Reorderables/Demos/*.

## Summary

- [`ReorderableArray`](./reorderable-array.md): Base class for making a reorderable array
- [*ReorderableClasses* script](./reorderable-classes.md): Ready-to-use implementations of reorderable lists and arrays.
- [`ReorderableList`](./reorderable-list.md): Base class for making a reorderable list (using `System.Collections.Generic.List<T>`)

Editor classes:

- [*ReorderableClassesDrawer* script](./reorderable-classes-drawer.md): Implementations of [`ReorderableDrawer`](./reorderable-drawer.md) for the ready-to-use reorderable types (see [*ReorderableClasses* script](./reorderable-classes) for more informations)
- [`ReorderableCustomClassDrawer`](./reorderable-custom-class-drawer.md): Base class for making a custom property drawer for simple serialized classes reorderables
- [`ReorderableDrawer`](./reorderable-array.md): Base class for making a reorderable custom property drawer