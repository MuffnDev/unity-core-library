# Muffin Dev for Unity - `BlackboardValueEditor`

Base class for creating custom editors for [Blackboard](../../Runtime/Utilities/Blackboard/README.md), and setup custom available values to be created directly from the editor.

A blackboard is a serializable ensemble of data which can have different types of serializable values.

![`BlackboardAsset` inspector preview](./Images/blackboard-example.png)

## Usage

In order to create a custom available value type and the editor that comes with, you just have to inherit from `BlackboardValueEditor<>` class and implement a GUI method. Don't forget that in a Blackboard, labels should also be customizable. But there's some shortcuts to deal with it.

### Usage for primitive or built-in types

The following script shows you how to implement a custom `BlackboardValueEditor` for `int` values. Note that this type is already implemented in the package, this is only a usage demo:

```cs
using System;
using UnityEngine;
using UnityEditor;
using MuffinDev.Core.EditorOnly;
	
[Serializable]
public class BlackboardValueEditor_Int : BlackboardValueEditor<int>
{
    public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
    {
        // Compute the rects (with appropriate size and position) for the label and the field
        MuffinDevGUI.ComputeLabelledFieldRects(_Position, out Rect labelRect, out Rect fieldRect);

        // Draw the key field, using the SetKey() shortcut
        SetKey(_Item, EditorGUI.TextField(labelRect, GetKey(_Item)));
        // Get the value using GetValue() shortcut, and draw the appropriate field
        int currentValue = GetValue(_Item);
        int newValue = EditorGUI.IntField(fieldRect, currentValue);
        // Apply changes if needed using the SetValue() shortcut
        if (currentValue != newValue)
            SetValue(_Item, newValue);
    }
}
```

### Usage for custom types

See the implementation demo in the *BlackboardValueEditor_PlayerData.cs* script. You can enable the demo implementation by uncommenting the very first line of the script.

As an example, here is how it's implemented. Given a class `PlayerData`:

```cs
[System.Serializable]
public class PlayerData
{
    public string name;
    public int score;
}
```

And here is the existing implementation for allowing users to create `PlayerData` fields in a `Blackboard` property.

```cs
using System;
using UnityEngine;
using UnityEditor;
using MuffinDev.Core.EditorOnly;

///<summary>
/// Creates a custom editor GUI for a value to set on a Blackboard object.
///</summary>
[Serializable]
public class BlackboardValueEditor_PlayerData : BlackboardValueEditor<BlackboardAssetDemo.PlayerData>
{
    // Override OnGUI() to define a GUI drawer using IMGUI. You can also use CreatePropertyGUI() to draw the GUI using UIElements
    public override void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label)
    {
        Rect rect = new Rect(_Position);
        rect.height = MuffinDevGUI.LINE_HEIGHT;
        // Compute the rects (with appropriate size and position) for the label and the field
        MuffinDevGUI.ComputeLabelledFieldRects(rect, out Rect labelRect, out Rect fieldRect);

        // Draw the key field, using the SetKey() shortcut
        SetKey(_Item, EditorGUI.TextField(labelRect, GetKey(_Item)));
        // Display a simple message that specify the type of the object to the user
        EditorGUI.LabelField(fieldRect, $"({ValueType.Name})");

        // Get the original data using the GetValue() shortcut
        BlackboardAssetDemo.PlayerData data = GetValue(_Item);
        // Indent the next field for more clarity
        EditorGUI.indentLevel++;
        {
            // Draw the "Name" property field
            rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
            rect = EditorGUI.IndentedRect(rect);
            data.name = EditorGUI.TextField(rect, "Name", data.name);

            // Draw the "Score" property field
            rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
            data.score = EditorGUI.IntField(rect, "Score", data.score);
        }
        EditorGUI.indentLevel--;

        // Apply changes if needed using the SetValue() shortcut
        SetValue(_Item, data);
    }

    // Compute the height of the GUI rect this property should occupy in the inspector
    public override float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label)
    {
        return MuffinDevGUI.LINE_HEIGHT * 3 + MuffinDevGUI.VERTICAL_MARGIN * 2;
    }
}
```

## Structure

```cs
public abstract class BlackboardValueEditor<T> : IBlackboardValueEditor { }
```

Creates a custom editor GUI for a value to set on a Blackboard object.

- `<T>`: The type of the value to decorate.

### `IBlackboardValueEditor` interface

#### `OnGUI()`

```cs
void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label);
```

Draws the GUI of the value (using IMGUI).

- `Rect _Position`: The position and available space to draw the GUI.
- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.
- `GUIContent _Label`: The label of the property.

#### `GetPropertyHeight()`

```cs
float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label);
```

Gets the expected height of the field in the editor.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.
- `GUIContent _Label`: The label of the property.

Returns the height of the property to display.

#### `CreatePropertyGUI()`

```cs
VisualElement CreatePropertyGUI(SerializedProperty _Item);
```

Creates a VisualElement to draw the GUI of the item

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

Returns the element to attach to the panel.

#### `ValueType`

```cs
Type ValueType { get; }
```

Gets the type of the decorated value.

## Public API

### Methods

#### `OnGUI()`

```cs
void OnGUI(Rect _Position, SerializedProperty _Item, GUIContent _Label);
```

Draws the GUI of the value (using IMGUI).

- `Rect _Position`: The position and available space to draw the GUI.
- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.
- `GUIContent _Label`: The label of the property.

#### `GetPropertyHeight()`

```cs
float GetPropertyHeight(SerializedProperty _Item, GUIContent _Label);
```

Gets the expected height of the field in the editor.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.
- `GUIContent _Label`: The label of the property.

Returns the height of the property to display.

#### `CreatePropertyGUI()`

```cs
VisualElement CreatePropertyGUI(SerializedProperty _Item);
```

Creates a VisualElement to draw the GUI of the item.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

Returns the element to attach to the panel.

#### `GetValue()`

```cs
public T GetValue(SerializedProperty _Item);
```

Gets the value of the given entry.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

#### `GetValue()`

```cs
public void SetValue(SerializedProperty _Item, T _Value);
```

Sets the serialized value of the given entry.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

#### `GetKeyProperty()`

```cs
public SerializedProperty GetKeyProperty(SerializedProperty _Item);
```

Gets the Serialized Property that represent the key field.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

#### `GetKey()`

```cs
public string GetKey(SerializedProperty _Item);
```

Gets the key of the given entry.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.

#### `SetKey()`

```cs
public void SetKey(SerializedProperty _Item, string _Key);
```

Sets the key of the given entry.

- `SerializedProperty _Item`: The serialized property that represent an entry of a blackboard.
- `string _Key`: The new key value you want to set.

### Accessors

#### `ValueType`

```cs
Type ValueType { get; }
```

Gets the type of the decorated value.