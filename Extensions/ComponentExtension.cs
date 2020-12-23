using System;
using System.Reflection;

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
		public static T GetComponentFromRoot<T>(this Component _Component)
            where T : Component
        {
            return _Component.gameObject.GetComponentFromRoot<T>();
        }

        /// <summary>
        /// Gets the component of the given type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this Component _Component, Type _ComponentType)
        {
            return _Component.gameObject.GetComponentFromRoot(_ComponentType);
        }

        /// <summary>
        /// Gets the component of the named type from the root GameObject of the hierarchy.
        /// </summary>
        /// <returns>Returns the found component, otherwise null.</returns>
        public static Component GetComponentFromRoot(this Component _Component, string _ComponentTypeName)
        {
            return _Component.gameObject.GetComponentFromRoot(_ComponentTypeName);
        }

        /// <summary>
        /// Copy the values of the original component into the given target one.
        /// </summary>
        /// <typeparam name="T">The type of the component to copy.</typeparam>
        /// <param name="_Original">The original component.</param>
        /// <param name="_Target">The target component, on which to set the original values.</param>
        /// <param name="_IgnoreTargetProperties">If true, all properties and fields will be copied BUT the target properties list.</param>
        /// <param name="_TargetProperties">Defines the names of the properties or fields you want to copy.</param>
        public static void CopyTo<T>(this T _Original, T _Target, bool _IgnoreTargetProperties, params string[] _TargetProperties)
            where T : Component
        {
            CopyTo(_Original as Component, _Target as Component, _TargetProperties, _IgnoreTargetProperties);
        }

        /// <summary>
        /// Copy the values of the original component into the given target one.
        /// </summary>
        /// <typeparam name="T">The type of the component to copy.</typeparam>
        /// <param name="_Original">The original component.</param>
        /// <param name="_Target">The target component, on which to set the original values.</param>
        /// <param name="_TargetProperties">Defines the names of the properties or fields you want to copy.</param>
        /// <param name="_IgnoreTargetProperties">If true, all properties and fields will be copied BUT the target properties list.</param>
        public static void CopyTo<T>(this T _Original, T _Target, string[] _TargetProperties = null, bool _IgnoreTargetProperties = false)
            where T : Component
        {
            CopyTo(_Original as Component, _Target as Component, _TargetProperties, _IgnoreTargetProperties);
        }

        /// <summary>
        /// Copy the values of the original component into the given target one. Note that if the types doesn't match, only the matching
        /// properties will be copied.
        /// </summary>
        /// <param name="_Original">The original component.</param>
        /// <param name="_Target">The target component, on which to set the original values.</param>
        /// <param name="_IgnoreTargetProperties">If true, all properties and fields will be copied BUT the target properties list.</param>
        /// <param name="_TargetProperties">Defines the names of the properties or fields you want to copy.</param>
        public static void CopyTo(this Component _Original, Component _Target, bool _IgnoreTargetProperties, params string[] _TargetProperties)
        {
            CopyTo(_Original, _Target, _TargetProperties, _IgnoreTargetProperties);
        }

        /// <summary>
        /// Copy the values of the original component into the given target one. Note that if the types doesn't match, only the matching
        /// properties will be copied.
        /// </summary>
        /// <param name="_Original">The original component.</param>
        /// <param name="_Target">The target component, on which to set the original values.</param>
        /// <param name="_TargetProperties">Defines the names of the properties or fields you want to copy.</param>
        /// <param name="_IgnoreTargetProperties">If true, all properties and fields will be copied BUT the target properties list.</param>
        public static void CopyTo(this Component _Original, Component _Target, string[] _TargetProperties = null, bool _IgnoreTargetProperties = false)
        {
#if UNITY_EDITOR
            if (UnityEditorInternal.ComponentUtility.CopyComponent(_Original))
            {
                UnityEditorInternal.ComponentUtility.PasteComponentValues(_Target);
            }
#else
            Type originalType = _Original.GetType();
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;

            // Write properties (use setters)
            PropertyInfo[] properties = originalType.GetProperties(flags);
            foreach (PropertyInfo property in properties)
            {
                // Skip if the property can't be written
                if (!property.CanWrite)
                    continue;

                // Skip if the property is obsolete
                bool obsolete = false;
                foreach (CustomAttributeData attribute in property.CustomAttributes)
                {
                    if (attribute.AttributeType == typeof(ObsoleteAttribute))
                    {
                        obsolete = true;
                        break;
                    }
                }
                if (obsolete)
                    continue;

                // Skip if the property should be ignored
                if (!ShouldCopyProperty(property.Name, _TargetProperties, _IgnoreTargetProperties))
                    continue;

                try { property.SetValue(_Target, property.GetValue(_Original, null), null); }
                catch { }
            }

            // Write fields
            FieldInfo[] fields = originalType.GetFields(flags);
            foreach(FieldInfo field in fields)
            {
                if (!ShouldCopyProperty(field.Name, _TargetProperties, _IgnoreTargetProperties))
                    continue;

                try { field.SetValue(_Target, field.GetValue(_Original)); }
                catch { }
            }
#endif
        }

        /// <summary>
        /// Checks if the named property should be copied when using CopyTo() method, depending on the given targets and settings.
        /// </summary>
        /// <param name="_PropertyName">The name of the current property to copy.</param>
        /// <param name="_TargetProperties">The list of the properties that should be copied.</param>
        /// <param name="_IgnoreTargetProperties">If true, the target properties list becomes the "ignored properties" list.</param>
        /// <returns>Returns true if the named property should be copied, otherwise false.</returns>
        private static bool ShouldCopyProperty(string _PropertyName, string[] _TargetProperties, bool _IgnoreTargetProperties)
        {
            // Returns true if the target properties list is not valid
            if (_TargetProperties == null || _TargetProperties.Length == 0)
                return true;

            foreach (string targetProperty in _TargetProperties)
            {
                // If the property is in the list of targets
                if (targetProperty == _PropertyName)
                    // Returns true if the property is a target and the targets are not ignored
                    return !_IgnoreTargetProperties;
            }

            // If the property is not in the list of targets, returns true if targets are ignored, otherwise false
            return _IgnoreTargetProperties;
        }

    }

}