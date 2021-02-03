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

        #region Properties

        private static readonly Dictionary<Type, IBlackboardValueEditor> VALUE_EDITORS = null;

        private const string SERIALIZED_DATA_LIST_PROP = "m_SerializedEntries";

        private const string DATA_TYPE_NAME_PROP = "m_DataTypeName";
        public const string SERIALIZED_DATA_PROP = "m_SerializedData";
        public const string KEY_PROP = "m_Key";

        #endregion


        #region Initialization

        /// <summary>
        /// Loads all IBlackboardValueEditor implementations.
        /// </summary>
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

        #endregion


        #region GUI

        /// <summary>
        /// Draws the Blackboard property GUI.
        /// </summary>
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
            // For each Blackboard entry
            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                Rect itemRect = new Rect(rect);
                itemRect.width = itemRect.height;
                if (GUI.Button(itemRect, EditorIcons.IconContent(EEditorIcon.Close, "Deletes this entry from the Blackboard"), MuffinDevGUI.PropertyFieldButtonStyle))
                {
                    serializedDataList.DeleteArrayElementAtIndex(i);
                    return;
                }

                itemRect.x += itemRect.width + MuffinDevGUI.HORIZONTAL_MARGIN;
                itemRect.width = rect.width - itemRect.width - MuffinDevGUI.HORIZONTAL_MARGIN;

                SerializedProperty item = serializedDataList.GetArrayElementAtIndex(i);
                Type dataType = Type.GetType(item.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue);

                // Display a warning field if the entry's type can't be found
                if (dataType == null)
                {
                    EditorGUI.HelpBox(itemRect, "Invalid Type", MessageType.Warning);
                    rect.y += itemRect.height + MuffinDevGUI.VERTICAL_MARGIN;
                    continue;
                }

                // If an editor exists for the entry's type, draw its GUI
                if (VALUE_EDITORS.TryGetValue(dataType, out IBlackboardValueEditor editor))
                {
                    itemRect.height = editor.GetPropertyHeight(item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue));
                    editor.OnGUI(itemRect, item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue));
                }
                // Else, draw the key field and display property type as a label
                else
                {
                    SerializedProperty keyProperty = item.FindPropertyRelative(KEY_PROP);
                    MuffinDevGUI.ComputeLabelledFieldRects(itemRect, out Rect labelRect, out Rect fieldRect);
                    keyProperty.stringValue = EditorGUI.TextField(labelRect, keyProperty.stringValue);
                    EditorGUI.LabelField(fieldRect, $"No editor for type {dataType.Name}", new GUIStyle(EditorStyles.helpBox).WordWrap(false));
                }
                rect.y += itemRect.height + MuffinDevGUI.VERTICAL_MARGIN;
                rect.height = MuffinDevGUI.LINE_HEIGHT;
            }

            // Draw the "Add entry" button
            DrawAddEntryButton(rect, serializedDataList);
        }

        /// <summary>
        /// Draws the "Add entry" button and the context menu
        /// </summary>
        private void DrawAddEntryButton(Rect _Position, SerializedProperty _DataList)
        {
            if (GUI.Button(_Position, "Add entry"))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddDisabledItem(new GUIContent("New Entry Type"));
                foreach (KeyValuePair<Type, IBlackboardValueEditor> valueEditor in VALUE_EDITORS)
                {
                    menu.AddItem(new GUIContent(ObjectNames.NicifyVariableName(valueEditor.Key.Name)), false, () =>
                    {
                        int index = _DataList.arraySize;
                        _DataList.InsertArrayElementAtIndex(index);

                        SerializedProperty prop = _DataList.GetArrayElementAtIndex(index);
                        prop.FindPropertyRelative(KEY_PROP).stringValue = $"New{valueEditor.Key.Name}";
                        prop.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue = valueEditor.Key.AssemblyQualifiedName;

                        object data = SerializationUtility.DeserializeFromString(valueEditor.Key, string.Empty);
                        prop.FindPropertyRelative(SERIALIZED_DATA_PROP).stringValue = SerializationUtility.SerializeToString(data);
                        Debug.Log("Create new property: " + $"New{valueEditor.Key.Name}");

                        prop.serializedObject.ApplyModifiedProperties();
                    });
                }
                menu.ShowAsContext();
            }
        }

        /// <summary>
        /// Gets the height of the property to display.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
        {
            // Gets the default property height if several properties are selected
            if (_Property.hasMultipleDifferentValues)
                return base.GetPropertyHeight(_Property, _Label);

            float height = 0f;
            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);

            // For each entry on the blackboard...
            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                SerializedProperty item = serializedDataList.GetArrayElementAtIndex(i);
                Type dataType = Type.GetType(item.FindPropertyRelative(DATA_TYPE_NAME_PROP).stringValue);

                // Set default height if the data type can't be found
                if (dataType == null)
                {
                    height += MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
                    continue;
                }

                // Use the found editor's property height, or the default one
                height += VALUE_EDITORS.TryGetValue(dataType, out IBlackboardValueEditor editor)
                    ? editor.GetPropertyHeight(item, new GUIContent(item.FindPropertyRelative("m_Key").stringValue))
                    : MuffinDevGUI.LINE_HEIGHT;
                height += MuffinDevGUI.VERTICAL_MARGIN;
            }

            return height + MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN;
        }

        #endregion

    }

}