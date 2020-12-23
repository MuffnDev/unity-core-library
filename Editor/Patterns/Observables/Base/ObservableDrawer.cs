using System;
using System.Reflection;

using UnityEngine;
using UnityEditor;

using MuffinDev.Core.EditorOnly;

namespace MuffinDev.Core
{

    /// <summary>
    /// Base class for making a PropertyDrawer for an serialized Observable property.
    /// </summary>
    /// <typeparam name="T">Type of the observed property.</typeparam>
    public abstract class ObservableDrawer<T> : PropertyDrawer
    {

        #region Properties

        private const string DEFAULT_VALUE_PROPERTY_NAME = "m_Value";
        private const string DEFAULT_EVENT_PROPERTY_NAME = "m_OnChange";

        private bool m_ShowEvent = false;

        #endregion


        #region Public Methods

        public override void OnGUI(Rect _Position, SerializedProperty _Property, GUIContent _Label)
        {
            if (_Property.hasMultipleDifferentValues)
            {
                EditorGUI.PropertyField(_Position, _Property);
                return;
            }

            // If value property does not exist, display dialog.
            SerializedProperty valueProperty = GetValueProperty(_Property);
            if(valueProperty == null)
            {
                EditorGUI.HelpBox(_Position, "The property " + ValuePropertyName + " does not exist on this object.", MessageType.None);
                return;
            }

            // Init layout values
            Vector2 position = _Position.position;
            Vector2 size = new Vector2(_Position.width, EditorGUI.GetPropertyHeight(valueProperty));
            
            m_ShowEvent = !CanExpand
                ? EditorGUI.Foldout(new Rect(position, new Vector2(0f, size.y)), m_ShowEvent, "")
                : valueProperty.isExpanded;

            // Draw value property
            EditorGUI.BeginChangeCheck();
            EditorGUI.PropertyField(new Rect(position, size), valueProperty, new GUIContent(_Property.displayName), true);
            // If the value property changed
            if (EditorGUI.EndChangeCheck())
            {
                // Notify changes on observable
                Observable<T> observable = fieldInfo.GetValue(_Property.serializedObject.targetObject) as Observable<T>;
                OnValueChange(observable, valueProperty);
            }

            position.y += size.y;
            size.y = EditorHelpers.LINE_HEIGHT + EditorHelpers.VERTICAL_MARGIN;

            // If event property does not exist, display dialog.
            SerializedProperty eventProperty = GetEventProperty(_Property);
            if(eventProperty == null)
            {
                EditorGUI.HelpBox(new Rect(position, size), "The event property " + EventPropertyName + " does not exist on this object.", MessageType.None);
                return;
            }
            // Display event field if allowed
            else if(m_ShowEvent)
            {
                size = new Vector2(size.x, EditorGUI.GetPropertyHeight(eventProperty));
                EditorGUI.PropertyField(new Rect(position, size), eventProperty);
            }

            // Force the inspector to repaint.
            // This is required to avoid glitches for observable value that contains children properties.
            EditorUtility.SetDirty(_Property.serializedObject.targetObject);
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// Called if value property is changed in the inspector.
        /// </summary>
        /// <param name="_Observable">The Observable shown in the inspector.</param>
        /// <param name="_ValueProperty">Property containing the updated value.</param>
        protected abstract void OnValueChange(Observable<T> _Observable, SerializedProperty _ValueProperty);

        #endregion


        #region Private Methods

        private bool CheckIfValueHasChildren(SerializedProperty _ValueProperty)
        {
            if(typeof(T).Name == "String")
            {
                return false;
            }
            else
            {
                return _ValueProperty.hasChildren;
            }
        }

        #endregion


        #region Accessors

        /// <summary>
        /// Gets the Observable property height.
        /// </summary>
        public override float GetPropertyHeight(SerializedProperty _Property, GUIContent _Label)
        {
            SerializedProperty valueProperty = GetValueProperty(_Property);
            if(valueProperty == null)
            {
                // Just return the height of a help box if value property does not exist
                return EditorHelpers.LINE_HEIGHT;
            }
            float height = EditorGUI.GetPropertyHeight(valueProperty);

            SerializedProperty eventProperty = GetEventProperty(_Property);
            if(eventProperty == null)
            {
                // Add the height of a help box if event property does not exist
                height += EditorHelpers.LINE_HEIGHT + EditorHelpers.VERTICAL_MARGIN;
            }
            else if(m_ShowEvent)
            {
                height += EditorGUI.GetPropertyHeight(eventProperty);
            }

            return height;
        }

        /// <summary>
        /// Gets the value property name of the Observable.
        /// </summary>
        protected virtual string ValuePropertyName
        {
            get { return DEFAULT_VALUE_PROPERTY_NAME; }
        }

        /// <summary>
        /// Gets the event property name of the Observable.
        /// </summary>
        protected virtual string EventPropertyName
        {
            get { return DEFAULT_EVENT_PROPERTY_NAME; }
        }

        /// <summary>
        /// Checks if the property can be expanded in the editor.
        /// For example, an integer can't expand because it does not have child property. On the other hand, an array can expand, because
        /// it contains as many child as indexes.
        /// </summary>
        protected virtual bool CanExpand
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the value property as SerializedProperty using its name.
        /// </summary>
        private SerializedProperty GetValueProperty(SerializedProperty _Property)
        {
            return _Property.FindPropertyRelative(ValuePropertyName);
        }

        /// <summary>
        /// Gets the event property as SerializedProperty using its name.
        /// </summary>
        private SerializedProperty GetEventProperty(SerializedProperty _Property)
        {
            return _Property.FindPropertyRelative(EventPropertyName);
        }

        #endregion

    }

}