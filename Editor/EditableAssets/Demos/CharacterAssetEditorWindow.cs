using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.Demos.EditableAsset
{

	///<summary>
	/// Used for EditableAsset demo.
	/// Editor window for Character asset.
	///</summary>
	public class CharacterAssetEditorWindow : EditorWindow
	{

		private const string EDITOR_WINDOW_MENU_ITEM = "Tools/Muffin Dev/Demos/Character Asset Editor";
		private const string EDITOR_WINDOW_TITLE = "Characte Asset";

		[SerializeField]
		private CharacterAssetEditorGUI m_GUI = null;

		private void OnEnable()
		{
			// Initialize the Charcter Assets editor GUI
			m_GUI = new CharacterAssetEditorGUI(null, Repaint);

			// Make the window display the settings of the selected Character asset, even if the selection changes
			OnSelectionChange();
			Selection.selectionChanged += OnSelectionChange;
		}

		private void OnDisable()
		{
			Selection.selectionChanged -= OnSelectionChange;
		}

		private void OnGUI()
		{
			// Draw the Character assets list, or the selected asset's editor
			m_GUI.DrawGUI();
		}

		// Focuses the window of this tool, or create it if it's not already open.
		[MenuItem(EDITOR_WINDOW_MENU_ITEM, false)]
		private static void ShowWindow()
		{
			CharacterAssetEditorWindow window = GetWindow<CharacterAssetEditorWindow>(false, EDITOR_WINDOW_TITLE, true);
			window.Show();
		}

		// Called when the selected object changes, so the window will display the editor of the selected asset
		private void OnSelectionChange()
		{
			if (Selection.activeObject is CharacterAsset)
				m_GUI.Asset = Selection.activeObject as CharacterAsset;
		}

	}

}