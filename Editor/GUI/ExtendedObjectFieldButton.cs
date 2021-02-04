using System;

using UnityEngine;

namespace MuffinDev.Core.EditorOnly
{
    
    /// <summary>
    /// Represents an additional button for an Extended Object Field element.
    /// Mostly used by MuffinDevGUI.ExtendedObjectField().
    /// </summary>
    public class ExtendedObjectFieldButton
    {

        #region Enums & Subclasses

        /// <summary>
        /// Defines the position of the button in the drawn field.
        /// </summary>
        public enum EPosition
        {
            // This button should be drawn on the left of the field, before the label
            BeforeLabel = 0,
            // This button should be drawn between the label and the input field
            BeforeField = 1,
            // This button should be drawn on the right on the field
            AfterField = 2
        }

        #endregion


        #region Properties

        // The icon to use for this button
        public Texture icon = null;

        // The optional hovering tooltip to show when the user let his mouse over the button
        public string tooltip = string.Empty;

        // The optional text of the button
        public string text = string.Empty;

        // The expected position of the button when drawing the Extended Object Field
        public EPosition position = EPosition.AfterField;

        // The action to perform when the user clicks on the button
        public Action onClick = null;

        // Defines if this button is enabled
        public bool enabled = true;

        #endregion


        #region Initialization

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Content">The content (icon, tooltip and text) to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(GUIContent _Content, Action _OnClick, bool _Enabled = true)
        {
            icon = _Content.image;
            tooltip = _Content.tooltip;
            text = _Content.text;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Content">The content (icon, tooltip and text) to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(GUIContent _Content, EPosition _Position, Action _OnClick, bool _Enabled = true)
        {
            icon = _Content.image;
            tooltip = _Content.tooltip;
            text = _Content.text;
            onClick = _OnClick;
            position = _Position;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            position = _Position;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            position = _Position;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_Icon">The icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(Texture _Icon, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = _Icon;
            position = _Position;
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, EPosition _Position, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            position = _Position;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            position = _Position;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconName">The name of the icon to use for this button (from built-in resources).</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(string _IconName, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconName);
            position = _Position;
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            position = _Position;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            position = _Position;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        /// <summary>
        /// Creates an instance of ExtendedObjectFieldButton.
        /// </summary>
        /// <param name="_IconType">The type of icon to use for this button.</param>
        /// <param name="_Position">The expected position of the button when drawing the Extended Object Field.</param>
        /// <param name="_Text">The text to use for this button.</param>
        /// <param name="_Tooltip">The hovering tooltip to use for this button.</param>
        /// <param name="_OnClick">The action to perform when the user clicks on the button.</param>
        /// <param name="_Enabled">Defines if this button is enabled.</param>
        public ExtendedObjectFieldButton(EEditorIcon _IconType, EPosition _Position, string _Text, string _Tooltip, Action _OnClick, bool _Enabled = true)
        {
            icon = EditorIcons.FindIcon(_IconType);
            position = _Position;
            text = _Text;
            tooltip = _Tooltip;
            onClick = _OnClick;
            enabled = _Enabled;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Creates a GUIContent instance that contains all this button's data.
        /// </summary>
        public GUIContent GetContent()
        {
            return new GUIContent { image = icon, text = text, tooltip = tooltip };
        }

        #endregion

    }

}