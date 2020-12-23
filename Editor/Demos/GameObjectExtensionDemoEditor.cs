// #define ENABLE_DEMO

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly.Demos
{

    ///<summary>
    /// Demo of GameObjectExtensionEditor class usage.
    /// You must uncomment the very first line of this script to see the result.
    /// When ENABLE_DEMO define is enabled, you'll see a list of the components of all GameObjects, right under their name in the inspector.
    ///</summary>
    #if ENABLE_DEMO
    [CustomEditor(typeof(GameObject))]
    #endif
    public class GameObjectExtensionDemoEditor : GameObjectExtensionEditor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.LabelField("Components on this GameObject:", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            Component[] components = Target.GetComponents<Component>();
            for(int i = 0; i < components.Length; i++)
            {
                EditorGUILayout.LabelField($"[{i.ToString()}] {components[i].GetType().Name}");
            }
            EditorGUI.indentLevel--;
        }

    }

}