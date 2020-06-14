using UnityEngine;

namespace MuffinDev
{

	///<summary>
	/// Extensions for UnityEngine.Rect struct.
	///</summary>
	public static class RectExtension
	{

        /// <summary>
        /// Keep the Rect inside the given "space" Rect.
        /// </summary>
        /// <returns>Returns the modified Rect.</returns>
		public static Rect HoldInSpace(this Rect _Rect, Rect _Space)
        {
            if (_Rect.x < _Space.x) { _Rect.x = _Space.x; }
            if (_Rect.y < _Space.y) { _Rect.y = _Space.y; }
            if (_Rect.y + _Rect.height > _Space.y + _Space.height) { _Rect.y = _Space.y + _Space.height - _Rect.height; }
            if (_Rect.x + _Rect.width > _Space.x + _Space.width) { _Rect.x = _Space.x + _Space.width - _Rect.width; }

            return _Rect;
        }

        /// <summary>
        /// Keep the Rect inside the Screen space.
        /// </summary>
        /// <returns>Returns the modified Rect.</returns>
        public static Rect HoldInScreenSpace(this Rect _Rect)
        {
            Vector2 screeSize = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            return _Rect.HoldInSpace(new Rect(Vector2.zero, screeSize));
        }

    }

}