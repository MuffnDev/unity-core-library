using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core
{

    /// <summary>
    /// Enables or disables the target property field, depending on the IfCheckedAttribute values.
    /// </summary>
    [CustomPropertyDrawer(typeof(IfCheckedAttribute))]
    public class IfCheckedDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if (_Property.hasMultipleDifferentValues)
            {
                return;
            }

            IfCheckedAttribute ifAttribute = attribute as IfCheckedAttribute;

            SerializedProperty compProperty = _Property.serializedObject.FindProperty(ifAttribute.PropertyName);
            if (compProperty != null)
            {
                GUI.enabled = ifAttribute.EnabledIfChecked ? compProperty.boolValue : !compProperty.boolValue;
            }

            EditorGUI.PropertyField(_Position, _Property);
            GUI.enabled = true;
        }

    }

}