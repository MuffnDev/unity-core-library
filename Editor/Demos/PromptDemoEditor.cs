using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorUtils.Demos
{

	///<summary>
	/// This editor window demonsttrates how to use the Prompt utility.
	///</summary>
	public class PromptDemoEditor : EditorWindow
	{

		private const string EDITOR_WINDOW_MENU_ITEM = "Tools/Muffin Dev/Demos/Prompt Dialog";
		private const string EDITOR_WINDOW_TITLE = "Prompt Dialog";
		private const int EDITOR_WINDOW_PRIORITY = 1;

		[SerializeField]
		private string m_PlayerName = string.Empty;

		private void OnGUI()
        {
			// Display the player name field, disabled. So the user must click on the "Change Player Name" button to set it
			GUI.enabled = false;
			EditorGUILayout.TextField("Player Name", m_PlayerName);
			GUI.enabled = true;

			if (GUILayout.Button("Change Player Name", GUILayout.Height(40f)))
            {
				// Display a prompt dialog window that will set the new player name when the user confirms
				Prompt.DisplayPrompt("Change Player Name", "What is the new name of the player?\n\nYou can validate by clicking on the \"Ok\" button, or by pressing the return key.\nYou can cancel by clicking on the \"Cancel\" button, or by pressing the escape key.", this, (answer) =>
				{
					m_PlayerName = answer;
					Repaint();
				});
            }
		}

		/// <summary>
		/// Gets the window of this tool, or create it if it's not already open.
		/// </summary>
		[MenuItem(EDITOR_WINDOW_MENU_ITEM, false, EDITOR_WINDOW_PRIORITY)]
		private static void ShowWindow()
		{
			PromptDemoEditor window = GetWindow<PromptDemoEditor>(false, EDITOR_WINDOW_TITLE, true);
			window.Show();
		}

	}

}