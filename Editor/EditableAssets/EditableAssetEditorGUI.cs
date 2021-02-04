using System;

using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

    ///<summary>
    /// Utility to draw GUI for editable assets. If no asset is opened (or selected), it draws a list of all the assets of the given type
    /// that exists in the project, allowing user to create or open one.
    ///</summary>
    public abstract class EditableAssetEditorGUI<TAssetType>
        where TAssetType : ScriptableObject
    {

        #region Properties

        [SerializeField]
        private EditableAssetsList<TAssetType> m_AssetsListGUI = null;

        [SerializeField]
        private TAssetType m_OpenedAsset = null;

        [SerializeField]
        private SerializedObject m_SerializedOpenedAsset = null;

        // Called when the editor requires repaint.
        private Action m_RepaintCallback = null;

        #endregion


        #region Initialization

        /// <summary>
        /// Creates an editor GUI for custom editable assets.
        /// </summary>
        public EditableAssetEditorGUI() { }

        /// <summary>
        /// Creates an editor GUI for custom editable assets.
        /// </summary>
        /// <param name="_Asset">The asset which you want to display the editor.</param>
        /// <param name="_RepaintCallback">Optional callback to trigger when the content is repainted.</param>
        public EditableAssetEditorGUI(TAssetType _Asset, Action _RepaintCallback = null)
        {
            Asset = _Asset;
            m_RepaintCallback = _RepaintCallback;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Draws the assets list if the asset is not already opened, otherwise draws the asset GUI.
        /// </summary>
        public void DrawGUI()
        {
            if (Asset == null)
            {
                AssetsListGUI.DrawLayout();
                return;
            }
            else
                DrawAssetGUI();
        }

        /// <summary>
        /// Requires the editor view to repaint (by calling the Repaint Callback).
        /// </summary>
        public void Repaint()
        {
            if (m_RepaintCallback != null)
                m_RepaintCallback.Invoke();
        }

        /// <summary>
        /// Builds a VisualElement, for using this UI with UIElements API.
        /// </summary>
        public virtual VisualElement BuildVisualElement()
        {
            return null;
        }

        /// <summary>
        /// Draws the GUI of the opened asset.
        /// Note that this method is called from DrawGUI(), and by default the asset is not null at this step.
        /// </summary>
        protected virtual void DrawAssetGUI()
        {
            MuffinDevGUI.DrawDefaultInspector(Asset);
        }

        /// <summary>
        /// Called when the opened asset changes.
        /// </summary>
        protected virtual void OnChangeAsset() { }

        #endregion



        #region Accessors

        /// <summary>
        /// Gets/sets the action to trigger when repaint operation is required.
        /// </summary>
        public Action RepaintCallback
        {
            get { return m_RepaintCallback; }
            set { m_RepaintCallback = value; }
        }

        /// <summary>
        /// Gets/Sets the opened asset.
        /// </summary>
        public TAssetType Asset
        {
            get { return m_OpenedAsset; }
            set
            {
                if (m_OpenedAsset == value)
                    return;

                m_OpenedAsset = value;
                m_SerializedOpenedAsset = null;
                OnChangeAsset();
            }
        }

        /// <summary>
        /// Gets/Sets the opened asset.
        /// </summary>
        protected TAssetType Target
        {
            get { return Asset; }
            set { Asset = value; }
        }

        /// <summary>
        /// Gets/Sets the opened asset.
        /// </summary>
        protected TAssetType target
        {
            get { return Asset; }
            set { Asset = value; }
        }

        /// <summary>
        /// Gets the serialized object that represents the opened asset.
        /// </summary>
        protected SerializedObject SerializedAsset
        {
            get
            {
                if (m_SerializedOpenedAsset == null && Asset != null)
                    m_SerializedOpenedAsset = new SerializedObject(Asset);
                return m_SerializedOpenedAsset;
            }
        }

        /// <summary>
        /// Gets the serialized object that represents the opened asset.
        /// </summary>
        protected SerializedObject serializedObject
        {
            get { return SerializedAsset; }
        }

        /// <summary>
        /// Gets the editable assets list, in order to create or open one.
        /// </summary>
        protected EditableAssetsList<TAssetType> AssetsListGUI
        {
            get
            {
                if (m_AssetsListGUI == null)
                    m_AssetsListGUI = new EditableAssetsList<TAssetType>(asset => m_OpenedAsset = asset);
                return m_AssetsListGUI;
            }
        }

        #endregion

    }

}