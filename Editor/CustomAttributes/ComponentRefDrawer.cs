using System;

using UnityEngine;
using UnityEditor;

using MuffinDev.Core.EditorOnly;

namespace MuffinDev.Core
{

    /// <summary>
    /// Automatically assign a component or a GameObject to the target property if possible.
    /// </summary>
    [CustomPropertyDrawer(typeof(ComponentRefAttribute))]
    public class ComponentRefDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            if(_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            if(_Property.propertyType == SerializedPropertyType.ObjectReference)
            {
                Component propertyContainer = _Property.serializedObject.targetObject as Component;
                if (propertyContainer != null)
                {
                    Type propertyType = EditorHelpers.GetPropertyType(_Property);
                    if (propertyType != null && propertyType.IsSubclassOf(typeof(Component)) || propertyType == typeof(GameObject))
                    {
                        if (_Property.objectReferenceValue == null)
                        {
                            ComponentRefAttribute attr = attribute as ComponentRefAttribute;
                            _Property.objectReferenceValue = ComponentExtension.FindComponentRef(propertyContainer, propertyType, attr.Scope, attr.RefObjectName);
                        }
                    }
                }
            }

            EditorGUI.PropertyField(_Position, _Property);
        }

    }

}