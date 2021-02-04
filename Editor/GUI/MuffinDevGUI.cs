using System;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using Object = UnityEngine.Object;

namespace MuffinDev.Core.EditorOnly
{

    ///<summary>
    /// Bundle of methods for custom GUI fields.
    ///</summary>
    public class MuffinDevGUI : EditorWindow
	{

        #region Properties

        // Default Muffin Dev' editor tools horizontal margin.
        public static readonly float HORIZONTAL_MARGIN = 2f;
        // Default Muffin Dev' editor tools vertical margin.
        public static readonly float VERTICAL_MARGIN = 2f;
        // Default Muffin Dev' editor tools property line height.
        public static readonly float LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
        // Default Muffin Dev' editor windows padding.
        public const float EDITOR_WINDOW_PADDING = 2f;
        public const float INSPECTOR_FOLDOUT_LEFT_OFFSET = 14f;

        public const string DEFAULT_BACK_BUTTON_LABEL = "Back";
        public const float DEFAULT_BACK_BUTTON_WIDTH = 64f;

        public const float BOOLEAN_SWITCH_TOOLBAR_WIDTH = 134f;
        public static readonly string[] BOOLEAN_SWITCH_LABELS = { "On", "Off" };

        private static GUIStyle s_ExtendedObjectFieldButtonStyle = null;
        private const float MIN_EXTENDED_OBJECT_FIELD_WIDTH = 54f;
        private const int MINI_BUTTON_PADDING = 1;

        private const float BROWSE_BUTTON_WIDTH = 100f;

        private const float PAGINATION_BUTTONS_WIDTH = 80f;
        private const float PAGINATION_LABEL_WIDTH = 32f;
        private const float PAGINATION_FIELD_WIDTH = 54f;

        #endregion


        #region Float Field

        /// <summary>
        /// Makes a text field for entering floats, but allows you to define the "drag hot zone" of the control.
        /// </summary>
        public static float FloatField(Rect _Position, Rect _DragHotZone, float _Value)
        {
            int controlID = GUIUtility.GetControlID("EditorTextField".GetHashCode(), FocusType.Keyboard, _Position);
            Type editorGUIType = typeof(EditorGUI);

            Type recycledTextEditorType = Assembly.GetAssembly(editorGUIType).GetType("UnityEditor.EditorGUI+RecycledTextEditor");
            Type[] argumentTypes = new Type[] { recycledTextEditorType, typeof(Rect), typeof(Rect), typeof(int), typeof(float), typeof(string), typeof(GUIStyle), typeof(bool) };
            MethodInfo doFloatFieldMethod = editorGUIType.GetMethod("DoFloatField", BindingFlags.NonPublic | BindingFlags.Static, null, argumentTypes, null);

            FieldInfo fieldInfo = editorGUIType.GetField("s_RecycledEditor", BindingFlags.NonPublic | BindingFlags.Static);
            object recycledEditor = fieldInfo.GetValue(null);

            object[] parameters = new object[] { recycledEditor, _Position, _DragHotZone, controlID, _Value, "g7", EditorStyles.numberField, true };

            return (float)doFloatFieldMethod.Invoke(null, parameters);
        }

        #endregion


        #region Horizontal Line

        /// <summary>
        /// Draws an horizontal line.
        /// </summary>
        /// <param name="_Rect">The position and size of the line.</param>
        public static void HorizontalLine(Rect _Rect)
        {
            HorizontalLine(_Rect, Color.grey);
        }

