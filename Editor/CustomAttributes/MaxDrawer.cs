using UnityEngine;
using UnityEditor;

namespace MuffinDev
{

    /// <summary>
    /// Locks the target property's value to the given maximum number in MaxAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(MaxAttribute))]
    [CustomPropertyDrawer(typeof(MaximumAttribute))]
        public class MaxDrawer : PropertyDrawer
    {

        #region UI

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if (_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            float max = (attribute is MaxAttribute) ? (attribute as MaxAttribute).Max : (attribute as MaximumAttribute).Max;

            bool isValid = true;
            switch (_Property.propertyType)
            {
                case SerializedPropertyType.Float:
                _Property.floatValue = GetValue(max, _Property.floatValue);
                break;

                case SerializedPropertyType.Integer:
                _Property.intValue = GetValue(max, _Property.intValue);
                break;

                case SerializedPropertyType.Vector2:
                _Property.vector2Value = GetValue(max, _Property.vector2Value);
                break;

                case SerializedPropertyType.Vector3:
                _Property.vector3Value = GetValue(max, _Property.vector3Value);
                break;

                case SerializedPropertyType.Vector4:
                _Property.vector4Value = GetValue(max, _Property.vector4Value);
                break;

                case SerializedPropertyType.Vector2Int:
                _Property.vector2IntValue = GetValue(max, _Property.vector2IntValue);
                break;

                case SerializedPropertyType.Vector3Int:
                _Property.vector3IntValue = GetValue(max, _Property.vector3IntValue);
                break;

                default:
                isValid = false;
                break;
            }

            if (isValid)
            {
                EditorGUI.PropertyField(_Position, _Property);
            }
            else
            {
                EditorGUI.HelpBox(_Position, "The Max attribute can't be use for this property type.", MessageType.None);
            }
        }

        #endregion


        #region Accessors

        private int GetValue(float _Max, int _Value)
        {
            return System.Convert.ToInt32(GetClampedValue(_Max, _Value));
        }

        private float GetValue(float _Max, float _Value)
        {
            return GetClampedValue(_Max, _Value);
        }

        private Vector2 GetValue(float _Max, Vector2 _Value)
        {
            _Value.x = GetClampedValue(_Max, _Value.x);
            _Value.y = GetClampedValue(_Max, _Value.y);
            return _Value;
        }

        private Vector3 GetValue(float _Max, Vector3 _Value)
        {
            _Value.x = GetClampedValue(_Max, _Value.x);
            _Value.y = GetClampedValue(_Max, _Value.y);
            _Value.z = GetClampedValue(_Max, _Value.z);
            return _Value;
        }

        private Vector4 GetValue(float _Max, Vector4 _Value)
        {
            _Value.x = GetClampedValue(_Max, _Value.x);
            _Value.y = GetClampedValue(_Max, _Value.y);
            _Value.z = GetClampedValue(_Max, _Value.z);
            _Value.w = GetClampedValue(_Max, _Value.w);
            return _Value;
        }

        private Vector2Int GetValue(float _Max, Vector2Int _Value)
        {
            _Value.x = GetClampedValue(_Max, _Value.x);
            _Value.y = GetClampedValue(_Max, _Value.y);
            return _Value;
        }

        private Vector3Int GetValue(float _Max, Vector3Int _Value)
        {
            _Value.x = GetClampedValue(_Max, _Value.x);
            _Value.y = GetClampedValue(_Max, _Value.y);
            _Value.z = GetClampedValue(_Max, _Value.z);
            return _Value;
        }

        private float GetClampedValue(float _Max, float _Value)
        {
            _Value = (_Value < _Max) ? _Max : _Value;
            return _Value;
        }

        private int GetClampedValue(float _Max, int _Value)
        {
            _Value = (_Value < _Max) ? Mathf.FloorToInt(_Max) : _Value;
            return _Value;
        }

        #endregion

    }

}