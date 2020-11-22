using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    /// Extensions for Unity's Input class.
    /// Note that the `Input` utility class can't be used if you have installed the Input System package.
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