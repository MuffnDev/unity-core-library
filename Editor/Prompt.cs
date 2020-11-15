using UnityEngine;
using UnityEditor;

namespace MuffinDev.EditorUtils
{

	///<summary>
	/// Editor utility that displays a Prompt dialog window, allowing the user to type a text, validate or cancel.
	///</summary>
	public class Prompt : EditorWindow
	{

		#region Properties

		/// <summary>
		/// Used to send the typed value when the prompt dialog is confirmed.
		/// </summary>
		/// <param name="_Answer">The prompt's input field value.</param>
		public delegate void OnConfirmDelegate(string _Answer);

		/// <summary>
		/// Used to send a feedback when the prompt dialog is cancelled.
		/// </summary>
		public delegate void OnCancelDelegate();

		private const string ANSWER_FIELD_NAME = "PromptAnswer";
		private const float INPUT_HEIGHT = 24f;

		private static Prompt s_OpenedWindow = null;
		private static GUIStyle s_PromptMessageBoxStyle = null;
		private static GUIStyle s_PromptInputStyle = null;

		private string m_Message = string.Empty;
		private string m_Answer = string.Empty;
		private bool m_HasSentFeedback = false;
		private bool m_HasBeenClosed = false;

		// Called when the user clicks on the "Ok" button or press the "Return" key
		private OnConfirmDelegate m_OnConfirmCallback = null;
		// Called when the user clicks on the "Cancel" button or press the "Escape" key
		private OnCancelDelegate m_OnCancelCallback = null;

        #endregion


        #region Lifecycle

		private void OnDisable()
        {
			// Ensures the cancel callback has been called as expected
			Cancel();
        }

		#endregion


