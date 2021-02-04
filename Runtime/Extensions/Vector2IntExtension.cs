using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for UnityEngine.Vector2Int values.
    ///</summary>
    public static class Vector2IntExtension
	{

        /// <summary>
        /// Returns a new Vector2Int instance with its values superior or equal to the given minimum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Min">The minimum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector2Int Min(this Vector2Int _Vector, float _Min)
        {
            int min = Mathf.FloorToInt(_Min);
            return new Vector2Int
            (
                Mathf.Max(_Vector.x, min),
                Mathf.Max(_Vector.y, min)
            );
        }

        /// <summary>
        /// Returns a new Vector2Int instance with its values inferior or equal to the given maximum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Max">The maximum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector2Int Max(this Vector2Int _Vector, float _Max)
        {
            int max = Mathf.FloorToInt(_Max);
            return new Vector2Int
            (
                Mathf.Min(_Vector.x, max),
                Mathf.Min(_Vector.y, max)
            );
        }

    }

}