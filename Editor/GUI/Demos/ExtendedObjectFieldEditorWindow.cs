using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly.Demos
{

	///<summary>
	/// Editor window to demonstrate the Extended Object Field usage, from Editor Helpers.
	///</summary>
	public class ExtendedObjectFieldEditorWindow : EditorWindow
	{

		private const string EDITOR_WINDOW_MENU_ITEM = "Tools/Muffin Dev/Demos/Extended Object Field";
		private const string EDITOR_WINDOW_TITLE = "Extended Object Field Demo";

		[SerializeField]
		private Object m_Object = null;
		public Object Obj => m_Object;

		[SerializeField]
		private bool m_Folded = true;

		[SerializeField]
		private bool m_Locked = false;

        private void OnGUI()
        {
			EditorGUILayout.HelpBox("The following field has been drawn with MuffinDevGUI.ExtendedObjectField().\nUse the added controls to, from left to right:\n\t- Show/hide informations about the selected asset\n\t- Lock/unlock the selected asset\n\t- Focus the asset in the project view\n\t- Create a new asset if applicable", MessageType.Info);
			EditorGUILayout.Space();

			bool locked = m_Object != null && m_Locked;
			bool canChangeFoldState = m_Object != null;
			bool canFocus = m_Object != null;
			bool canCreate = m_Object != null ? m_Object is ScriptableObject : false;

			GUI.enabled = !locked;
			m_Object = MuffinDevGUI.ExtendedObjectField("Selected Asset", m_Object, typeof(Object), true, new ExtendedObjectFieldButton[]
			{
				// Add "Fold/Unfold" button
				new ExtendedObjectFieldButton(m_Folded ? EEditorIcon.Unfold : EEditorIcon.Fold, ExtendedObjectFieldButton.EPosition.BeforeLabel, m_Folded ? "Hide informations" : "Show informations", () =>
				{
					m_Folded = !m_Folded;
				}, canChangeFoldState),

				// Add "Focus" button
				new ExtendedObjectFieldButton(locked ? EEditorIcon.Lock : EEditorIcon.Unlock, ExtendedObjectFieldButton.EPosition.BeforeField, locked ? "Locked" : "Unlocked", locked ? "Unlock selected asset" : "Lock selected asset", () =>
				{
					m_Locked = !m_Locked;
				}, m_Object != null),

				// Add "Focus" button
				new ExtendedObjectFieldButton(EEditorIcon.Focus, ExtendedObjectFieldButton.EPosition.AfterField, "Focus asset", () =>
				{
					EditorHelpers.FocusAsset(m_Object, true, true);
				}, canFocus),

				// Add "Create" button
				new ExtendedObjectFieldButton(EEditorIcon.Add, ExtendedObjectFieldButton.EPosition.AfterField, $"Create new {(m_Object != null ? m_Object.GetType().Name : "Object")} asset", () =>
				{
					if(m_Object != null)
                    {
						EditorHelpers.CreateAssetPanel(m_Object.GetType(), out Object newAsset, $"Create new {m_Object.GetType().Name} asset", $"New{m_Object.GetType().Name}", "", "asset", false);
						if(newAsset != null)
							m_Object = newAsset;
					}
				}, canCreate)
			});
			GUI.enabled = true;

			if (!m_Folded)
            {
				EditorGUI.indentLevel++;
				using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
					EditorGUILayout.LabelField("Asset Informations", EditorStyles.boldLabel);

					if (m_Object == null)
						EditorGUILayout.LabelField("No asset selected...");
					else
					{
						EditorGUILayout.LabelField("Asset Type", m_Object.GetType().Name);
						EditorGUILayout.LabelField("Asset Path", AssetDatabase.GetAssetPath(m_Object));
					}
				}
				EditorGUI.indentLevel--;
            }
		}

        /// <summary>
        /// Gets the window of this tool, or create it if it's not already open.
        /// </summary>
        [MenuItem(EDITOR_WINDOW_MENU_ITEM)]
		private static void ShowWindow()
		{
			ExtendedObjectFieldEditorWindow window = GetWindow<ExtendedObjectFieldEditorWindow>(false, EDITOR_WINDOW_TITLE, true);
			window.Show();
		}

	}

}