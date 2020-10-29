using UnityEngine;
using UnityEditor;

namespace MuffinDev
{

    /// <summary>
    /// Indents the target property field by the level defined in IndentAttribute.
    /// </summary>
    [CustomPropertyDrawer(typeof(IndentAttribute))]
    public class IndentDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            IndentAttribute indentAttribute = attribute as IndentAttribute;

            int lastIndentLevel = EditorGUI.indentLevel;

            EditorGUI.indentLevel = indentAttribute.IndentLevel;
            EditorGUI.PropertyField(_Position, _Property);
            EditorGUI.indentLevel = lastIndentLevel;
        }

    }

}