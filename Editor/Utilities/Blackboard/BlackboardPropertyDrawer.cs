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

            SerializedProperty serializedDataList = _Property.FindPropertyRelative(SERIALIZED_DATA_LIST_PROP);

            // Draw box and header
            Rect rect = new Rect(_Position);
            GUI.Box(_Position, "", MuffinDevGUI.ReorderableListBoxStyle);
            rect.height = MuffinDevGUI.LINE_HEIGHT;
            GUI.Box(rect, _Property.displayName, MuffinDevGUI.ReorderableListHeaderStyle);

            Rect buttonRect = new Rect(rect);
            //buttonRect.width = Mathf.Clamp(rect.width * 40 / 100, 68, 100);
            buttonRect.width = 200f;
            buttonRect.x = rect.x + rect.width - MuffinDevGUI.HORIZONTAL_MARGIN - buttonRect.width;
            buttonRect.height -= MuffinDevGUI.VERTICAL_MARGIN;
            buttonRect.y += MuffinDevGUI.VERTICAL_MARGIN;
            DrawAddEntryButton(buttonRect, serializedDataList);

            // Setup layout
            rect.height = MuffinDevGUI.LINE_HEIGHT;
            rect.y += MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN * 2;
            rect.width -= MuffinDevGUI.HORIZONTAL_MARGIN * 2;
            rect.x += MuffinDevGUI.HORIZONTAL_MARGIN;

            // For each Blackboard entry
            for (int i = 0; i < serializedDataList.arraySize; i++)
            {
                Rect itemRect = new Rect(rect);
                itemRect.width = itemRect.height;
                // Draw "remove entry" button
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

            // Draw "is empty" label if the list is empty
            if (serializedDataList.arraySize == 0)
            {
                EditorGUI.LabelField(rect, "This Blackboard is empty...");
            }
        }

        /// <summary>
        /// Draws the "Add entry" button and the context menu
        /// </summary>
        private void DrawAddEntryButton(Rect _Position, SerializedProperty _DataList)
        {
            if (GUI.Button(_Position, EditorIcons.IconContent(EEditorIcon.AddDropdown, "Add Entry", "Creates a new entry on this Blackboard"), MuffinDevGUI.PropertyFieldButtonStyle))
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

            if (serializedDataList.arraySize == 0)
                return MuffinDevGUI.LINE_HEIGHT * 2 + MuffinDevGUI.VERTICAL_MARGIN * 3;

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

            return height + MuffinDevGUI.LINE_HEIGHT + MuffinDevGUI.VERTICAL_MARGIN * 2;
        }

        #endregion

    }

}