using System;

using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Defines a color, using flags or prepared blends.
    /// You can resolve the Color instance using Colors.Get().
    /// </summary>
    [Flags]
    public enum EColor
    {
        // RVB components
        Red = 1,
        Maroon = 2,
        Lime = 4,
        Green = 8,
        Blue = 16,
        Navy = 32,
        
        // Tints
        Black = 0,
        Grey = Maroon | Green | Navy,
        White = Red | Lime | Blue,

        // Special
        Alpha0 = 64,
        Alpha14 = 128,
        Alpha25 = 256,
        Alpha50 = 512,
        Alpha75 = 1024,
        Alpha87 = 2048,

        // Other colors
        Orange = Red | Green,
        Yellow = Red | Lime,
        Olive = Maroon | Green,
        Purple = Maroon | Navy,
        Magenta = Red | Blue,
        Pink = Red | Green | Blue,
        Teal = Green | Navy,
        Azure = Green | Blue,
        Cyan = Lime | Blue,

        // Aliases
        Fuchsia = Magenta,
        Aqua = Cyan,
    }

    /// <summary>
    /// Basic colors utility class.
    /// </summary>
    public static class Colors
	{

        #region Properties

        public static readonly Color aqua = Get(EColor.Aqua);
        public static readonly Color azure = Get(EColor.Azure);
        public static readonly Color black = Get(EColor.Black);
        public static readonly Color blue = Get(EColor.Blue);
        public static readonly Color cyan = Get(EColor.Cyan);
        public static readonly Color fuchsia = Get(EColor.Fuchsia);
        public static readonly Color green = Get(EColor.Green);
        public static readonly Color grey = Get(EColor.Grey);
        public static readonly Color lime = Get(EColor.Lime);
        public static readonly Color magenta = Get(EColor.Magenta);
        public static readonly Color maroon = Get(EColor.Maroon);
        public static readonly Color navy = Get(EColor.Navy);
        public static readonly Color olive = Get(EColor.Olive);
        public static readonly Color orange = Get(EColor.Orange);
        public static readonly Color pink = Get(EColor.Pink);
        public static readonly Color purple = Get(EColor.Purple);
        public static readonly Color red = Get(EColor.Red);
        public static readonly Color teal = Get(EColor.Teal);
        public static readonly Color white = Get(EColor.White);
        public static readonly Color yellow = Get(EColor.Yellow);

        #endregion


        #region Public Methods

        /// <summary>
        /// Resolve Color instance using given color blend preset or flags (based on Color.black).
        /// </summary>
        public static Color Get(EColor _Color)
        {
            Color color = Color.black;

            if ((_Color & EColor.Red) != 0) { color.r = 1f; }
            else if ((_Color & EColor.Maroon) != 0) { color.r = 0.5f; }

            if ((_Color & EColor.Lime) != 0) { color.g = 1f; }
            else if ((_Color & EColor.Green) != 0) { color.g = 0.5f; }

            if ((_Color & EColor.Blue) != 0) { color.b = 1f; }
            else if ((_Color & EColor.Navy) != 0) { color.b = 0.5f; }

            if ((_Color & EColor.Alpha0) != 0) { color.a = 0f; }
            else if ((_Color & EColor.Alpha14) != 0) { color.a = 0.14f; }
            else if ((_Color & EColor.Alpha25) != 0) { color.a = 0.25f; }
            else if ((_Color & EColor.Alpha50) != 0) { color.a = 0.5f; }
            else if ((_Color & EColor.Alpha75) != 0) { color.a = 0.75f; }
            else if ((_Color & EColor.Alpha87) != 0) { color.a = 0.87f; }

            return color;
        }

        #endregion

    }

}