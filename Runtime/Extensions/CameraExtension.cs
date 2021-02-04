using UnityEngine;
using System.Collections.Generic;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for Unity's Input class.
    ///</summary>
    public static class CameraExtension
	{

        /// <summary>
        /// Calculates the camera render extents, using orthographic mode.
        /// The extents are Vector2(orthographic size * aspect ratio, orthographic size).
        /// You can use this value to automatically define screen limits, useful for mobile games.
        /// </summary>
        public static Vector2 GetExtentsOrthographic(this Camera _Camera)
        {
            if (_Camera.orthographic)
            {
                return new Vector2
                (
                    _Camera.orthographicSize * _Camera.aspect,
                    _Camera.orthographicSize
                );
            }
            else
            {
                Debug.LogFormat("This camera ({0}) must be turned to orthographic mode in order to use GetScreenBounds() method.", _Camera.name);
                return Vector2.zero;
            }
        }

        /// <summary>
        /// Calculates the camera render extends using GetExtentsOrthographic() and double it to get bounds, using orthographic mode.
        /// </summary>
        public static Vector2 GetBoundsOrthographic(this Camera _Camera)
        {
            return _Camera.GetExtentsOrthographic() * 2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Camera"></param>
        /// <param name="_RefPosition"></param>
        /// <returns></returns>
        [System.Obsolete("This method is not complete. Its implementation is an example, and needs to be reworked and tested.")]
        public static KeyValuePair<float, float> GetBounds(this Camera _Camera, Vector3 _RefPosition)
        {
            // Gets the distance vector to the reference position
            float zDistance = _RefPosition.z - _Camera.transform.position.z;

            // Calculate the left limit on x axis
            float leftLimit = _Camera.ScreenToWorldPoint(new Vector3(
                0,
                0,
                zDistance / (Mathf.Cos(_Camera.transform.localEulerAngles.x * Mathf.Deg2Rad))
            )).x;

            // Calculate the right limit on x axis
            float rightLimit = _Camera.ScreenToWorldPoint(new Vector3(
                Screen.width,
                0,
                zDistance / (Mathf.Cos(_Camera.transform.localEulerAngles.x * Mathf.Deg2Rad))
            )).x;

            // Return left and right limits as "key value pair" instance
            return new KeyValuePair<float, float>(leftLimit, rightLimit);
        }

	}

}