        /// <summary>
        /// Draws an horizontal line.
        /// </summary>
        /// <param name="_Rect">The position and size of the line.</param>
        /// <param name="_Color">The color of the line.</param>
        public static void HorizontalLine(Rect _Rect, Color _Color)
        {
            EditorGUI.DrawRect(_Rect, _Color);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        public static void HorizontalLine(bool _Wide = false)
        {
            HorizontalLine(1f, Color.grey, _Wide);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Height">The height of the line.</param>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        public static void HorizontalLine(float _Height, bool _Wide = false)
        {
            HorizontalLine(_Height, Color.grey, _Wide);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Height">The height of the line.</param>
        /// <param name="_Color">The color of the line.</param>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        public static void HorizontalLine(float _Height, Color _Color, bool _Wide = false)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, _Height);
            if (_Wide)
            {
                float sizeDiff = EditorGUIUtility.currentViewWidth - rect.width;
                rect.position = new Vector2(rect.x - sizeDiff / 2f, rect.y);
                rect.size = new Vector2(rect.width + sizeDiff, rect.height);
            }
            rect.height = _Height;
            EditorGUI.DrawRect(rect, _Color);
        }

        #endregion


        #region Search Bar

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        public static string SearchBar(string _Text, Action _OnCancel = null)
        {
            string output = _Text;
            EditorGUILayout.BeginHorizontal();
            {
                output = EditorGUILayout.TextField(output, new GUIStyle("SearchTextField"));
                if (_OnCancel == null)
                    GUILayout.Button("", new GUIStyle("SearchCancelButtonEmpty"));
                else if (GUILayout.Button("", new GUIStyle("SearchCancelButton")))
                    _OnCancel.Invoke();
            }
            EditorGUILayout.EndHorizontal();
            return output;
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Label">The label of the search bar field.</param>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        public static string SearchBar(string _Label, string _Text, Action _OnCancel = null)
        {
            string output = _Text;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(output, GUILayout.Width(EditorGUIUtility.labelWidth));
                output = EditorGUILayout.TextField(output, new GUIStyle("SearchTextField"));
                if (_OnCancel == null)
                    GUILayout.Button("", new GUIStyle("SearchCancelButtonEmpty"));
                else if (GUILayout.Button("", new GUIStyle("SearchCancelButton")))
                    _OnCancel.Invoke();
            }
            EditorGUILayout.EndHorizontal();
            return output;
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <param name="_SearchBarStyle">The GUIStyle to apply on the search bar field.</param>
        /// <param name="_CancelButtonStyle">The GUIStyle to apply on the cancel button.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        public static string SearchBar(string _Text, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null)
        {
            string output = _Text;
            EditorGUILayout.BeginHorizontal();
            {
                output = EditorGUILayout.TextField(output, _SearchBarStyle);
                if (_OnCancel == null)
                    GUILayout.Button("", new GUIStyle("SearchCancelButtonEmpty"));
                else if (GUILayout.Button("", _CancelButtonStyle))
                    _OnCancel.Invoke();
            }
            EditorGUILayout.EndHorizontal();
            return output;
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Label">The label of the search bar field.</param>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <param name="_SearchBarStyle">The GUIStyle to apply on the search bar field.</param>
        /// <param name="_CancelButtonStyle">The GUIStyle to apply on the cancel button.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        public static string SearchBar(string _Label, string _Text, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null)
        {
            string output = _Text;
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField(_Label, GUILayout.Width(EditorGUIUtility.labelWidth));
                output = EditorGUILayout.TextField(output, _SearchBarStyle);
                if (_OnCancel == null)
                    GUILayout.Button("", new GUIStyle("SearchCancelButtonEmpty"));
                else if (GUILayout.Button("", _CancelButtonStyle))
                    _OnCancel.Invoke();
            }
            EditorGUILayout.EndHorizontal();
            return output;
        }

        #endregion


        #region Switch Field

        /// <summary>
        /// Draws a "On/Off" switch field.
        /// </summary>
        /// <param name="_Value">The current property value.</param>
        /// <returns>Returns true if "On" is selected, otherwise false.</returns>
        public static bool SwitchField(bool _Value)
        {
            int selectedIndex = GUILayout.Toolbar(_Value ? 0 : 1, BOOLEAN_SWITCH_LABELS, GUILayout.Width(BOOLEAN_SWITCH_TOOLBAR_WIDTH));
            _Value = selectedIndex == 0;
            return _Value;
        }

        /// <summary>
        /// Draws a "On/Off" switch field.
        /// </summary>
        /// <param name="_Label">The label of the property.</param>
        /// <param name="_Value">The current property value.</param>
        /// <returns>Returns true if "On" is selected, otherwise false.</returns>
        public static bool SwitchField(string _Label, bool _Value)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label(_Label, GUILayout.Width(EditorGUIUtility.labelWidth));
                int selectedIndex = GUILayout.Toolbar(_Value ? 0 : 1, BOOLEAN_SWITCH_LABELS, GUILayout.Width(BOOLEAN_SWITCH_TOOLBAR_WIDTH));
                _Value = selectedIndex == 0;
            }
            EditorGUILayout.EndHorizontal();
            return _Value;
        }

        #endregion


        #region Object Field

        /// <summary>
        /// Draws an Object field with a "Create new" button on the right.
        /// </summary>
        /// <typeparam name="TObjectType">The type of the object that can be passed to the Object field.</typeparam>
        /// <param name="_Position">The position and size of the field.</param>
        /// <param name="_Label">The label to display on the left of the field.</param>
        /// <param name="_Property">The property on which you want to set the field value.</param>
        /// <param name="_PanelTitle">The title of the SavePanel utility.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to pass scene object in the object field.</param>
        public static void ObjectField<TObjectType>(Rect _Position, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true)
            where TObjectType : Object
        {
            ObjectField(_Position, typeof(TObjectType), _Label, _Property, _PanelTitle, _AllowSceneObjects);
        }

        /// <summary>
        /// Draws an Object field with a "Create new" button on the right.
        /// </summary>
        /// <param name="_Position">The position and size of the field.</param>
        /// <param name="_ObjectType">The type of the object that can be passed to the Object field.</param>
        /// <param name="_Label">The label to display on the left of the field.</param>
        /// <param name="_Property">The property on which you want to set the field value.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to pass scene object in the object field.</param>
        public static void ObjectField(Rect _Position, Type _ObjectType, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true)
        {
            ExtendedObjectField(_Position, _Property, _Label.text, _ObjectType, _AllowSceneObjects, new ExtendedObjectFieldButton[]
            {
                new ExtendedObjectFieldButton(EEditorIcon.Add, () =>
                {
                    EditorHelpers.CreateAssetPanel(_ObjectType, out Object asset, _PanelTitle, $"New{_ObjectType.Name}", "", "asset", false);
                    if (asset != null)
                        _Property.objectReferenceValue = asset;
                })
            });
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static T ExtendedObjectField<T>(T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            return ExtendedObjectField(EditorGUILayout.GetControlRect(), _Object, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static T ExtendedObjectField<T>(string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            _Object = ExtendedObjectField(EditorGUILayout.GetControlRect(), _Label, _Object, typeof(T), _AllowSceneObjects, _Buttons) as T;
            return _Object;
        }

        /// <summary>
        /// Draws the input field for an Extended Object Field, and the additional controls before and after that field.
        /// </summary>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        /// <returns>Returns the selected object.</returns>
        public static Object ExtendedObjectField(Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            ExtendedObjectField(EditorGUILayout.GetControlRect(), _Object, _ObjectType, _AllowSceneObjects, _Buttons);
            return _Object;
        }

        /// <summary>
        /// Draws the input field for an Extended Object Field, and the additional controls before and after that field.
        /// </summary>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        /// <returns>Returns the selected object.</returns>
        public static Object ExtendedObjectField(string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            _Object = ExtendedObjectField(EditorGUILayout.GetControlRect(), _Label, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
            return _Object;
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static void ExtendedObjectField(SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            ExtendedObjectField(EditorGUILayout.GetControlRect(), _Property, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static void ExtendedObjectField(SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            ExtendedObjectField(EditorGUILayout.GetControlRect(), _Property, _Label, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static T ExtendedObjectField<T>(Rect _Rect, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            _Object = ExtendedObjectField(_Rect, _Object, typeof(T), _AllowSceneObjects, _Buttons) as T;
            return _Object;
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static T ExtendedObjectField<T>(Rect _Rect, string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            _Object = ExtendedObjectField(_Rect, _Label, _Object, typeof(T), _AllowSceneObjects, _Buttons) as T;
            return _Object;
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static Object ExtendedObjectField(Rect _Rect, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            Rect rect = new Rect(_Rect);

            float relativeX = 0f;
            // Draw buttons before label
            DrawExtendedObjectFieldButtons(ref rect, ref relativeX, ExtendedObjectFieldButton.EPosition.BeforeLabel, _Buttons);

            rect.width = _Rect.width - relativeX;
            return DrawExtendedObjectFieldInput(rect, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static Object ExtendedObjectField(Rect _Rect, string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            Rect rect = new Rect(_Rect);

            float relativeX = 0f;
            // Draw buttons before label
            DrawExtendedObjectFieldButtons(ref rect, ref relativeX, ExtendedObjectFieldButton.EPosition.BeforeLabel, _Buttons);

            // Draw label
            rect.width = Mathf.Max(EditorGUIUtility.labelWidth - relativeX, MIN_EXTENDED_OBJECT_FIELD_WIDTH);
            EditorGUI.LabelField(rect, _Label);

            rect.x += rect.width + HORIZONTAL_MARGIN;
            relativeX += rect.width + HORIZONTAL_MARGIN;

            rect.width = _Rect.width - relativeX;
            return DrawExtendedObjectFieldInput(rect, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            _Property.objectReferenceValue = ExtendedObjectField(_Rect, _Property.displayName, _Property.objectReferenceValue, _ObjectType, _AllowSceneObjects, _Buttons);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            _Property.objectReferenceValue = ExtendedObjectField(_Rect, _Label, _Property.objectReferenceValue, _ObjectType, _AllowSceneObjects, _Buttons);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws the input field for an Extended Object Field, and the additional controls before and after that field.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        /// <returns>Returns the selected object.</returns>
        private static Object DrawExtendedObjectFieldInput(Rect _Rect, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            Rect rect = new Rect(_Rect);

            float relativeX = 0f;
            // Draw buttons before field
            DrawExtendedObjectFieldButtons(ref rect, ref relativeX, ExtendedObjectFieldButton.EPosition.BeforeField, _Buttons);

            // Compute field width
            float fieldWidth = _Rect.width - relativeX;
            foreach (ExtendedObjectFieldButton btn in _Buttons)
            {
                if (btn.position == ExtendedObjectFieldButton.EPosition.AfterField)
                {
                    GUIContent btnContent = btn.GetContent();
                    fieldWidth -= ComputeExtendedObjectFieldButtonSize(btn).x + HORIZONTAL_MARGIN;
                }
            }
            rect.width = Mathf.Max(fieldWidth, MIN_EXTENDED_OBJECT_FIELD_WIDTH);
            // Draw field
            _Object = EditorGUI.ObjectField(rect, _Object, _ObjectType, _AllowSceneObjects);

            rect.x += rect.width + HORIZONTAL_MARGIN;

            // Draw buttons after field
            DrawExtendedObjectFieldButtons(ref rect, ref relativeX, ExtendedObjectFieldButton.EPosition.AfterField, _Buttons);

            return _Object;
        }

        /// <summary>
        /// Draws all buttons of the list for an Extended Object Field that has the given position.
        /// </summary>
        /// <param name="_Rect">The position of the button to draw.</param>
        /// <param name="_RelativeX">The current X offset when drawing the GUI.</param>
        /// <param name="_Position">The position in the field of the buttons you want to draw..</param>
        /// <param name="_Buttons">The content and settings of all the buttons to draw for a field.</param>
        private static void DrawExtendedObjectFieldButtons(ref Rect _Rect, ref float _RelativeX, ExtendedObjectFieldButton.EPosition _Position, ExtendedObjectFieldButton[] _Buttons)
        {
            foreach (ExtendedObjectFieldButton btn in _Buttons)
            {
                if (btn.position != _Position)
                    continue;

                DrawExtendedObjectFieldButton(ref _Rect, ref _RelativeX, btn);
            }
        }

        /// <summary>
        /// Draws a button for an Extended Object Field.
        /// </summary>
        /// <param name="_Rect">The position of the button to draw.</param>
        /// <param name="_RelativeX">The current X offset when drawing the GUI.</param>
        /// <param name="_Button">The content and settings of the button you want to draw.</param>
        private static void DrawExtendedObjectFieldButton(ref Rect _Rect, ref float _RelativeX, ExtendedObjectFieldButton _Button)
        {
            Vector2 btnSize = ComputeExtendedObjectFieldButtonSize(_Button);
            _Rect.width = !string.IsNullOrEmpty(_Button.text) ? btnSize.x : btnSize.y;
            bool originalGUIState = GUI.enabled;
            GUI.enabled = _Button.enabled;
            if (GUI.Button(_Rect, _Button.GetContent(), ExtendedObjectFieldButtonStyle))
            {
                if (_Button.onClick != null)
                    _Button.onClick.Invoke();
            }
            GUI.enabled = originalGUIState;

            _Rect.x += _Rect.width + HORIZONTAL_MARGIN;
            _RelativeX += _Rect.width + HORIZONTAL_MARGIN;
        }

        /// <summary>
        /// Computes the final size of the button to draw in an Extended Object Field.
        /// </summary>
        /// <param name="_Button">The content and settings of the button.</param>
        /// <returns>Returns the final size of the button to draw in an Extended Object Field.</returns>
        private static Vector2 ComputeExtendedObjectFieldButtonSize(ExtendedObjectFieldButton _Button)
        {
            Vector2 btnSize = ExtendedObjectFieldButtonStyle.CalcSize(_Button.GetContent());
            if (string.IsNullOrEmpty(_Button.text))
                btnSize.x = btnSize.y;
            return btnSize;
        }

        #endregion


        #region Back Button

        /// <summary>
        /// Draws a "Back" button.
        /// </summary>
        /// <param name="_Label">The content of the button.</param>
        /// <param name="_Tooltip">The tooltip of the button.</param>
        /// <param name="_Width">The optional width of the button.</param>
        /// <param name="_ButtonStyle">The GUIStyle of the button.</param>
        /// <returns>Returns true if the button has been clicked this frame, otherwise false.</returns>
        public static bool BackButton(string _Label = null, string _Tooltip = null, float _Width = DEFAULT_BACK_BUTTON_WIDTH, GUIStyle _ButtonStyle = null)
        {
            Rect rect = EditorGUILayout.GetControlRect(false, GUILayout.Width(_Width));
            return BackButton(rect, _Label, _Tooltip, _ButtonStyle, _Width);
        }

        /// <summary>
        /// Draws a "Back" button.
        /// </summary>
        /// <param name="_Rect">The Rect container of the button.</param>
        /// <param name="_Label">The content of the button.</param>
        /// <param name="_Tooltip">The tooltip of the button.</param>
        /// <param name="_ButtonStyle">The GUIStyle of the button.</param>
        /// <param name="_Width">The optional width of the button.</param>
        /// <returns>Returns true if the button has been clicked this frame, otherwise false.</returns>
        public static bool BackButton(Rect _Rect, string _Label = null, string _Tooltip = null, GUIStyle _ButtonStyle = null, float _Width = DEFAULT_BACK_BUTTON_WIDTH)
        {
            if (string.IsNullOrEmpty(_Label)) _Label = DEFAULT_BACK_BUTTON_LABEL;
            if (_ButtonStyle == null) _ButtonStyle = GUI.skin.button;
            _Rect.width = Mathf.Max(_Width, DEFAULT_BACK_BUTTON_WIDTH);

            GUIContent backBtnContent = EditorGUIUtility.IconContent("tab_prev", _Tooltip);
            backBtnContent.text = _Label;
            return GUI.Button(_Rect, backBtnContent, _ButtonStyle);
        }

        #endregion


        #region Default Inspector

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Asset">The asset of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        public static void DrawDefaultInspector(Object _Asset, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            SerializedObject obj = new SerializedObject(_Asset);
            DrawDefaultInspector(obj, _IncludeScriptProperty, _CustomLabelWidth);
        }

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Object">The SerializedObject of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        public static void DrawDefaultInspector(SerializedObject _Object, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            SerializedProperty prop = _Object.GetIterator();
            DrawDefaultInspector(prop, _IncludeScriptProperty, _CustomLabelWidth);
        }

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Property">The SerializedProperty of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        public static void DrawDefaultInspector(SerializedProperty _Property, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            // Set the label width if needed
            float lastLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = _CustomLabelWidth > 0f ? _CustomLabelWidth : lastLabelWidth;

            _Property.NextVisible(true);
            bool hasDrawnScriptProperty = false;

            // Draw all properties
            do
            {
                if (!hasDrawnScriptProperty && _Property.name == "m_Script")
                {
                    hasDrawnScriptProperty = true;
                    if (!_IncludeScriptProperty)
                        continue;
                }
                EditorGUILayout.PropertyField(_Property, true);
                _Property.serializedObject.ApplyModifiedProperties();
            }
            while (_Property.NextVisible(false));

            EditorGUIUtility.labelWidth = lastLabelWidth;
        }

        #endregion


        #region Pagination Bar

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_NbElements">The total number of elements in your paginated list.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        public static Pagination PaginationBar(int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return PaginationBar(rect, _NbElements, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        public static Pagination PaginationBar<T>(IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return PaginationBar(rect, _List.Count, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_Rect">The position and size of the element to draw.</param>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        public static Pagination PaginationBar<T>(Rect _Rect, IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return PaginationBar(_Rect, _List.Count, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_Rect">The position and size of the element to draw.</param>
        /// <param name="_NbElements">The total number of elements in your paginated list.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        public static Pagination PaginationBar(Rect _Rect, int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            Pagination pagination = new Pagination(_NbElements, _Page, _NbElementsPerPage);
            Rect rect = new Rect(_Rect);

            // Draw "Previous" button
            rect.width = PAGINATION_BUTTONS_WIDTH;
            GUI.enabled = pagination.Page > 0;
            if (GUI.Button(rect, "< Previous"))
                pagination.Page--;
            GUI.enabled = true;

            // Compute the spaces, in order to center the page fields
            float middleSpace = _Rect.width - PAGINATION_BUTTONS_WIDTH * 2 - HORIZONTAL_MARGIN * 5;
            float space = Mathf.Max(0, (middleSpace - PAGINATION_LABEL_WIDTH - PAGINATION_FIELD_WIDTH * 2) / 2);

            // Draw "Page" label and int field
            rect.x += rect.width + HORIZONTAL_MARGIN + space;
            rect.width = PAGINATION_LABEL_WIDTH + HORIZONTAL_MARGIN + PAGINATION_FIELD_WIDTH;
            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = PAGINATION_LABEL_WIDTH;
            pagination.Page = EditorGUI.IntField(rect, "Page", pagination.Page);
            EditorGUIUtility.labelWidth = labelWidth;

            // Draw the total number of pages label
            rect.x += rect.width + HORIZONTAL_MARGIN;
            rect.width = PAGINATION_FIELD_WIDTH;
            EditorGUI.LabelField(rect, $"/{pagination.NbPages - 1}");

            // Draw the "Next" button
            rect.x += rect.width + space + HORIZONTAL_MARGIN;
            rect.width = PAGINATION_BUTTONS_WIDTH;
            GUI.enabled = pagination.Page < pagination.NbPages - 1;
            if (GUI.Button(rect, "Next >"))
                pagination.Page++;
            GUI.enabled = true;

            return pagination;
        }

        #endregion


        #region Path Fields

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        public static void FolderPathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            _Property.stringValue = DrawPathField(rect, _Property.displayName, _Property.stringValue, _Editable, _InProject, false);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        public static string FolderPathField(string _Path, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return DrawPathField(rect, null, _Path, _Editable, _InProject, false);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        public static string FolderPathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return DrawPathField(rect, _Label, _Path, _Editable, _InProject, false);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        public static void FolderPathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            _Property.stringValue = DrawPathField(_Rect, _Property.displayName, _Property.stringValue, _Editable, _InProject, true);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        public static string FolderPathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return DrawPathField(_Rect, null, _Path, _Editable, _InProject, false);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        public static string FolderPathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return DrawPathField(_Rect, _Label, _Path, _Editable, _InProject, false);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        public static void FilePathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            _Property.stringValue = DrawPathField(rect, _Property.displayName, _Property.stringValue, _Editable, _InProject, true);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        public static string FilePathField(string _Path, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return DrawPathField(rect, null, _Path, _Editable, _InProject, true);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        public static string FilePathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            return DrawPathField(rect, _Label, _Path, _Editable, _InProject, true);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        public static void FilePathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            _Property.stringValue = DrawPathField(_Rect, _Property.displayName, _Property.stringValue, _Editable, _InProject, true);
            _Property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        public static string FilePathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return DrawPathField(_Rect, null, _Path, _Editable, _InProject, true);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        public static string FilePathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return DrawPathField(_Rect, _Label, _Path, _Editable, _InProject, true);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        /// <param name="_File">If true, the "Browse..." button opens a file selector panel, otherwise it opens a folder selector
        /// panel.</param>
        /// <returns>Returns the selected path.</returns>
        private static string DrawPathField(Rect _Rect, string _Label, string _Path, bool _Editable, bool _InProject, bool _File)
        {
            Rect rect = new Rect(_Rect);

            // Draws the label if needed
            if (!string.IsNullOrEmpty(_Label))
            {
                rect.width = EditorGUIUtility.labelWidth;
                EditorGUI.LabelField(rect, _Label);
                rect.x += rect.width + HORIZONTAL_MARGIN;
                rect.width = _Rect.width - rect.width - BROWSE_BUTTON_WIDTH - HORIZONTAL_MARGIN;
            }
            else
            {
                rect.width = _Rect.width - BROWSE_BUTTON_WIDTH;
            }

            // Draw the path text field
            GUI.enabled = _Editable;
            string newPath = EditorGUI.TextField(rect, _Path);
            GUI.enabled = true;

            // Draw the "Browse..." button
            rect.x += rect.width + HORIZONTAL_MARGIN;
            rect.width = BROWSE_BUTTON_WIDTH;
            if (GUI.Button(rect, EditorIcons.IconContent(EEditorIcon.Search, "Browse...", "Open your explorer to set the path"), GUI.skin.button.Padding(1, 1)))
            {
                string dir = _InProject ? "Assets" : null;
                if (_File)
                    newPath = EditorUtility.OpenFilePanel("Select File Path", dir, null);
                else
                    newPath = EditorUtility.OpenFolderPanel("Select File Path", dir, null);
            }

            // If the new path is null or empty, consider the selected path didn't change
            if (string.IsNullOrEmpty(newPath))
                return _Path;

            // If the path must target the project but it actually doesn't, display a dialog
            if (!_Editable && _InProject && !string.IsNullOrEmpty(_Path) && !EditorHelpers.IsPathRelativeToCurrentProjectFolder(_Path))
            {
                EditorUtility.DisplayDialog("Invalid Path", $"The path must target a {(_File ? "file" : "directory")} in this project", "Ok");
                return newPath == _Path ? string.Empty : _Path;
            }

            return newPath;
        }

        #endregion


        #region Layout Helpers

        /// <summary>
        /// Computes the size and position of a label and a field, depending on the given available screen space.
        /// </summary>
        public static void ComputeLabelledFieldRects(Rect _Position, out Rect _LabelRect, out Rect _FieldRect)
        {
            float fieldWidth = _Position.width - EditorGUIUtility.labelWidth - MuffinDevGUI.HORIZONTAL_MARGIN;

            if (fieldWidth < 0)
            {
                _LabelRect = new Rect();
                _FieldRect = _Position;
            }

            else
            {
                _LabelRect = new Rect(_Position);
                _LabelRect.width = EditorGUIUtility.labelWidth;

                _FieldRect = new Rect(_Position);
                _FieldRect.width = fieldWidth;
                _FieldRect.x += _LabelRect.width + MuffinDevGUI.HORIZONTAL_MARGIN;
            }
        }

        #endregion


        #region Editor Styles

        public static GUIStyle HelpBoxStyle
        {
            get
            {
                GUIStyle style = new GUIStyle("HelpBox");
                style.richText = true;
                style.fontSize = 11;
                return style;
            }
        }

        public static GUIStyle ReorderableListHeaderStyle
        {
            get
            {
                GUIStyle style = new GUIStyle("RL Header");
                style.alignment = TextAnchor.MiddleLeft;
                style.fontStyle = FontStyle.Bold;
                style.fontSize = 11;
                style.padding.left = 8;

                return style;
            }
        }

        public static GUIStyle ReorderableListBoxStyle
        {
            get
            {
                GUIStyle style = new GUIStyle("RL Background");
                style.padding = new RectOffset(0, 0, 0, 0);

                return style;
            }
        }

        public static GUIStyle PropertyFieldButtonStyle
        {
            get
            {
                GUIStyle style = new GUIStyle(EditorStyles.miniButton);
                style.padding = new RectOffset(0, 0, 0, 0);

                return style;
            }
        }

        private static GUIStyle ExtendedObjectFieldButtonStyle
        {
            get
            {
                if(s_ExtendedObjectFieldButtonStyle == null)
                {
                    s_ExtendedObjectFieldButtonStyle = new GUIStyle(EditorStyles.miniButton);
                    s_ExtendedObjectFieldButtonStyle.padding = new RectOffset(MINI_BUTTON_PADDING, MINI_BUTTON_PADDING, MINI_BUTTON_PADDING, MINI_BUTTON_PADDING);
                }
                return s_ExtendedObjectFieldButtonStyle;
            }
        }

        #endregion

    }

}