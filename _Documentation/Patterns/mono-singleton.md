# Muffin Dev for Unity - `MonoSingleton`

Implementation of singleton pattern for `MonoBehaviour` object.

## Purpose

The singleton pattern is made to guarantee that you have only one instance of a certain type in all your project, accessible from anywhere.

With this `MonoBehaviour` version, you can make component that any object can get, and only one instance of that object will be used. So you can store other GameObject in it, scene settings, etc., and give access to these datas to all other objects.

## Usage

```cs
public class WorldSettings : MonoSingleton<WorldSettings>
{
    public GameObject[] enemyPrefabs = { };

    protected override void MonoSingletonOnInit()
    {
        Debug.LogFormat("The object {0} is now defined as only WorldSettings singleton instance.", name);
    }

    protected override void MonoSingletonRejected()
    {
        Debug.LogWarning(string.Format("There's more than 1 instance of WorldSettings in the scene. The object {0} is rejected.", name));
    }

    protected override void MonoSingletonOnAwake()
    {
        Debug.LogFormat("The object {0} awakes.", name);
    }

    protected override void MonoSingletonOnDestroy()
    {
        Debug.LogFormat("The object {0} is destroyed.", name);
    }
}
```

## Behaviors

If there's a single instance in the scene, that instance is defined as the singleton instance.

If the instance is required (by calling `Instance` accessor) but the singleton instance has not been set:

- If there's an instance in the scene:
    - Gets that instance using `GameObject.FindObjectOfType()`
    - Sets the found instance as the singleton instance
- Else:
    - Creates a new `GameObject`, and attach the instance using `GameObject.AddComponent()`
    - Sets that new instance as the singleton instance

If there's more than one instances of the component, the first to receive `Awake()` message will be set as singleton instance. Others will be destroyed.

The `Awake()` ordrer is not predictable for objects of the same type. So you will be aware of the multiple instances (by message in Unity console), but you have to delete them from scene manually in the editor.

If you use `MonoSingleton` instance in the editor, when the game is not running, the static variable won't be used.

## Callbacks & Messages

You won't be able to use `Awake()` and `OnDestory()` Unity messages. They are replaced by `MonoSingletonOnAwake()` and `MonoSingletonOnDestroy()`.

```cs
protected override void MonoSingletonOnInit()
```

Called at `Awake()`, before `MonoSingletonOnAwake()`, but only if the current instance has been set as the MonoSingleton instance.

Note that if you want to have this instance persisting form a scene to another, you can use this callback to call `DontDestroyOnLoad()`. And so, the stored `MonoSingleton` instance won't be lost.

---

```cs
protected virtual void MonoSingletonOnAwake()
```

Called at `Awake()`, after `MonoSingletonOnInit()`, but only if the current instance has been set as the Mono Singleton instance.

---

```cs
protected virtual void MonoSingletonRejected()
```

Called at `Awake()`, before `MonoSingletonOnDestroy()`, but only if the current instance can't be set as the Mono Singleton instance.

---

```cs
protected virtual void MonoSingletonOnDestroy()
```

Called at `OnDestory()`, no matter if this instance is the Mono Singleton instance.

## Accessors

```cs
public static TSingletonType Instance { get; }
```

Gets the instance of this `MonoSingleton`.