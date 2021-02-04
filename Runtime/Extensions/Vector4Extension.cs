using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for UnityEngine.Vector4 values.
    ///</summary>
    public static class Vector4Extension
	{

        /// <summary>
        /// Returns a new Vector4 instance with its values superior or equal to the given minimum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Min">The minimum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector4 Min(this Vector4 _Vector, float _Min)
        {
            return new Vector4
            (
                Mathf.Max(_Vector.x, _Min),
                Mathf.Max(_Vector.y, _Min),
                Mathf.Max(_Vector.z, _Min),
                Mathf.Max(_Vector.w, _Min)
            );
        }

        /// <summary>
        /// Returns a new Vector4 instance with its values inferior or equal to the given maximum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Max">The maximum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector4 Max(this Vector4 _Vector, float _Max)
        {
            return new Vector4
            (
                Mathf.Min(_Vector.x, _Max),
                Mathf.Min(_Vector.y, _Max),
                Mathf.Min(_Vector.z, _Max),
                Mathf.Min(_Vector.w, _Max)
            );
        }

    }

}