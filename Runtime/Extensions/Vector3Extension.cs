using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for UnityEngine.Vector3 values.
    ///</summary>
    public static class Vector3Extension
	{

        /// <summary>
        /// Returns a new Vector3 instance with its values superior or equal to the given minimum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Min">The minimum value of given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector3 Min(this Vector3 _Vector, float _Min)
        {
            return new Vector3
            (
                Mathf.Max(_Vector.x, _Min),
                Mathf.Max(_Vector.y, _Min),
                Mathf.Max(_Vector.z, _Min)
            );
        }

        /// <summary>
        /// Returns a new Vector3 instance with its values inferior or equal to the given maximum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Max">The maximum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector3 Max(this Vector3 _Vector, float _Max)
        {
            return new Vector3
            (
                Mathf.Min(_Vector.x, _Max),
                Mathf.Min(_Vector.y, _Max),
                Mathf.Min(_Vector.z, _Max)
            );
        }

    }

}