using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Shortcut for making editable assets editor windows, which updates the GUI depending on the selection.
	///</summary>
	///<typeparam name="TAsset">The type of the editable assets.</typeparam>
	///<typeparam name="TEditorGUI">The type of the EditableAssetEditorGUI that represents GUI for the given editable asset
	///type.</typeparam>
	public abstract class EditableAssetEditorWindow<TAsset, TEditorGUI> : EditorWindow
        where TAsset : ScriptableObject
        where TEditorGUI : EditableAssetEditorGUI<TAsset>, new()
	{

		[SerializeField]
		private TEditorGUI m_AssetGUI = null;

		[SerializeField]
		private Vector2 m_AssetGUIScrollPosition = Vector2.zero;

		/// <summary>
		/// Called when this window is opened.
		/// </summary>
		protected virtual void OnEnable()
        {
			OnSelectionChange();
			Selection.selectionChanged += OnSelectionChange;
		}

		/// <summary>
		/// Called when this window is closed.
		/// </summary>
		protected virtual void OnDisable()
        {
			Selection.selectionChanged -= OnSelectionChange;
		}

		/// <summary>
		/// Called when the selection changes.
		/// </summary>
		private void OnSelectionChange()
		{
			if (Selection.activeObject is TAsset)
				AssetGUI.Asset = Selection.activeObject as TAsset;
		}

		/// <summary>
		/// Draws the editor window GUI.
		/// </summary>
		protected virtual void OnGUI()
        {
			m_AssetGUIScrollPosition = EditorGUILayout.BeginScrollView(m_AssetGUIScrollPosition);
            {
				AssetGUI.DrawGUI();
            }
			EditorGUILayout.EndScrollView();
		}

		/// <summary>
		/// Draws a toolbar, which contains a "Back" button that goes back to the assets list.
		/// </summary>
		protected void DrawDefaultToolbar()
        {
			using (new GUILayout.HorizontalScope(EditorStyles.toolbar, GUILayout.ExpandWidth(true)))
			{
				GUI.enabled = AssetGUI.Asset != null;
				GUIContent backBtnContent = EditorGUIUtility.IconContent("tab_prev", "Back to assets list");
				backBtnContent.text = "Back";
				if (GUILayout.Button(backBtnContent, EditorStyles.toolbarButton, GUILayout.Width(EditorHelpers.DEFAULT_BACK_BUTTON_WIDTH)))
					AssetGUI.Asset = null;
				GUI.enabled = true;
			}
		}

		/// <summary>
		/// Gets the editor GUI to draw in this window.
		/// </summary>
		protected TEditorGUI AssetGUI
        {
            get
            {
				if (m_AssetGUI == null)
                {
					m_AssetGUI = new TEditorGUI();
					m_AssetGUI.RepaintCallback += Repaint;
				}
				return m_AssetGUI;
            }
		}

		/// <summary>
		/// Gets the scroll position of the editor GUI.
		/// </summary>
		protected Vector2 AssetGUIScrollPosition
        {
            get { return m_AssetGUIScrollPosition; }
            set { m_AssetGUIScrollPosition = value; }
        }

	}

}