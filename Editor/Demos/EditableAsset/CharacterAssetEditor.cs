using UnityEngine;
using UnityEditor;

using MuffinDev.Core.EditorOnly;

namespace MuffinDev.Core.Demos.EditableAsset
{

	///<summary>
	/// Used for EditableAsset demo.
	/// Custom editor for Character asset.
	///</summary>
	[CustomEditor(typeof(CharacterAsset))]
	public class CharacterAssetEditor : TEditor<CharacterAsset>
	{

		[SerializeField]
		private CharacterAssetEditorGUI m_GUI = null;

		private void OnEnable()
		{
			// Initialize the asset's editor GUI
			m_GUI = new CharacterAssetEditorGUI(Target, Repaint);
		}

		public override void OnInspectorGUI()
		{
			// Draw the asset's editor GUI
			m_GUI.DrawGUI();
		}

	}

}