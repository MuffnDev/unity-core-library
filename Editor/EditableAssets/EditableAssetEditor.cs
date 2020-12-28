using UnityEngine;

namespace MuffinDev.Core.EditorOnly
{

	///<summary>
	/// Shortcut for making editable assets custom inspector editor.
	///</summary>
	///<typeparam name="TAsset">The type of the editable assets.</typeparam>
	///<typeparam name="TEditorGUI">The type of the EditableAssetEditorGUI that represents GUI for the given editable asset
	///type.</typeparam>
	public class EditableAssetEditor<TAsset, TEditorGUI> : TEditor<TAsset>
		where TAsset : ScriptableObject
		where TEditorGUI : EditableAssetEditorGUI<TAsset>, new()
	{

		private TEditorGUI m_GUI = null;

		public override void OnInspectorGUI()
		{
			AssetGUI.DrawGUI();
		}

		/// <summary>
		/// Called after the Editor GUI has been created.
		/// Override this method to customize the editor GUI settings.
		/// </summary>
		protected virtual void AfterInitEditorGUI() { }

		/// <summary>
		/// Gets the editor GUI for the inspected asset.
		/// </summary>
		protected TEditorGUI AssetGUI
        {
			get
            {
				if(m_GUI == null)
                {
					m_GUI = new TEditorGUI();
					m_GUI.Asset = Target;
					m_GUI.RepaintCallback = Repaint;

					AfterInitEditorGUI();
				}
				return m_GUI;
            }
        }

	}

}