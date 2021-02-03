using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

    ///<summary>
    /// 
    ///</summary>
    [CustomPropertyDrawer(typeof(Blackboard))]
    public class BlackboardPropertyDrawer : PropertyDrawer
	{

        private static readonly Dictionary<Type, IBlackboardValueEditor> VALUE_EDITORS = null;

        private const string SERIALIZED_DATA_LIST_PROP = "m_SerializedEntries";

        private const string DATA_TYPE_NAME_PROP = "m_DataTypeName";
        public const string SERIALIZED_DATA_PROP = "m_SerializedData";
        public const string KEY_PROP = "m_Key";

        static BlackboardPropertyDrawer()
        {
            IEnumerable<Type> valueEditorsTypes = ReflectionUtility.GetAllTypesAssignableFrom<IBlackboardValueEditor>();
            VALUE_EDITORS = new Dictionary<Type, IBlackboardValueEditor>();
            foreach (Type t in valueEditorsTypes)
            {
                IBlackboardValueEditor valueEditor = Activator.CreateInstance(t) as IBlackboardValueEditor;
                VALUE_EDITORS.Add(valueEditor.ValueType, valueEditor);
            }
        }

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            // Avoid strange behaviours when multi-selecting elements
            if (_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            Rect rect = new Rect(_Position);

            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);
            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                rect.height = MuffinDevGUI.LINE_HEIGHT;
                SerializedProperty item = serializedDataList.GetArrayElementAtIndex(i);
                Type dataType = Type.GetType(item.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue);

                if (dataType == null)
                {
                    EditorGUI.HelpBox(rect, "Invalid Type", MessageType.Warning);
                    rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
                    continue;
                }

                if (VALUE_EDITORS.TryGetValue(dataType, out IBlackboardValueEditor editor))
                {
                    rect.height = editor.GetPropertyHeight(item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue));
                    editor.OnGUI(rect, item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue));
                }
                else
                {
                    SerializedProperty keyProperty = item.FindPropertyRelative(KEY_PROP);
                    MuffinDevGUI.ComputeLabelledFieldRects(rect, out Rect labelRect, out Rect fieldRect);
                    keyProperty.stringValue = EditorGUI.TextField(labelRect, keyProperty.stringValue);
                    EditorGUI.LabelField(fieldRect, $"No editor for type {dataType.Name}", new GUIStyle(EditorStyles.helpBox).WordWrap(false));
                }
                rect.y += rect.height + MuffinDevGUI.VERTICAL_MARGIN;
            }

            if (GUI.Button(rect, "Add entry"))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("New Entry Type"));
                foreach (KeyValuePair<Type, IBlackboardValueEditor> valueEditor in VALUE_EDITORS)
                {
                    menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(valueEditor.Key.Name)), false, () =>
                    {
                        Debug.LogWarning("@todo: Create new entry of type: " + valueEditor.Key.FullName);
                    });
                }
                menu.ShowAsContext();
            }
        }

        public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
        {
            if (_Property.hasMultipleDifferentValues)
                return base.GetPropertyHeight(_Property, _Label);

            float height = 0f;
            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);

            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                SerializedProperty item = serializedDataList.GetArrayElementAtIndex(i);
                Type dataType = Type.GetType(item.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue);
                if (dataType == null)
                {
                    Debug.Log("Adding default height for type NULL: " + (MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN));
                    height += MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
                    continue;
                }

                height += VALUE_EDITORS.TryGetValue(dataType, out IBlackboardValueEditor editor)
                    ? editor.GetPropertyHeight(item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue))
                    : MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
            }

            Debug.Log("Output: " + height);
            return height;
        }

    }

}