# Muffin Dev for Unity - `CameraExtension`

Extension methods for `Camera` instances.

## Methods

```cs
public static Vector2 GetExtentsOrthographic(this Camera _Camera)
```

Calculates the camera render extents, using orthographic mode. The extents are:

`Vector2(orthographic_size * aspect_ratio, orthographic_size)`

---

```cs
public static Vector2 GetBoundsOrthographic(this Camera _Camera)
```

Calculates the camera render extends using GetExtentsOrthographic() and double it to get bounds, and so the screen limits, using orthographic mode.