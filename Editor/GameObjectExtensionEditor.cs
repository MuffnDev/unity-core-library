using System;
using System.Reflection;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.EditorUtils
{

    /// <summary>
    /// Utility class to inherit for making GameObject editor extensions.
    /// This is based on the Work of Lucas Guibert, who made a multi-tags system for GameObjects:
    ///     - Lucas Guibert's Multi-Tags extension: https://github.com/LucasJoestar/Multi-Tags
    ///     - More about extending Editor class inspectors (FR): https://docs.google.com/document/d/1Ql_IPvYrfzT3Jep0-k2SzJlX6q2r48xG4sN_aiv_CqA/edit#heading=h.1zj7cjedrug2
    /// </summary>
    // Add this line in inheritors script
    //[CustomEditor(typeof(GameObject))]
    public abstract class GameObjectExtensionEditor : TEditor<GameObject>
    {

        #region Properties

        private Editor m_DefaultGameObjectEditor = null;

        #endregion


        #region Lifecycle

        protected virtual void OnEnable()
        {
            // Gets the default inspector view of GameObject
            m_DefaultGameObjectEditor = CreateEditor(target, Type.GetType("UnityEditor.GameObjectInspector, UnityEditor"));
        }

        protected virtual void OnDisable()
        {
            if (m_DefaultGameObjectEditor != null)
            {
                // Check if the preview cache is set or not
                object previewCache = m_DefaultGameObjectEditor.GetType()
                    .GetField("m_PreviewCache", BindingFlags.Instance | BindingFlags.NonPublic)
                    .GetValue(m_DefaultGameObjectEditor);

                // If the preview cache is not defined, call OnEnable() method to initialize the GameObject editor
                if (previewCache == null)
                {
                    m_DefaultGameObjectEditor.GetType().GetMethod("OnEnable").Invoke(m_DefaultGameObjectEditor, null);
                }

                DestroyImmediate(m_DefaultGameObjectEditor);
            }
        }

        #endregion


        #region UI

        public override void OnInspectorGUI()
        {
            m_DefaultGameObjectEditor.OnInspectorGUI();
        }

        protected override void OnHeaderGUI()
        {
            m_DefaultGameObjectEditor.DrawHeader();
        }

        #endregion

    }

}