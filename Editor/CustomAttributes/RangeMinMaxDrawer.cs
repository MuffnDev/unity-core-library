using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core
{

    /// <summary>
    /// Draws a MinMaxSlider field for the target property.
    /// </summary>
    [CustomPropertyDrawer(typeof(RangeMinMaxAttribute))]
    public class RangeMinMaxDrawer : PropertyDrawer
    {

        private const float NUMBER_FIELD_WIDTH = 45.0f;
        private const float MARGIN = 4.0f;

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if (_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            if(_Property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.HelpBox(_Position, "The RangeMinMax attribute can only be applied to Vector2 properties.", MessageType.None);
                return;
            }

            RangeMinMaxAttribute attr = attribute as RangeMinMaxAttribute;

            float min = _Property.vector2Value.x;
            float max = _Property.vector2Value.y;

            Vector2 position = _Position.position;
            Vector2 size = _Position.size;

            // Draw label
            size.x = EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(new Rect(position, size), _Label);

            // Draw "min" number field
            position.x += size.x;
            size.x = NUMBER_FIELD_WIDTH;
            min = Mathf.Clamp(EditorGUI.FloatField(new Rect(position, size), min), attr.MinLimit, max);

            // Draw slider
            position.x += size.x + MARGIN;
            size.x = _Position.size.x - (EditorGUIUtility.labelWidth + (NUMBER_FIELD_WIDTH + MARGIN) * 2);
            EditorGUI.MinMaxSlider(new Rect(position, size), ref min, ref max, attr.MinLimit, attr.MaxLimit);

            // Draw "max" number field
            position.x += size.x + MARGIN;
            size.x = NUMBER_FIELD_WIDTH;
            max = Mathf.Clamp(EditorGUI.FloatField(new Rect(position, size), max), min, attr.MaxLimit);

            if (attr.ForceInt)
            {
                min = Mathf.Floor(min);
                max = Mathf.Floor(max);
            }

            _Property.vector2Value = new Vector2(min, max);
        }

    }

}