using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core
{

    [CustomPropertyDrawer(typeof(EnumFlagsAttribute))]
    public class EnumFlagsDrawer : PropertyDrawer
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

            _Property.intValue = EditorGUI.MaskField(_Position, _Label, _Property.intValue, _Property.enumDisplayNames);
        }

        #endregion

    }

}