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
            rect.height = MuffinDevGUI.LINE_HEIGHT;

            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);
            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                SerializedProperty item = serializedDataList.GetArrayElementAtIndex(i);
                Type dataType = Type.GetType(item.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue);
                if (dataType == null)
                {
                    EditorGUI.HelpBox(rect, "Null type", MessageType.Warning);
                    rect.y += MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
                    continue;
                }

                if (VALUE_EDITORS.TryGetValue(dataType, out IBlackboardValueEditor editor))
                {
                    editor.OnGUI(rect, item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue));
                }
                else
                {
                    EditorGUI.HelpBox(rect, "No custom editor found for type " + dataType.FullName, MessageType.Info);
                }
                rect.y += MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
            }
        }

        public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
        {
            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);
            return serializedDataList.arraySize * MuffinDevGUI.LINE_HEIGHT + serializedDataList.arraySize * MuffinDevGUI.VERTICAL_MARGIN;
        }

    }

}