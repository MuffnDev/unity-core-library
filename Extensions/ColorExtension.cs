using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Extensions for Color class.
    /// </summary>
    public static class ColorExtension
    {

        /// <summary>
        /// Converts the given Color into an hexadecimal value as string.
        /// </summary>
        /// <returns>Returns the "Color string" with format "RRGGBB".</returns>
        public static string ToHexRGB(this Color _Color)
        {
            return ColorUtility.ToHtmlStringRGB(_Color);
        }

        /// <summary>
        /// Converts the given Color into an hexadecimal value as string.
        /// </summary>
        /// <returns>Returns the "Color string" with format "RRGGBBAA".</returns>
        public static string ToHexRGBA(this Color _Color)
        {
            return ColorUtility.ToHtmlStringRGBA(_Color);
        }

    }

}