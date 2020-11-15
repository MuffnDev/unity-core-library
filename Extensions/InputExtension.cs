using UnityEngine;

namespace MuffinDev
{

	///<summary>
	/// Extensions for Unity's Input class.
	///</summary>
	public static class InputExtension
	{

		public static Vector2 RelativeMousePosition
        {
            get
            {
                return new Vector2
                (
                    Input.mousePosition.x / Screen.width,
                    Input.mousePosition.y / Screen.height
                );
            }
        }

	}

}