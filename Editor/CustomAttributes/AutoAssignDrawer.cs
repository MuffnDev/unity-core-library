using System;

using UnityEngine;
using UnityEditor;

using MuffinDev.EditorUtils;

namespace MuffinDev
{

    /// <summary>
    /// Automatically assign a component to the target property if possible.
    /// </summary>
    [CustomPropertyDrawer(typeof(AutoAssignAttribute))]
    public class AutoAssignDrawer : PropertyDrawer
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
                if(propertyContainer != null)
                {
                    // If the target property type's inherits from MonoBehaviour
                    Type propertyType = EditorHelpers.GetPropertyType(_Property);
                    if (propertyType.IsSubclassOf(typeof(Component)))
                    {
                        if (_Property.objectReferenceValue == null)
                        {
                            switch ((attribute as AutoAssignAttribute).AutoAssignMethod)
                            {
                                case EAutoAssignMethod.GetComponentInChildren:
                                _Property.objectReferenceValue = propertyContainer.GetComponentInChildren(propertyType);
                                break;

                                case EAutoAssignMethod.GetComponentInParent:
                                _Property.objectReferenceValue = propertyContainer.GetComponentInParent(propertyType);
                                break;

                                case EAutoAssignMethod.GetComponentFromRoot:
                                _Property.objectReferenceValue = propertyContainer.GetComponentFromRoot(propertyType);
                                break;

                                case EAutoAssignMethod.FindObjectOfType:
                                _Property.objectReferenceValue = GameObject.FindObjectOfType(propertyType);
                                break;

                                default:
                                _Property.objectReferenceValue = propertyContainer.GetComponent(propertyType);
                                break;
                            }
                        }
                    }
                }

                EditorGUI.PropertyField(_Position, _Property);
            }
            else
            {
                EditorGUI.HelpBox(_Position, "AutoAssign attribute only works with object reference properties", MessageType.None);
            }
        }

    }

}