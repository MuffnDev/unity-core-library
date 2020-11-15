using System;

namespace MuffinDev
{

    ///<summary>
    /// Extensions for arrays.
    ///</summary>
    public static class ArrayExtension
	{

        /// <summary>
        /// Checks if the given index is in the array's range (so greater than or equal to 0, and less than or equal to the array's length.
        /// </summary>
        /// <param name="_Index">The index you want to check.</param>
        /// <returns>Returns true if the given index is in the array's range, otherwise false.</returns>
        public static bool IsInRange(this Array _Array, int _Index)
        {
            return (_Index >= 0 && _Index < _Array.Length);
        }

    }

}