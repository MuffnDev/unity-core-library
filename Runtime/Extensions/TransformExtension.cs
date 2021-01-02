using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for Transform component.
    ///</summary>
    public static class TransformExtension
	{

        /// <summary>
        /// Destroys all child GameObjects of this Transform.
        /// </summary>
        public static void ClearHierarchy(this Transform _Transform)
        {
            foreach(Transform child in _Transform)
            {
                if (child == _Transform)
                    continue;

                Object.Destroy(child.gameObject);
            }
        }

    }

}