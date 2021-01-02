# Muffin Dev for Unity - `Spinner`

Utility class to update and draw a loading spinner icon.

## Usage

Since editor GUI is not always updated as components can be in your game, you'll need to update the spinner yourself, by using the `Spinner.Update()` method. And calling this method at the appropriate moment depends on your editor context (window, property drawer, custom inspector, ...).

Note that the `Spinner` uses `EditorApplication.timeSinceStartup` value in order to animate the icon. **Because of that, you have to initialize the `Spinner` variable in `OnEnable()`**. If you initialize it when the variable is declared, you'll get an exception from Unity.

### In an `Editor Window`

`EditorWindow` class is easy to update, since it has access to the `Udpate()` message, like with `MonoBehaviour`. So the process is to update the `Spinner` and repaint the window each update, and just draw the `Spinner` in the `OnGUI()` method.

```cs
using UnityEngine;
using UnityEditor;
using MuffinDev.Core.EditorOnly;
public class SpinnerEditorWindow : EditorWindow
{
    private Spinner m_Spinner = null;

    private void OnEnable()
    {
        m_Spinner = new Spinner();
    }

    // Make the window update each editor frame, so the spinner can be updated and the GUI repainted
    private void Update()
    {
        if (m_Spinner != null)
            m_Spinner.Update();

        Repaint();
    }

    [MenuItem("Demos/Spinner", false)]
    public static void ShowWindow()
    {
        SpinnerEditorWindow window = GetWindow<SpinnerEditorWindow>(false, "Spinner", true).Show();
    }

    private void OnGUI()
    {
        m_Spinner.DrawGUI();
    }
}
```

You can open this window from `Demos > Spinner`.

### In a custom inspector (`Editor`)

When it comes to custom inspectors, there's no `Update()` method. There's two ways to force the inspector to be repainted: by calling `Repaint()` manually (at the end of `OnInspectorGUI()` for example), or by overriding `RequiresConstantRepaint()`. In both cases, `OnInspectorGUI()` will be called each editor frames, so you can update the spinner directly inside the `OnInspectorGUI()` method.

Let's create a `Dummy` component in order to create the custom editor that overrides its default inspector.

```cs
using UnityEngine;
public class Dummy : MonoBehaviour { }
```

Now, create its custom editor.

```cs
using UnityEditor;
using MuffinDev.Core.EditorOnly;

[CustomEditor(typeof(Dummy))]
public class DummyEditor : Editor
{
    private Spinner m_Spinner = null;

    private void OnEnable()
    {
        m_Spinner = new Spinner();
    }

    public override void OnInspectorGUI()
    {
        m_Spinner.Update();
        m_Spinner.DrawGUI();
    }

    public override bool RequiresConstantRepaint()
    {
        return true;
    }
}
```

Place a `Dummy` component on an object in your scene, and see the result in the inspector.

### Using Editor coroutines

Unity released the [Editor Coroutines](https://docs.unity3d.com/Packages/com.unity.editorcoroutines@0.0) package in April 2020, so you can use them to update the `Spinner` when required. You can download it from the `Package Manager`, in *Unity 2018.4+*.

[=> See the official documentation](https://docs.unity3d.com/Packages/com.unity.editorcoroutines@0.0)

## Public API

### Constructors

```cs
public Spinner();
public Spinner(float _SpinInterval);
```

Creates a `Spinner` instance.

- `float _SpinInterval`: The interval of each spinner update.

### Methods

#### `Update()`

```cs
public void Update();
```

Updates this `Spinner`.

#### `Resume()`

```cs
public void Resume();
```

Resumes this `Spinner`, so it can be updated.

#### `Pause()`

```cs
public void Pause();
```

Pauses this `Spinner`, avoiding it to be updated.

#### `DrawGUI()`

```cs
public void DrawGUI(params GUILayoutOption[] _GUILayoutOptions)
```

Draws this `Spinner` in your GUI, using Layout methods.

---

```cs
public void DrawGUI(Rect _Rect);
public void DrawGUI(Rect _Rect, GUIStyle _Style);
```

Draws this `Spinner` in yout GUI.

### Accessors

#### `Paused`

```cs
public bool Paused { get; set; }
```

Pauses/resumes this `Spinner`.