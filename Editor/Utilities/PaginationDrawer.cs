using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Custom editor for Pagination properties.
	///</summary>
    [CustomPropertyDrawer(typeof(Pagination))]
	public class PaginationDrawer : PropertyDrawer
	{

        private const string PAGE_PROP = "m_Page";
        private const string NB_ELEMENTS_PER_PAGE = "m_NbElementsPerPage";

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if (_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            SerializedProperty pageProperty = _Property.FindPropertyRelative(PAGE_PROP);
            SerializedProperty nbElementsPerPageProperty = _Property.FindPropertyRelative(NB_ELEMENTS_PER_PAGE);

            // Draw label
            Rect rect = new Rect(_Position);
            rect.width = EditorGUIUtility.labelWidth;
            EditorGUI.LabelField(rect, _Label, _Property.prefabOverride ? EditorStyles.boldLabel : EditorStyles.label);

            float fieldWidth = (_Position.width - EditorGUIUtility.labelWidth - MuffinDevGUI.HORIZONTAL_MARGIN * 2) / 2;
            EditorGUIUtility.labelWidth = fieldWidth / 2;
            // Draw current page field
            rect.x += rect.width + MuffinDevGUI.HORIZONTAL_MARGIN;
            rect.width = fieldWidth;
            pageProperty.intValue = EditorGUI.IntField(rect, new GUIContent("Page", "Current page index (0-based)"), pageProperty.intValue);

            // Draw "nb. elements per page" field
            rect.x += rect.width + MuffinDevGUI.HORIZONTAL_MARGIN;
            nbElementsPerPageProperty.intValue = EditorGUI.IntField(rect, new GUIContent("Nb. per page", "Number of elements per page"), nbElementsPerPageProperty.intValue);

            _Property.serializedObject.ApplyModifiedProperties();
        }

    }

}