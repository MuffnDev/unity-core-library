# Muffin Dev for Unity - `TEditor`

Shortcut for making custom editors containing a "typed" target object.

## Usage

```cs
public class Player : MonoBehaviour
{
    public void Jump() { }
}
```

```cs
[CustomInspector(typeof(Player))]
public class PlayerEditor : TEditor<Player>
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Make player jump"))
        {
            Target.Jump();

            // Without TEditor:
            // Player player = (target as Player);
            // if(player != null)
            // {
            //     player.Jump();
            // }
        }
    }
}
```

## Structure

```cs
public class TEditor<TTarget> : Editor
    where TTarget : Object
```

* `TTarget`: the type of the expected target object.

Note that in `Editor` class, target is the selected object. See [Unity documentation](https://docs.unity3d.com/ScriptReference/Editor.html) for more informations.

## Methods

```cs
protected virtual void OnEnable()
```

This message is used by `TEditor` for initializing typed target. If you need to use this message, you must override it and use `base.OnEnable()` to make things work correctly.

## Accessors

```cs
public TTarget TypedTarget { get; }
```

Gets the "typed" target (cast [`Editor.target`](https://docs.unity3d.com/ScriptReference/Editor-target.html) property).

---

```cs
public TTarget Target { get; }
```

Alias of `TypedTarget` accessor.