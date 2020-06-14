using System;

using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    /// Extensions for MonoBehaviour objects.
    ///</summary>
    public static class MonoBehaviourExtension
	{

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
		public static T GetComponentFromRoot<T>(this MonoBehaviour _MonoBehaviour)
            where T : Component
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot<T>();
        }

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, Type _ComponentType)
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot(_ComponentType);
        }

        /// <summary>
        /// Gets the component of the named type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this MonoBehaviour _MonoBehaviour, string _ComponentName)
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot(_ComponentName);
        }

    }

}