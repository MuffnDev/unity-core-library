using System;

using UnityEngine;

namespace MuffinDev.Core
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

        public static readonly Color Aqua = Get(EColor.Aqua);
        public static readonly Color Azure = Get(EColor.Azure);
        public static readonly Color Black = Get(EColor.Black);
        public static readonly Color Blue = Get(EColor.Blue);
        public static readonly Color Cyan = Get(EColor.Cyan);
        public static readonly Color Fuchsia = Get(EColor.Fuchsia);
        public static readonly Color Green = Get(EColor.Green);
        public static readonly Color Grey = Get(EColor.Grey);
        public static readonly Color Lime = Get(EColor.Lime);
        public static readonly Color Magenta = Get(EColor.Magenta);
        public static readonly Color Maroon = Get(EColor.Maroon);
        public static readonly Color Navy = Get(EColor.Navy);
        public static readonly Color Olive = Get(EColor.Olive);
        public static readonly Color Orange = Get(EColor.Orange);
        public static readonly Color Pink = Get(EColor.Pink);
        public static readonly Color Purple = Get(EColor.Purple);
        public static readonly Color Red = Get(EColor.Red);
        public static readonly Color Teal = Get(EColor.Teal);
        public static readonly Color White = Get(EColor.White);
        public static readonly Color Yellow = Get(EColor.Yellow);

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