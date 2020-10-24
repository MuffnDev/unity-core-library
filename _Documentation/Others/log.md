# Muffin Dev for Unity - `Log`

Container for log entry.

## Constructor

```cs
public Log(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
```

Creates an instance of `Log` with the given content.

* `ELogType _Type = ELogType.Log`: Defines the log level
* `bool _AvailableForUser = false`: Defines if this log can be displayed to the user

## Accessors

```cs
public string Content { get; set; }
```

Gets or sets the content of this log entry.

---

```cs
public ELogType Type { get; set; }
```

Gets or sets the level of this log entry.

---

```cs
public bool AvailableForUser { get; set; }
```

Checks or defines if this log entry can be displayed to the user.