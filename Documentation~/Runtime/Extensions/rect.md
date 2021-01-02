# Muffin Dev for Unity - `RectExtension`

Extension methods for `Rect` instances.

## Public API

### Methods

#### `HoldInSpace()`

```cs
public static Rect HoldInSpace(this Rect _Rect, Rect _Space)
```

Keep the Rect inside the given "space" Rect.

#### `HoldInScreenSpace()`

```cs
public static Rect HoldInScreenSpace(this Rect _Rect)
```

Keep the Rect inside the screen space.