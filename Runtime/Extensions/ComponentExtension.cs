using System;
using System.Reflection;

using UnityEngine;

using Object = UnityEngine.Object;

namespace MuffinDev.Core
{

    ///<summary>
    /// Extensions for Component objects.
    ///</summary>
    public static class ComponentExtension
	{

        /// <summary>
        /// Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <typeparam name="T">The type of the component you want to get.</typeparam>
        /// <param name="_IncludeSelf">If enabled, try get component on the source object.</param>
        /// <param name="_IncludeInactive">If enabled, try get component on disabled objects.</param>
        /// <returns>Returns the first component of the given type found in the hierarchy.</returns>
        public static T GetComponentInHierarchy<T>(this Component _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
            where T : Component
        {
            return GameObjectExtension.GetComponentInHierarchy<T>(_Obj.gameObject, _IncludeSelf, _IncludeInactive);
        }

        /// <summary>
        /// Gets a component in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <param name="_ComponentType">The type of the component you want to get.</param>
        /// <param name="_IncludeSelf">If enabled, try get component on the source object.</param>
        /// <param name="_IncludeInactive">If enabled, try get component on disabled objects.</param>
        /// <returns>Returns the first component of the given type found in the hierarchy.</returns>
        public static Component GetComponentInHierarchy(this Component _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            return GameObjectExtension.GetComponentInHierarchy(_Obj.gameObject, _ComponentType, _IncludeSelf, _IncludeInactive);
        }

        /// <summary>
        /// Gets all components in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <typeparam name="T">The type of the components you want to get.</typeparam>
        /// <param name="_IncludeSelf">If enabled, includes the given source object in the resulting list.</param>
        /// <param name="_IncludeInactive">If enabled, includes the inactive objects in the resulting list.</param>
        /// <returns>Returns all the components of the given type found in the hierarchy.</returns>
        public static T[] GetComponentsInHierarchy<T>(this Component _Obj, bool _IncludeSelf = true, bool _IncludeInactive = false)
            where T : Component
        {
            return GameObjectExtension.GetComponentsInHierarchy<T>(_Obj.gameObject, _IncludeSelf, _IncludeInactive);
        }

        /// <summary>
        /// Gets all components in the hierarchy, by iterating through "hierarchy levels" instead of plain recusivity as
        /// GetComponentsInChildren() would do.
        /// </summary>
        /// <param name="_ComponentType">The type of the components you want to get.</param>
        /// <param name="_IncludeSelf">If enabled, includes the given source object in the resulting list.</param>
        /// <param name="_IncludeInactive">If enabled, includes the inactive objects in the resulting list.</param>
        /// <returns>Returns all the components of the given type found in the hierarchy.</returns>
        public static Component[] GetComponentsInHierarchy(this Component _Obj, Type _ComponentType, bool _IncludeSelf = true, bool _IncludeInactive = false)
        {
            return GameObjectExtension.GetComponentsInHierarchy(_Obj.gameObject, _ComponentType, _IncludeSelf, _IncludeInactive);
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

#if !UNITY_EDITOR
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
#endif

        /// <summary>
		/// Initializes reference to components or GameObject on fields that use the [ComponentRef] attribute.
		/// </summary>
		public static void InitComponentRefs(this Component _Component)
        {
            foreach (FieldInfo fieldInfo in _Component.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                // If the current field doesn't use the ComponentRef attribute, skip this field
                if (!Attribute.IsDefined(fieldInfo, typeof(ComponentRefAttribute)))
                    continue;

                // If the current field's value is already set, skip this field
                if (fieldInfo.GetValue(_Component) != null)
                    continue;

                ComponentRefAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(ComponentRefAttribute)) as ComponentRefAttribute;
                // Find a component or GameObject reference depending on the [ComponentRef] attribute settings
                Object reference = FindComponentRef(_Component, fieldInfo.FieldType, attribute.Scope, attribute.RefObjectName);

                // Replace the field value if applicable
                if(reference != null)
                    fieldInfo.SetValue(_Component, reference);
            }
        }

        /// <summary>
        /// Find a component reference depending on the given settings.
        /// </summary>
        /// <param name="_Component">The component from which you want to get a reference.</param>
        /// <param name="_PropertyType">The type of the component you want to get. THis can also be GameObject.</param>
        /// <param name="_Scope">The scope of the research to use for getting the reference.</param>
        /// <param name="_RefObjectName">The name of the object on which you want to get the reference.</param>
        /// <returns>Returns the found reference, or null if it failed.</returns>
        public static Object FindComponentRef(Component _Component, Type _PropertyType, EComponentRefScope _Scope = EComponentRefScope.Local, string _RefObjectName = null)
        {
            bool isGameObjectRef = _PropertyType == typeof(GameObject);
            if (!_PropertyType.IsSubclassOf(typeof(Component)) && !isGameObjectRef)
                return null;

            // Get local references (using GetComponent())
            if (_Scope.HasFlag(EComponentRefScope.Local))
            {
                if (isGameObjectRef)
                {
                    if (string.IsNullOrEmpty(_RefObjectName) || _Component.gameObject.name == _RefObjectName)
                        return _Component.gameObject;
                }
                else
                {
                    if (string.IsNullOrEmpty(_RefObjectName) || _Component.gameObject.name == _RefObjectName)
                        return _Component.GetComponent(_PropertyType);
                }
            }

            // Get children references (using GetComponentsInHierarchy())
            if (_Scope.HasFlag(EComponentRefScope.Children))
            {
                // If we search for a GameObject
                if (isGameObjectRef)
                {
                    // If no target name has been mentionned, find the first GameObject in the hierarchy
                    if (string.IsNullOrEmpty(_RefObjectName))
                    {
                        foreach(Transform child in _Component.transform)
                        {
                            if (child != _Component.transform)
                                return child.gameObject;
                        }
                    }
                    // Else, finds the GameObject in the hierarchy that has the given name
                    else
                    {
                        return _Component.gameObject.Find(_RefObjectName, false, true);
                    }
                }
                // Else, if we search for a component
                else
                {
                    // If no object name has been mentionned, find the first component in the hierarchy with the given type
                    if (string.IsNullOrEmpty(_RefObjectName))
                        return _Component.GetComponentInHierarchy(_PropertyType, false, true);

                    // Else, try to get the component from the object of the given name in the hierarchy
                    GameObject target = _Component.gameObject.Find(_RefObjectName, false, true);
                    if (target != null && target.TryGetComponent(_PropertyType, out Component comp))
                        return comp;
                }
            }

            // Get World references (using FindObjectOfType())
            if (_Scope.HasFlag(EComponentRefScope.World))
            {
                if (isGameObjectRef)
                {
                    return string.IsNullOrEmpty(_RefObjectName)
                        ? Object.FindObjectOfType<GameObject>()
                        : GameObject.Find(_RefObjectName);
                }
                else
                {
                    Object target = Object.FindObjectOfType(_PropertyType);

                    if (string.IsNullOrEmpty(_RefObjectName) || target.name == _RefObjectName)
                        return target;

                    Object[] objs = Object.FindObjectsOfType(_PropertyType);
                    foreach(Object obj in objs)
                    {
                        if (obj.name == _RefObjectName)
                            return obj;
                    }
                }
            }

            return null;
        }

    }

}