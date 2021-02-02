using System;
using System.Linq;
using System.Collections.Generic;

using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for GameObject objects.
    ///</summary>
    public static class GameObjectExtension
    {

        /// <summary>
        /// Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <typeparam name="T">The type of the component you want to get.</typeparam>
        /// <param name="_IncludeSelf">If enabled, try get component on the source object.</param>
        /// <param name="_IncludeInactive">If enabled, try get component on disabled objects.</param>
        /// <returns>Returns the first component of the given type found in the hierarchy.</returns>
        public static T GetComponentInHierarchy<T>(this GameObject _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
            where T : Component
        {
            return GetComponentInHierarchy(_Obj, typeof(T), _IncludeSelf, _IncludeInactive) as T;
        }

        /// <summary>
        /// Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <param name="_ComponentType">The type of the component you want to get.</param>
        /// <param name="_IncludeSelf">If enabled, try get component on the source object.</param>
        /// <param name="_IncludeInactive">If enabled, try get component on disabled objects.</param>
        /// <returns>Returns the first component of the given type found in the hierarchy.</returns>
        public static Component GetComponentInHierarchy(this GameObject _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            Queue<Transform> hierarchy = new Queue<Transform>();

            Component comp = GetComponentInHierarchy(_ComponentType, _Obj.transform, hierarchy, _IncludeSelf, _IncludeInactive);
            while (comp == null && hierarchy.Count > 0)
            {
                comp = GetComponentInHierarchy(_ComponentType, hierarchy.Dequeue(), hierarchy, false, _IncludeInactive);
            }

            return comp;
        }

        /// <summary>
        /// Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <param name="_ComponentType">The type of the component you want to find.</param>
        /// <param name="_Source">The source object from which you want to get the components.</param>
        /// <param name="_Hierarchy">The eventual children in which to get components at next iteration.</param>
        /// <param name="_IncludeSelf">If enabled, try get component on the source object.</param>
        /// <param name="_IncludeInactive">If enabled, try get component on disabled objects.</param>
        /// <returns>Returns the first component of the given type found in the hierarchy.</returns>
        private static Component GetComponentInHierarchy(Type _ComponentType, Transform _Source, Queue<Transform> _Hierarchy, bool _IncludeSelf, bool _IncludeInactive)
        {
            foreach (Transform child in _Source)
            {
                // Skip if the current child is this object, but user doesn't want to include it
                if (child == _Source && !_IncludeSelf)
                    continue;

                // Skip if the current child is inactive and the inactive objects are discarded
                if (!_IncludeInactive && !child.gameObject.activeInHierarchy)
                    continue;

                // Add all components of the expected type to the output list
                if (child.TryGetComponent(_ComponentType, out Component comp))
                    return comp;

                // Register the child for the next iteration
                if (child.childCount > 0 && child != _Source)
                    _Hierarchy.Enqueue(child);
            }

            return null;
        }

        /// <summary>
        /// Gets all components in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <typeparam name="T">The type of the components you want to get.</typeparam>
        /// <param name="_IncludeSelf">If enabled, includes the given source object in the resulting list.</param>
        /// <param name="_IncludeInactive">If enabled, includes the inactive objects in the resulting list.</param>
        /// <returns>Returns all the components of the given type found in the hierarchy.</returns>
        public static T[] GetComponentsInHierarchy<T>(this GameObject _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
            where T : Component
        {
            return GetComponentsInHierarchy(_Obj, typeof(T), _IncludeSelf, _IncludeInactive)
                .Cast<T>()
                .ToArray();
        }
        
        /// <summary>
        /// Gets all components in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <param name="_ComponentType">The type of the components you want to get.</param>
        /// <param name="_IncludeSelf">If enabled, includes the given source object in the resulting list.</param>
        /// <param name="_IncludeInactive">If enabled, includes the inactive objects in the resulting list.</param>
        /// <returns>Returns all the components of the given type found in the hierarchy.</returns>
        public static Component[] GetComponentsInHierarchy(this GameObject _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            List<Component> output = new List<Component>();
            Queue<Transform> hierarchy = new Queue<Transform>();

            GetComponentsInHierarchy(_ComponentType, _Obj.transform, output, hierarchy, _IncludeSelf, _IncludeInactive);
            while (hierarchy.Count > 0)
            {
                GetComponentsInHierarchy(_ComponentType, hierarchy.Dequeue(), output, hierarchy, false, _IncludeInactive);
            }

            return output.ToArray();
        }

        /// <summary>
        /// Gets all components on the given source object (if Include Self parameter is enabled) and in its hierarchy.
        /// </summary>
        /// <param name="_ComponentType">The type of the components you want to find.</param>
        /// <param name="_Source">The source object from which you want to get the components.</param>
        /// <param name="_Output">The list of found components.</param>
        /// <param name="_Hierarchy">The eventual children in which to get components at next iteration.</param>
        /// <param name="_IncludeSelf">If enabled, includes the given source object in the resulting list.</param>
        /// <param name="_IncludeInactive">If enabled, includes the inactive objects in the resulting list.</param>
        private static void GetComponentsInHierarchy(Type _ComponentType, Transform _Source, List<Component> _Output, Queue<Transform> _Hierarchy, bool _IncludeSelf, bool _IncludeInactive)
        {
            foreach (Transform child in _Source)
            {
                // Skip if the current child is this object, but user doesn't want to include it
                if (child == _Source && !_IncludeSelf)
                    continue;

                // Skip if the current child is inactive and the inactive objects are discarded
                if (!_IncludeInactive && !child.gameObject.activeInHierarchy)
                    continue;

                // Add all components of the expected type to the output list
                _Output.AddRange(child.GetComponents(_ComponentType));
                // Register the child for the next iteration
                if (child.childCount > 0 && child != _Source)
                    _Hierarchy.Enqueue(child);
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
        public static GameObject Find(this GameObject _Obj, string _Name, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            Transform target = TransformExtension.Find(_Obj.transform, _Name, _IncludeSelf, _IncludeInactive);
            return target != null ? target.gameObject : null;
        }

    }

}