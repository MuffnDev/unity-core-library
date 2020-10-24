using System;

namespace MuffinDev
{

	///<summary>
	/// Extensions for string objects.
	///</summary>
	public static class StringExtension
	{

        /// <summary>
        /// Formats the string for user display purposes:
        ///     - adds a space between words using camel case
        /// </summary>
        /// <returns>Returns the formatted string.</returns>
		public static string ToDisplayName(this string _String)
        {
            string finalName = string.Empty;
            finalName += _String[0];

            for(int i = 1; i < _String.Length - 1; i++)
            {
                if(char.IsUpper(_String[i]) && !char.IsUpper(_String[i + 1]))
                {
                    finalName += " ";
                }
                finalName += _String[i];
            }

            finalName += _String[_String.Length - 1];
            return finalName;
        }

        /// <summary>
        /// Checks if the given string exists, and exists only once in the given array.
        /// </summary>
        public static bool IsUnique(this string _String, string[] _Array)
        {
            bool found = false;
            foreach (string s in _Array)
            {
                if (s == _String)
                {
                    if (found) { return false; }
                    found = true;
                }
            }
            return found;
        }

        /// <summary>
        /// Shortcut for using string.Split() with one string as a separator.
        /// </summary>
        /// <param name="_String">The string to split.</param>
        /// <param name="_Separator">The separator to use for splitting the string.</param>
        /// <param name="_SplitOptions">Eventual split options.</param>
        /// <returns>Returns the splitted string.</returns>
        public static string[] Split(this string _String, string _Separator, StringSplitOptions _SplitOptions = StringSplitOptions.None)
        {
            return _String.Split(new string[1] { _Separator }, _SplitOptions);
        }

    }

}