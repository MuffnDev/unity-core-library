using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for UnityEngine.Vector3Int values.
    ///</summary>
    public static class Vector3IntExtension
	{

        /// <summary>
        /// Returns a new Vector3Int instance with its values superior or equal to the given minimum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Min">The minimum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector3Int Min(this Vector3Int _Vector, float _Min)
        {
            int min = Mathf.FloorToInt(_Min);
            return new Vector3Int
            (
                Mathf.Max(_Vector.x, min),
                Mathf.Max(_Vector.y, min),
                Mathf.Max(_Vector.z, min)
            );
        }

        /// <summary>
        /// Returns a new Vector3Int instance with its values inferior or equal to the given maximum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Max">The maximum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector3Int Max(this Vector3Int _Vector, float _Max)
        {
            int max = Mathf.FloorToInt(_Max);
            return new Vector3Int
            (
                Mathf.Min(_Vector.x, max),
                Mathf.Min(_Vector.y, max),
                Mathf.Min(_Vector.z, max)
            );
        }

    }

}