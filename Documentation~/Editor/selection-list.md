# Muffin Dev for Unity - Core - `Selection List`

Draws a list with selectable labels. This editor utility is quite a simplified version of Unity's *IMGUI* Tree view.

![`SelectionList` field preview](./Images/selection-list.jpg)

## Usage

As an example, here is the implementation of the `SelectionListDemoEditor` script that demonstrates how to use the `SelectionList` fiedl usage:

```cs
using UnityEngine;
using UnityEditor;
using MuffinDev.Core.EditorUtils;

public class SelectionListDemoEditor : EditorWindow
{
    // The list of items that will be displayed in 
    [SerializeField]
    private string[] m_Items = { };

    // The SelectionList field to display
    [SerializeField]
    private SelectionList m_SelectionList = null;

    // The scroll position of the items array part
    [SerializeField]
    private Vector2 m_ScrollPosition = Vector2.zero;

    // Cache the array items serialized property in order to display the array property field as expected
    private SerializedProperty m_ItemsProp = null;

    private void OnEnable()
    {
        // Initialize the items list, then create the selection list that will show these items and make them selectable
        if (m_Items == null || m_Items.Length == 0)
            m_Items = new string[] { "Apple", "Banana", "Lemon" };
        m_SelectionList = new SelectionList(m_Items);
        m_SelectionList.OnSelectItem += OnSelectItem;

        // Serialize the items property in order to display the property field for the array
        SerializedObject obj = new SerializedObject(this);
        m_ItemsProp = obj.FindProperty(nameof(m_Items));
    }

    // Called when the selected item changes
    private void OnSelectItem(int _Index, string _Item, int _LastSelectedIndex)
    {
        m_LastSelectedItem = _Item;
    }

    // Draw the window GUI
    private void OnGUI()
    {
        // Display the array properties exposed in the selection list
        m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition, GUILayout.Height(120f));
        {
            m_SelectionList.Items = m_Items;
            EditorGUILayout.PropertyField(m_ItemsProp, true);
            m_ItemsProp.serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space();
        // Display the last selected item
        GUI.enabled = false;
        EditorGUILayout.TextField("Last Selected Item", m_LastSelectedItem);
        GUI.enabled = true;
        // Draw the SelectionList field
        m_SelectionList.DrawLayout();
    }

    // Create a Demos > Selection List menu that will open this editor window.
    [MenuItem("Demos/Selection List")]
    private static void ShowWindow()
    {
        SelectionListDemoEditor window = GetWindow<SelectionListDemoEditor>(false, EDITOR_WINDOW_TITLE, true);
        window.Show();
    }
}
```

Open that new window by clicking on *Demos > Selection List*.

In this window, feel free to add or remove items in the array. You'll see these elements as selectable items in the `SelectionList` under the array property!

### Shortcuts

## Demo

See how this utility behaves by going to *Tools > Muffin Dev > Demos > Selection List*. Add or remove items int the array, and use the selection list field to select the item you want.

## Public API

### Delegates

#### `OnConfirmDelegate`

```cs
public delegate void SelectItemDelegate(int _Index, string _Item, int _LastSelectedIndex)
```

Used to send feedback when the selected item of this selection list changes.

- `int _Index`: The index of the selected item in the list.
- `string _Item`: The content of the selected item.
- `int _LastSelectedIndex`: The previous selected item index in the list.

### Constructors

```cs
public SelectionList();
public SelectionList(IEnumerable<string> _Items);
```

- `IEnumerable<string> _Items`: The array or list of items you want to set for this selection list.

### Methods

#### `DrawLayout()`

```cs
public void DrawLayout();
public void DrawLayout(GUIStyle _Style);
```

Draws the selection list using -Layout methods.

- `GUIStyle _Style`: The style to use for the items list.

### Accessors

#### `Items`

```cs
public IEnumerable<string> Items { get; set; }
```

Gets/Sets the items list.

#### `ItemsListStyle`

```cs
public static GUIStyle ItemsListStyle { get; }
```

Gets the default style of the items list.

#### `SelectedItemStyle`

```cs
public static GUIStyle SelectedItemStyle { get; }
```

Gets the style for selected items.

#### `UnselectedItemStyle`

```cs
public static GUIStyle UnselectedItemStyle { get; }
```

Gets the style for unselected items.