# Muffin Dev for Unity - Core - `Observable<T>` class

Base class for making observable values properties.

Note that there's an abstract class that inherits from `Observable<T>` for serializable properties, making custom inspector fields easier to implement.

[See `ObservableSerialized<T, U>` for more informations](./observable-serialized.md)

## How to make custom observables

In this example, we make a puzzle game with grid data separated from grid renderer. The grid is a 2D array of `PuzzleCell`, which is not a property type that Unity can serialize.

We want to make the grid data observable. And so, when the grid changes, we can spawn the grid's cell prefabs to display the level to the player.

### Cells and grid

We make an observable grid, but the observed value is the 2D array of `PuzzleCell`.

Note that we must create an event to notify all observers that a change occured.

```cs
using MuffinDev.Core;

public class PuzzleCell { }

public class PuzzleGrid : Observable<PuzzleCell[,]>
{
    public delegate void OnGridChangeDelegate(PuzzleCell[,] _Grid);
    public event OnGridChangeDelegate OnGridChange;

    public PuzzleGrid()
    {
        ResetGrid();
    }

    public void ResetGrid()
    {
        Value = new PuzzleCell[2, 2]
        {
            { new PuzzleCell(), new PuzzleCell() },
            { new PuzzleCell(), new PuzzleCell() }
        };
    }

    protected override void HandleChanges(ObservableChanges<PuzzleCell[,]> _Changes)
    {
        if(OnGridChange != null)
        {
            OnGridChange.Invoke(_Changes.NewValue);
        }
    }
}
```

### Renderer

For this example, we create a `PuzzleGridManager` that contains a `PuzzleGrid` instance. We can assume that this class manage level loading.

For testing purposes, we load a new level by pressing the space bar.

```cs
using UnityEngine;
public class PuzzleGridManager : MonoBehaviour
{
    public PuzzleGrid grid = new PuzzleGrid();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            grid.ResetGrid();
        }
    }
}
```

Now, we can create a `PuzzleGridRenderer` class, that is responsible of spawning grid cells in the 3D world. This class must listen for grid changes in order to spawn cell prefabs when a new level is loaded.

Here, spawning cell prefabs are illustrated by a simple log in Unity console.

```cs
using UnityEngine;
public class PuzzleGridRenderer : MonoBehaviour
{
    private PuzzleGridManager _gridManager;

    private void Awake()
    {
        _gridManager = FindObjectOfType<PuzzleGridManager>();
    }

    private void OnEnable()
    {
        _gridManager.grid.OnGridChange += RespawnCells;
    }

    private void OnDisable()
    {
        _gridManager.grid.OnGridChange -= RespawnCells;
    }

    public void RespawnCells(PuzzleCell[,] _Cells)
    {
        Debug.Log("Respawn cell prefabs");
    }
}
```

### Test

Place the two `MonoBehaviour` scripts in a scene, and go to play mode. You can see the "Respawn cell prefabs" log each time you press the space bar, and so each time a level is loaded.

## Methods

```cs
public virtual void Notify()
```

Triggers the observable's changes event.

---

```cs
protected abstract void HandleChanges(ObservableChanges<T> _Changes)
```

Called when changes event is triggered.

---

```cs
protected virtual void SetValue(T _NewValue, bool _ForceEvent = false)
```

Sets this observable's value.

* `T _NewValue`: The new value of this observable
* `bool _ForceEvent = false`: If true, the changes event will be triggered even if the given new value is the same as the current one

## Accessors

```cs
public T Value { get; set; }
```

Gets this observable's value.

Sets this observable's value, and triggers changes event if the value has really changed. Note that this accessor uses `SetValue(T, false)` method.