# Muffin Dev for Unity - `OperationState`

Represents the state of an operation.

It contains a list of `Log` and the information if the operation result is valid (see `IsOk` accessor). Note that an `OperationState` instance can be used as boolean, so it returns `IsOk` value.

## Methods

```cs
public void AddLog(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
```

Adds a `Log` entry to the list.

* `ELogType _Type = ELogType.Log`: Defines the log level
* `bool _AvailableForUser = false`: Defines if this log can be displayed to the user

---

```cs
public void DisplayLogs()
```

Display all logs in the list in Unity console.

## Accessors

```cs
public bool IsOk { get; set; }
```

Checks of defines if this `OperationState` is valid.

---

```cs
public Log[] Logs { get; set; }
```

Gets or sets the list of logs.

---

```cs
public Log[] GetUserLogs()
```

Gets all logs available for users.

## Operators

```cs
public static implicit operator bool(OperationState _Operation)
```

Implicit conversion into a boolean. Returns the vaue of `IsOk` accessor.