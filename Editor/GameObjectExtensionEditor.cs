using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using Object = UnityEngine.Object;

namespace MuffinDev.EditorUtils
{

    /// <summary>
    /// Utility class to inherit for making GameObject editor extensions.
    /// This is based on the Work of Lucas Guibert, who made a multi-tags system for GameObjects:
    ///     - Lucas Guibert's Multi-Tags extension: https://github.com/LucasJoestar/Multi-Tags
    ///     - More about extending Editor class inspectors (FR): https://docs.google.com/document/d/1Ql_IPvYrfzT3Jep0-k2SzJlX6q2r48xG4sN_aiv_CqA/edit#heading=h.1zj7cjedrug2
    /// </summary>
    // Add this line in inheritors script
    // [CustomEditor(typeof(GameObject))]
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
            if (m_DefaultGameObjectEditor == null) return;
            
            // Check if the preview cache is set or not
            object previewCache = ReflectionUtility.GetFieldValue("m_PreviewCache", m_DefaultGameObjectEditor);

            // If the preview cache is not defined, call OnEnable() method to initialize the GameObject editor
            if (previewCache == null)
                ReflectionUtility.CallMethod("OnEnable", m_DefaultGameObjectEditor);

            DestroyImmediate(m_DefaultGameObjectEditor);
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

        /// <summary>
        /// Called when the GameObject is dragged to the scene view.
        /// NOTE: This override is necessary in order to load the object correctly when it's dragged into the scene.
        /// Without this, the object cannot be dragged from the Project browser to the Scene View (dragging it to the
        /// Hierarchy works though). See UnityEditor.GameObjectInspector class from Unity CS reference for more
        /// informations: https://github.com/Unity-Technologies/UnityCsReference
        /// </summary>
        /// <param name="_SceneView">The SceneView where this GameObject is dragged to.</param>
        public void OnSceneDrag(SceneView _SceneView)
        {
            if (m_DefaultGameObjectEditor == null)
                return;

            ReflectionUtility.CallMethod("OnSceneDrag", m_DefaultGameObjectEditor, new object[] { _SceneView });
        }

        /// <summary>
        /// Called when a list of assets including the target(s) GameObject(s) is displayed.
        /// NOTE: This override is necessary in order to load the asset preview correctly when displayed in the project
        /// browser. See UnityEditor.GameObjectInspector class from Unity CS reference for more informations:
        /// https://github.com/Unity-Technologies/UnityCsReference
        /// </summary>
        public override Texture2D RenderStaticPreview(string _AssetPath, Object[] _SubAssets, int _Width, int _Height)
        {
            if (m_DefaultGameObjectEditor == null)
                return null;

            return ReflectionUtility.CallMethod<Texture2D>("RenderStaticPreview", m_DefaultGameObjectEditor, new object[] { _AssetPath, _SubAssets, _Width, _Height });
        }

        /// <summary>
        /// Draws the preview of the GameObject.
        /// NOTE: This override is necessary in order to load the asset preview correctly when displayed in the
        /// inspector. See UnityEditor.GameObjectInspector class from Unity CS reference for more informations:
        /// https://github.com/Unity-Technologies/UnityCsReference
        /// </summary>
        public override void OnPreviewGUI(Rect _Rect, GUIStyle _Background)
        {
            if (m_DefaultGameObjectEditor == null)
                return;

            ReflectionUtility.CallMethod("OnPreviewGUI", m_DefaultGameObjectEditor, new object[] { _Rect, _Background });
        }

        #endregion

    }

}