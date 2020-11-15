using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    /// Extensions for UnityEngine.Object objects.
    ///</summary>
    public static class ObjectExtension
	{

        /// <summary>
        /// Gets the GUID string of this Object.
        /// Note that GUIDs are assigned only to assets, not to scene objects. So if this Object is a scene object, this method will return
        /// null.
        /// WARNING: This method is meant to be used in Editor. So in build, this method will always return null.
        /// </summary>
        /// <param name="_Obj">The Object whose GUID you want to get.</param>
        /// <returns>Returns the GUID string of this Object, or null if this Object is not an asset (meaning it's a scene object).</returns>
        public static string GetGUID(this Object _Obj)
        {
            #if UNITY_EDITOR
            UnityEditor.AssetDatabase.TryGetGUIDAndLocalFileIdentifier(_Obj, out string guid, out long localId);
            return localId != 0 ? guid : null;
            #else
            return null;
            #endif
        }

        /// <summary>
        /// Checks if this Object is an asset (and not a scene object).
        /// WARNING: This method is meant to be used in Editor. So in build, this method will always return false.
        /// </summary>
        /// <returns>Returns true if the given Object is an asset, otherwise false.</returns>
        public static bool IsAsset(this Object _Obj)
        {
            return GetGUID(_Obj) != null;
        }

	}

}