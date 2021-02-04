using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for UnityEngine.Vector2 values.
    ///</summary>
    public static class Vector2Extension
	{

        /// <summary>
        /// Returns a new Vector2 instance with its values superior or equal to the given minimum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Min">The minimum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector2 Min(this Vector2 _Vector, float _Min)
        {
            return new Vector2
            (
                Mathf.Max(_Vector.x, _Min),
                Mathf.Max(_Vector.y, _Min)
            );
        }

        /// <summary>
        /// Returns a new Vector2 instance with its values inferior or equal to the given maximum value.
        /// </summary>
        /// <param name="_Vector">The input vector to compute.</param>
        /// <param name="_Max">The maximum value of the given vector.</param>
        /// <returns>Returns the computed vector.</returns>
        public static Vector2 Max(this Vector2 _Vector, float _Max)
        {
            return new Vector2
            (
                Mathf.Min(_Vector.x, _Max),
                Mathf.Min(_Vector.y, _Max)
            );
        }

    }

}