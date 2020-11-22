using UnityEngine;
using UnityEditor;

namespace MuffinDev
{

    /// <summary>
    /// Locks the target property's value to the given minimum number in MinAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(MuffinDev.MinAttribute))]
    [CustomPropertyDrawer(typeof(MinimumAttribute))]
    public class MinDrawer : PropertyDrawer
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

            float min = (attribute is MinAttribute) ? (attribute as MinAttribute).Min : (attribute as MinimumAttribute).Min;

            bool isValid = true;
            switch (_Property.propertyType)
            {
                case SerializedPropertyType.Float:
                _Property.floatValue = GetValue(min, _Property.floatValue);
                break;

                case SerializedPropertyType.Integer:
                _Property.intValue = GetValue(min, _Property.intValue);
                break;

                case SerializedPropertyType.Vector2:
                _Property.vector2Value = GetValue(min, _Property.vector2Value);
                break;

                case SerializedPropertyType.Vector3:
                _Property.vector3Value = GetValue(min, _Property.vector3Value);
                break;

                case SerializedPropertyType.Vector4:
                _Property.vector4Value = GetValue(min, _Property.vector4Value);
                break;

                case SerializedPropertyType.Vector2Int:
                _Property.vector2IntValue = GetValue(min, _Property.vector2IntValue);
                break;

                case SerializedPropertyType.Vector3Int:
                _Property.vector3IntValue = GetValue(min, _Property.vector3IntValue);
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
                EditorGUI.HelpBox(_Position, "The Min attribute can't be used for this property type.", MessageType.None);
            }
        }

        #endregion


        #region Accessors

        private int GetValue(float _Min, int _Value)
        {
            return System.Convert.ToInt32(GetClampedValue(_Min, _Value));
        }

        private float GetValue(float _Min, float _Value)
        {
            return GetClampedValue(_Min, _Value);
        }

        private Vector2 GetValue(float _Min, Vector2 _Value)
        {
            _Value.x = GetClampedValue(_Min, _Value.x);
            _Value.y = GetClampedValue(_Min, _Value.y);
            return _Value;
        }

        private Vector3 GetValue(float _Min, Vector3 _Value)
        {
            _Value.x = GetClampedValue(_Min, _Value.x);
            _Value.y = GetClampedValue(_Min, _Value.y);
            _Value.z = GetClampedValue(_Min, _Value.z);
            return _Value;
        }

        private Vector4 GetValue(float _Min, Vector4 _Value)
        {
            _Value.x = GetClampedValue(_Min, _Value.x);
            _Value.y = GetClampedValue(_Min, _Value.y);
            _Value.z = GetClampedValue(_Min, _Value.z);
            _Value.w = GetClampedValue(_Min, _Value.w);
            return _Value;
        }

        private Vector2Int GetValue(float _Min, Vector2Int _Value)
        {
            _Value.x = GetClampedValue(_Min, _Value.x);
            _Value.y = GetClampedValue(_Min, _Value.y);
            return _Value;
        }

        private Vector3Int GetValue(float _Min, Vector3Int _Value)
        {
            _Value.x = GetClampedValue(_Min, _Value.x);
            _Value.y = GetClampedValue(_Min, _Value.y);
            _Value.z = GetClampedValue(_Min, _Value.z);
            return _Value;
        }

        private float GetClampedValue(float _Min, float _Value)
        {
            _Value = (_Value < _Min) ? _Min : _Value;
            return _Value;
        }

        private int GetClampedValue(float _Min, int _Value)
        {
            _Value = (_Value < _Min) ? Mathf.FloorToInt(_Min) : _Value;
            return _Value;
        }

        #endregion

    }

}