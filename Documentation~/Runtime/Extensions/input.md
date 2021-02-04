# Muffin Dev for Unity - `InputExtension`

Extension methods for `Input` class.

Note that the `Input` utility class can't be used if you have installed the [*Input System* package](docs.unity3d.com/Packages/com.unity.inputsystem@1.0).

## Public API

### Accessors

#### `RelativeMousePosition`

```cs
public static Vector2 RelativeMousePosition { get; }
```

Gets the ratio of mouse position on screen size.