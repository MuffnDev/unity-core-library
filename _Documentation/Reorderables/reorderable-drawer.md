# Muffin Dev for Unity - Core - `ReorderableDrawer` class

Base class for making a reorderable custom property drawer.

## Usage

You can inherit from this class for 3 use cases:

- for making a property drawer for a basic reorderable with unique item's property field (not a custom class)
- for making a property drawer for a custom class that have a custom property drawer
- for making your own property drawer behavior for custom reorderable types

### For properties with unique item's property field

In this example, we make a custom reorderable class for sorting colliders:

```cs
using UnityEngine;
using MuffinDev;
[System.Serializable]
public class ReoderableColliderArray : ReorderableArray<Collider> { }
```

Now, create a property drawer for this reorderable type, using `ReorderableDrawer`. Note that the following script **must be placed in an *Editor/* folder**:

```cs
using UnityEditor;
using MuffinDev.EditorUtils;
[CustomPropertyDrawer(typeof(ReoderableColliderArray))]
public class ReorderableColliderDrawer : ReorderableDrawer { }
```

To test these scripts, create a `MonoBehaviour` that uses the `ReorderableColliderArray` property:

```cs
using UnityEngine;
public class ReorderableColliderArrayExample : MonoBehaviour
{
    public ReoderableColliderArray colliders;
}
```

Place this component in a scene, and you'll get this layout in the inspector:

![`ReorderableColliderArray` example](./Images/reorderable-colliders-example.jpg)

### Make your own property drawer for your own reorderable types

In this example, you'll learn to make a reorderable drawer for the `Highscore` class as in the example on the [main documentation](./README.md), except this time we'll make a custom property drawer for that class.

#### The reorderable class

First of all, create the `Highscore` class and its reorderable list.

```cs
[System.Serializable]
public class Highscore
{
    public string username;
    public int score;
}
```

```cs
using MuffinDev;
[System.Serializable]
public class ReorderableHighscoreList : ReorderableList<Highscore> { }
```

#### Property drawer for `Highscore`

A property drawer is meant to be used only in the editor, so **you must create this script in an *Editor/* folder**.

For this example, we want any `Highscore` instance properties drawn in the inspector to be on a single line. To do that, we can make a script that inherits from `PropertyDrawer` and overrides both `OnGUI()` and `GetPropertyHeight()` methods:

```cs
using UnityEngine;
using UnityEditor;
using MuffinDev.EditorUtils;
[CustomPropertyDrawer(typeof(Highscore))]
public class HighscoreDrawer : PropertyDrawer
{
    public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
    {
        // Draws the defaultproperty layout if multiple values are selected
        if(_Property.hasMultipleDifferentValues)
        {
            EditorGUI.PropertyField(_Position, _Property);
            return;
        }

        // Get username and score properties of the Highscore target
        SerializedProperty usernameProperty = _Property.FindPropertyRelative("username");
        SerializedProperty scoreProperty = _Property.FindPropertyRelative("score");

        // Init layout values
        Vector2 position = _Position.position;
        Vector2 size = new Vector2(EditorGUIUtility.labelWidth, EditorHelpers.LINE_HEIGHT);

        // Draw the username property field, at the usual label position
        EditorGUI.PropertyField(new Rect(position, size), usernameProperty, new GUIContent(""));
        position.x += size.x + EditorHelpers.HORIZONTAL_MARGIN;

        // Draw the score property field, at the usual value field position
        size.x = _Position.width - size.x - EditorHelpers.HORIZONTAL_MARGIN;
        EditorGUI.PropertyField(new Rect(position, size), scoreProperty, new GUIContent(""));

        _Property.serializedObject.ApplyModifiedProperties();
    }

    public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
    {
        // Returns the default height of the property if multiple values are selected
        if(_Property.hasMultipleDifferentValues)
        {
            return base.GetPropertyHeight(_Property, _Label);
        }
        else
        {
            return EditorHelpers.LINE_HEIGHT;
        }
    }
}
```

#### Reorderable drawer

Create a class that inherits from `ReorderableDrawer` to make a custom property drawer for `ReorderableHighscoreList` class:

```cs
using UnityEditor;
using MuffinDev.EditorUtils;
[CustomPropertyDrawer(typeof(ReorderableHighscoreList))]
public class ReorderableHighscoreListDrawer : ReorderableDrawer { }
```

#### Test

To test this brand new reorderable type, create a `MonoBehaviour` that have a `ReorderableHighscoreList` property:

```cs
using UnityEngine;
public class ReorderableHighscoreExample : MonoBehaviour
{
    public ReorderableHighscoreList highscores;
}
```

Place this component in a scene, and you'll get this layout in the inspector:

![`ReorderableHighscoreList` example](./Images/reorderable-highscore-example2.jpg)

## Methods

```cs
public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
```

Called when the inspector draws the given property.

---

```cs
protected virtual bool DrawList(ReorderableList _List, Rect _Position, SerializedProperty _Property)
```

Draws the given ReorderableList.

* `ReorderableList _List`: The list to draw
* `Rect _Position`: Position and size of the property in the inspector
* `SerializedProperty _Property`: The Reorderable object as `SerializedProperty`

---

```cs
protected virtual void OnDrawItem(Rect _Rect, int _Index, bool _IsActive, bool _IsFocused)
```

Called when an item is drawn in the reorderable list.

* `Rect _Rect`: Position and size of the drawn item
* `int _Index`: Index of the current item
* `bool _IsActive`: Is this item selected and active in inspector view?
* `bool _IsFocused`: Is this item selected in inspector view?

## Accessors

```cs
public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
```

Gets the height of the reorderable object's inspector.

---

```cs
protected virtual float GetItemHeight()
```

Gets the height of an item of the ReorderableList.

---

```cs
protected ReorderableList ItemsList
```

Gets the ReorderableList relative to the reorderable object's list or array.