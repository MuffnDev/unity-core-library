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

        /// <summary>
        /// Find an object by name, recursively in the source's hierarchy.
        /// </summary>
        /// <param name="_Obj">The object from which you want to find the named object.</param>
        /// <param name="_Name">The name of the object you want to find.</param>
        /// <param name="_IncludeSelf">If enabled, the input object is included in the research.</param>
        /// <param name="_IncludeInactive">If enabled, this method will also search if the name of disabled objects match.</param>
        /// <returns>Returns the found object, or null if the source doesn't contain any object with the given name in its
        /// hierarchy.</returns>
        public static Transform Find(this Transform _Obj, string _Name, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            // If the given object is inactive but inactive are discarded from search, cancel
            if (!_IncludeInactive && !_Obj.gameObject.activeSelf)
                return null;

            // If the given object's name matches with the given name, return given object
            if(_Obj.name == _Name)
                return _Obj;

            Transform target = null;
            // Foreach child of the given object
            foreach(Transform child in _Obj)
            {
                // If the current child is the input object, skip
                if (child == _Obj)
                    continue;

                target = Find(child, _Name, _IncludeInactive);
                if (target != null)
                    break;
            }

            return target;
        }

    }

}