		#region Public Methods

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
        {
			DisplayPrompt(_Title, string.Empty, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
        {
			DisplayPrompt(_Title, _Message, new Rect(), _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_Position">The position of the prompt window.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, Vector2 _Position, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, _Message, new Rect(_Position, _Size), _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Position">The position of the prompt window.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, Vector2 _Position, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, string.Empty, new Rect(_Position, _Size), _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, string.Empty, _OwnerWindow, Vector2.zero, Vector2.zero, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, _Message, _OwnerWindow, Vector2.zero, Vector2.zero, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, string.Empty, _OwnerWindow, Vector2.zero, _Size, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, _Message, _OwnerWindow, Vector2.zero, _Size, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_Offset">Adds this offset to the owner window's position.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, EditorWindow _OwnerWindow, Vector2 _Offset, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, string.Empty, _OwnerWindow, _Offset, _Size, _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_OwnerWindow">The window from which the prompt utility is displayed. The prompt will be placed at the same coordinates
		/// of its owning popup.</param>
		/// <param name="_Offset">Adds this offset to the owner window's position.</param>
		/// <param name="_Size">The size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, EditorWindow _OwnerWindow, Vector2 _Offset, Vector2 _Size, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
		{
			DisplayPrompt(_Title, _Message, new Rect(_OwnerWindow.position.position + _Offset, _Size), _OnConfirmCallback, _OnCancelCallback);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Position">The position and size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, Rect _Position, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
        {
			DisplayPrompt(_Title, string.Empty, _Position, _OnConfirmCallback, _OnCancelCallback = null);
		}

		/// <summary>
		/// Display a prompt utility window.
		/// </summary>
		/// <param name="_Title">The window title.</param>
		/// <param name="_Message">The message to display to the user.</param>
		/// <param name="_Position">The position and size of the prompt window.</param>
		/// <param name="_OnConfirmCallback">This method will be called once the user clicks on the "Ok" button.</param>
		/// <param name="_OnCancelCallback">This method will be called once the user clicks on the "Cancel" button or clicks out of the prompt
		/// window.</param>
		public static void DisplayPrompt(string _Title, string _Message, Rect _Position, OnConfirmDelegate _OnConfirmCallback, OnCancelDelegate _OnCancelCallback = null)
        {
			// Close the eventually existing window
			if (s_OpenedWindow != null)
			{
				s_OpenedWindow.Cancel();
			}

			s_OpenedWindow = GetWindow<Prompt>(true, _Title, true);

			// Set size and position if needed.
			Rect rect = s_OpenedWindow.position;
			if(_Position.position != Vector2.zero)
            {
				rect.position = _Position.position;
			}
			if(_Position.size != Vector2.zero)
            {
				rect.size = _Position.size;
            }
			s_OpenedWindow.position = rect;

			// Initialize prompt window
			s_OpenedWindow.m_Message = _Message;
			s_OpenedWindow.m_OnConfirmCallback = _OnConfirmCallback;
			s_OpenedWindow.m_OnCancelCallback = _OnCancelCallback;

			s_OpenedWindow.Show();
		}

		#endregion


		#region GUI

		/// <summary>
		/// Draw the prompt GUI.
		/// </summary>
		private void OnGUI()
		{
			// Display user message
			if(!string.IsNullOrEmpty(m_Message))
            {
				GUILayout.Box(m_Message, PromptMessageBoxStyle, GUILayout.ExpandWidth(true));
            }

			// Confirm prompt input when pressing Enter or Return, or cancel it when pressing Escape
			if (Event.current.type == EventType.KeyDown)
			{
				if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter)
				{
					Confirm();
					Event.current.Use();
				}
				else if (Event.current.keyCode == KeyCode.Escape)
				{
					Cancel();
					Event.current.Use();
				}
			}

			// Draw input field
			EditorGUILayout.Space();
			GUI.SetNextControlName(ANSWER_FIELD_NAME);
			m_Answer = EditorGUILayout.TextField(m_Answer, PromptInputStyle, GUILayout.Height(INPUT_HEIGHT));
			EditorGUI.FocusTextInControl(ANSWER_FIELD_NAME);

			// Draw prompt buttons
			EditorGUILayout.Space();
			EditorGUILayout.BeginHorizontal();
			{
				if (GUILayout.Button("Cancel"))
				{
					Cancel();
				}

				if (GUILayout.Button("Ok"))
				{
					Confirm();
				}
			}
			EditorGUILayout.EndHorizontal();
		}

		/// <summary>
		/// Called when the window lost the focus in the editor.
		/// </summary>
		private void OnLostFocus()
        {
			Cancel();
        }

		/// <summary>
		/// Trigger the OnConfirm callback and close the prompt window.
		/// </summary>
		private void Confirm()
		{
			if (m_HasSentFeedback)
				return;

			m_HasSentFeedback = true;
			if (m_OnConfirmCallback != null)
				m_OnConfirmCallback.Invoke(m_Answer);
			CloseOnce();
		}

		/// <summary>
		/// Trigger the OnCancel callback and close the prompt window.
		/// </summary>
		private void Cancel()
		{
			if (m_HasSentFeedback)
				return;

			m_HasSentFeedback = true;
			if(m_OnCancelCallback != null)
				m_OnCancelCallback.Invoke();
			CloseOnce();
		}

		/// <summary>
		/// Avoid multiple destroy errors.
		/// </summary>
		private void CloseOnce()
        {
			if(!m_HasBeenClosed)
            {
				Close();
				m_HasBeenClosed = true;
            }
        }

        #endregion


        #region Accessors

		/// <summary>
		/// Gets the prompt's message box style.
		/// </summary>
		public static GUIStyle PromptMessageBoxStyle
        {
			get
            {
				if(s_PromptMessageBoxStyle == null)
                {
					s_PromptMessageBoxStyle = new GUIStyle(GUI.skin.box);
					s_PromptMessageBoxStyle.padding = new RectOffset(8, 8, 8, 8);
                }
				return s_PromptMessageBoxStyle;
			}
		}


		/// <summary>
		/// Gets the prompt's input field style.
		/// </summary>
		public static GUIStyle PromptInputStyle
		{
			get
			{
				if (s_PromptInputStyle == null)
				{
					s_PromptInputStyle = new GUIStyle(EditorStyles.textField);
					s_PromptInputStyle.alignment = TextAnchor.MiddleLeft;
					s_PromptInputStyle.padding = new RectOffset(8, 8, 0, 0);
				}
				return s_PromptInputStyle;
			}
		}

		#endregion

	}

}