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
            if (!ReflectionUtility.CallMethod<bool>("HasStaticPreview", m_DefaultGameObjectEditor) || !ShaderUtil.hardwareSupportsRectRenderTexture)
                return null;
            object previewData = ReflectionUtility.CallMethod<object>("GetPreviewData", m_DefaultGameObjectEditor);
            PreviewRenderUtility renderUtility = (PreviewRenderUtility)ReflectionUtility.GetNestedType("PreviewData", m_DefaultGameObjectEditor)
                .GetField("renderUtility").GetValue(previewData);
            renderUtility.BeginStaticPreview(new Rect(0.0f, 0.0f, (float) _Width, (float) _Height));
            ReflectionUtility.CallMethod("DoRenderPreview", m_DefaultGameObjectEditor);
            return renderUtility.EndStaticPreview();
        }
        
        /// <summary>
        /// Draws the preview of the GameObject.
        /// NOTE: This override is necessary in order to load the asset preview correctly when displayed in the
        /// inspector. See UnityEditor.GameObjectInspector class from Unity CS reference for more informations:
        /// https://github.com/Unity-Technologies/UnityCsReference
        /// </summary>
        public override void OnPreviewGUI(Rect _Rect, GUIStyle _Background)
        {
            if (!ShaderUtil.hardwareSupportsRectRenderTexture)
            {
                if (Event.current.type != UnityEngine.EventType.Repaint)
                    return;
                EditorGUI.DropShadowLabel(new Rect(_Rect.x, _Rect.y, _Rect.width, 40f), "Preview requires\nrender texture support");
            }
            else
            {
                Vector2 vector2 = (Vector2)Type.GetType("PreviewGUI, UnityEditor")
                    .GetMethod("Drag2D", ReflectionUtility.STATIC)
                    .Invoke(null, new object[] { ReflectionUtility.GetFieldValue("m_PreviewDir", m_DefaultGameObjectEditor), _Rect });
                
                if (vector2 != ReflectionUtility.GetFieldValue<Vector2>("m_PreviewDir", m_DefaultGameObjectEditor))
                {
                    ReflectionUtility.CallMethod("ClearPreviewCache", m_DefaultGameObjectEditor);
                    ReflectionUtility.SetFieldValue("m_PreviewDir", vector2, m_DefaultGameObjectEditor);
                }

                if (Event.current.type != UnityEngine.EventType.Repaint)
                    return;

                if (ReflectionUtility.GetFieldValue<Rect>("m_PreviewRect", m_DefaultGameObjectEditor) != _Rect)
                {
                    ReflectionUtility.CallMethod("ClearPreviewCache", m_DefaultGameObjectEditor);
                    ReflectionUtility.SetFieldValue("m_PreviewRect", _Rect, m_DefaultGameObjectEditor);
                }
                
                object previewData = ReflectionUtility.CallMethod<object>("GetPreviewData", m_DefaultGameObjectEditor);
                PreviewRenderUtility renderUtility = (PreviewRenderUtility) ReflectionUtility.GetNestedType("PreviewData", m_DefaultGameObjectEditor).GetField("renderUtility").GetValue(previewData);

                Dictionary<int, Texture> previewCache = ReflectionUtility.GetFieldValue<Dictionary<int, Texture>>("m_PreviewCache", m_DefaultGameObjectEditor);
                int referenceTargetIndex = ReflectionUtility.GetPropertyValue<int>("referenceTargetIndex", m_DefaultGameObjectEditor);
                if (previewCache.TryGetValue(referenceTargetIndex, out Texture texture))
                {
                    typeof(PreviewRenderUtility).GetMethod("DrawPreview", ReflectionUtility.STATIC)
                        .Invoke(null, new object[] { _Rect, texture });
                }
                else
                {
                    renderUtility.BeginPreview(_Rect, _Background);
                    ReflectionUtility.CallMethod("DoRenderPreview", m_DefaultGameObjectEditor);
                    renderUtility.EndAndDrawPreview(_Rect);
                    RenderTexture dest = (RenderTexture) (renderUtility.GetType().GetProperty("renderTexture", ReflectionUtility.INSTANCE).GetValue(renderUtility));
                    RenderTexture active = RenderTexture.active;
                    Graphics.Blit((RenderTexture) (renderUtility.GetType().GetProperty("renderTexture", ReflectionUtility.INSTANCE).GetValue(renderUtility)), dest);
                    RenderTexture.active = active;
                    previewCache.Add(referenceTargetIndex, dest);
                }
            }
        }

        #endregion

    }

}