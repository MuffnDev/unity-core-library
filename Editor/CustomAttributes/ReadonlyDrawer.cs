using UnityEngine;
using UnityEditor;

namespace MuffinDev
{

    /// <summary>
    /// Disables a serialized property field in the Inspector view.
    /// </summary>
    [CustomPropertyDrawer(typeof(ReadonlyAttribute))]
    public class ReadonlyDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(_Position, _Property, true);
            GUI.enabled = true;
        }

    }

}