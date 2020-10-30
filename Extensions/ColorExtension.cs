using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Extensions for Color class.
    /// </summary>
    public static class ColorExtension
    {

        public const float MAX_COLOR_VALUE = 255f;

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

        /// <summary>
        /// Gets a Color value from an hexadecimal string.
        /// </summary>
        public static Color FromHex(string _HexadecimalString)
        {
            _HexadecimalString = _HexadecimalString.Replace("#", "");
            if (_HexadecimalString.Length < 8)
            {
                _HexadecimalString += "ff";
            }

            string[] split = new string[4];
            try
            {
                split[0] = _HexadecimalString.Substring(0, 2);
                split[1] = _HexadecimalString.Substring(2, 2);
                split[2] = _HexadecimalString.Substring(4, 2);
                split[3] = _HexadecimalString.Substring(6, 2);
            }
            catch(Exception)
            {
                return Color.white;
            }

            return new Color
            (
                int.Parse(split[0], System.Globalization.NumberStyles.HexNumber) / MAX_COLOR_VALUE,
                int.Parse(split[1], System.Globalization.NumberStyles.HexNumber) / MAX_COLOR_VALUE,
                int.Parse(split[2], System.Globalization.NumberStyles.HexNumber) / MAX_COLOR_VALUE,
                int.Parse(split[3], System.Globalization.NumberStyles.HexNumber) / MAX_COLOR_VALUE
            );
        }

        /// <summary>
        /// Gets a Color value from an hexadecimal string.
        /// </summary>
        public static Color FromHex(this Color _Color, string _HexadecimalString)
        {
            return FromHex(_HexadecimalString);
        }

    }

}