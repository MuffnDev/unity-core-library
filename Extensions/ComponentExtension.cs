using System;

using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    /// Extensions for Component objects.
    ///</summary>
    public static class ComponentExtension
	{

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
		public static T GetComponentFromRoot<T>(this Component _MonoBehaviour)
            where T : Component
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot<T>();
        }

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this Component _MonoBehaviour, Type _ComponentType)
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot(_ComponentType);
        }

        /// <summary>
        /// Gets the component of the named type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this Component _MonoBehaviour, string _ComponentName)
        {
            return _MonoBehaviour.gameObject.GetComponentFromRoot(_ComponentName);
        }

    }

}