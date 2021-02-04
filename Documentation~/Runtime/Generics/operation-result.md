# Muffin Dev for Unity - `OperationResult`

Represents the result of an operation.

It allows you to return multiple logs and a result for a method call.

## Usage

```cs
public struct ComputePlayerPositionResult : OperationResult<Vector3> { }

public class PlayersManager
{
    public ComputePlayerPositionResult ComputePlayerPosition(Player player)
    {
        ComputePlayerPositionResult result = new ComputePlayerPositionResult();

        if(player == null)
        {
            result.AddLog("Given player is null", ELogType.Warning);
        }

        else
        {
            Vector3 position = player.transform.position;
            result.AddLog("Given player is at " + position);
            result.AddLog("Player found!", ELogType.Log, true);
            result.Result = position;
            result.IsOk = true;
        }

        return result;
    }
}
```

## Structure

```cs
public struct OperationResult<T>
```

* `T`: Type of the result of a method call

## Methods

```cs
public void AddLog(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
```

Calls `AddLog()` method of the contained [`OperationState`](../Others/operation-state.md).

---

```cs
public void DisplayLogs()
```

Displays all logs in the contained [`OperationState`](../Others/operation-state.md).

## Accessors

```cs
public OperationState State { get; set; }
```

Gets or sets the operation state.

---

```cs
public T Result { get; set; }
```

Gets or sets the result of this operation.

---

```cs
public bool IsOk { get; set; }
```

Checks or defines if this operation is valid.

---

```cs
public Log[] Logs { get; set; }
```

Gets or sets the logs in the contained [`OperationState`](../Others/operation-state.md).

## Operators

```cs
public static implicit operator bool(OperationResult<T> _Operation)
```

Implicit conversion into a boolean, using the contained [`OperationState`](../Others/operation-state.md) as boolean.