using UnityEngine;
using UnityEditor;

namespace MuffinDev
{

    /// <summary>
    /// Displays a custom curve editor in the inspector view for AnimationCurve properties, based on the AnimCurveAttribute values.
    /// </summary>
    [CustomPropertyDrawer(typeof(AnimCurveAttribute))]
    public class AnimCurveDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if(_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            AnimCurveAttribute animCurve = attribute as AnimCurveAttribute;

            if (_Property.propertyType == SerializedPropertyType.AnimationCurve)
            {
                EditorGUI.CurveField(_Position, _Property, animCurve.CurveColor, animCurve.GetRanges());
                _Property.serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUI.HelpBox(_Position, "The AnimCurve attribute can only be used on AnimationCurve properties.", MessageType.None);
            }
        }

    }

}