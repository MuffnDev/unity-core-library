using UnityEngine;
using MuffinDev.Core.EditorOnly;

namespace MuffinDev.Core.Demos.EditableAsset
{

    ///<summary>
    /// Utility that draws a custom GUI for Character assets. If no asset is currently opened, invite the user to open one of the Character
    /// assets in the project, or create a new one.
    ///</summary>
    [System.Serializable]
    public class CharacterAssetEditorGUI : EditableAssetEditorGUI<CharacterAsset>
    {

        /// <summary>
        /// Creates an editor GUI for Character assets.
        /// </summary>
        /// <param name="_Asset">The asset which you want to display the editor.</param>
        /// <param name="_RepaintCallback">Optional callback to trigger when the content is repainted.</param>
        public CharacterAssetEditorGUI(CharacterAsset _Asset, System.Action _RepaintCallback = null)
            : base(_Asset, _RepaintCallback) { }

        protected override void DrawAssetGUI()
        {
            base.DrawAssetGUI();

            // Add a button to randomize the character stats
            if(GUILayout.Button("Randomize values"))
            {
                serializedObject.FindProperty("maxHP").intValue = Random.Range(50, 200);
                serializedObject.FindProperty("force").intValue = Random.Range(3, 30);
                serializedObject.FindProperty("defense").intValue = Random.Range(3, 30);
                serializedObject.ApplyModifiedProperties();
            }
        }

    }

}