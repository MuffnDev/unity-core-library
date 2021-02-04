# Muffin Dev for Unity - Core - `Prompt`

Editor utility that displays a Prompt dialog window, allowing the user to type a text, validate or cancel.

![Prompt dialog window](./Images/prompt-dialog.jpg)

## Usage

As an example, here is the implementation of the `PromptDemoEditor` script that demonstrates how to use the `Prompt` utility:

```cs
using UnityEngine;
using UnityEditor;

using MuffinDev.Core.EditorOnly;

public class PromptDemoEditor : EditorWindow
{
    private string m_PlayerName = string.Empty;

    private void OnGUI()
    {
        // Display the player name field, disabled. So the user must click on the "Change Player Name" button to set it
        GUI.enabled = false;
        EditorGUILayout.TextField("Player Name", m_PlayerName);
        GUI.enabled = true;

        if (GUILayout.Button("Change Player Name", GUILayout.Height(40f)))
        {
            // Display a prompt dialog window that will set the new player name when the user confirms
            Prompt.DisplayPrompt("Change Player Name", "What is the new name of the player?", this, (answer) =>
            {
                m_PlayerName = answer;
                Repaint();
            });
        }
    }

    [MenuItem("Demos/Prompt Dialog Demo", false)]
    private static void ShowWindow()
    {
        PromptDemoEditor window = GetWindow<PromptDemoEditor>(false, EDITOR_WINDOW_TITLE, true);
        window.Show();
    }
}
```

Open that new window by clicking on *Demos > Prompt Dialog Demo*.

In this window, click on the *Change Player Name* button to open the prompt dialog. Type the new name of the player you want to set, and click on the *Ok* button. You can see that the answer you typed in has been set as the player name in the original `PromptDemoEditor` window!

### Shortcuts

You can **confirm** your input:

- by clicking on the *Ok* button
- by pressing the *Return* key
- by pressing the *Enter* key (on the numpad)

You can **cancel**:

- by clicking on the *Cancel* button
- by pressing the *Escape* key
- by clicking anywhere out of the prompt dialog

## Demo

See how this utility behaves by going to *Tools > Muffin Dev > Demos > Prompt Dialog*. Click on the *Change Player Name* to trigger the prompt dialog and just follow the instructions.

## Public API

### Delegates

#### `OnConfirmDelegate`

```cs
public delegate void OnConfirmDelegate(string _Answer)
```

Used to send the typed value when the prompt dialog is confirmed.

- `string _Answer`: The prompt's input field value

#### `OnCancelDelegate`

```cs
public delegate void OnCancelDelegate()
```

Used to send a feedback when the prompt dialog is cancelled.

### Methods

#### `DisplayPrompt()`

```cs
public static void DisplayPrompt(string _Title, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, Vector2 _Position, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, Vector2 _Position, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, Vector2 _Offset, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, Vector2 _Offset, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, Rect _Position, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
public static void DisplayPrompt(string _Title, string _Message, Rect _Position, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null);
```

Display a prompt utility window.

- `string _Title`: The window title
- `OnConfirmDelegate _OnConfirmCallback`: This method will be called once the user clicks on the *Ok* button
- `OnCancelDelegate _OnCancelCallback = null`: This method will be called once the user clicks on the *Cancel* button or clicks out of the prompt window
- `string _Message`: The message to display to the user
- `Vector2 _Position`: The position of the prompt window
- `Vector2 _Size`: The size of the prompt window
- `EditorWindow _OwnerWindow`: The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates of its owning popup
- `Vector2 _Offset`: Adds this offset to the owner window's position
- `Rect _Position`: The position and size of the prompt window

### Accessors

#### `PromptMessageBoxStyle`

```cs
public static GUIStyle PromptMessageBoxStyle
```

Gets the prompt's message box style.

#### `PromptInputStyle`

```cs
public static GUIStyle PromptInputStyle
```

Gets the prompt's input field style.