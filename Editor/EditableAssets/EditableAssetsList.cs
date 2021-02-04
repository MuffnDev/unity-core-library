using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

    ///<summary>
    /// GUI utility to display a list of assets that can be edited using a custom editor.
    ///</summary>
    [System.Serializable]
    public class EditableAssetsList<T>
        where T : ScriptableObject
	{

        #region Properties

        private const float BUTTONS_WIDTH = 140f;
        private const float ASSET_NAME_MAX_WIDTH = 260f;

        private const string DEFAULT_OPEN_ASSET_BUTTON_LABEL = "Open Asset";
        private const string DEFAULT_CREATE_ASSET_BUTTON_LABEL = "Create New Asset...";

        /// <summary>
        /// Invoked when an asset is opened.
        /// </summary>
        /// <param name="_Asset">The opened asset.</param>
        public delegate void OpenAssetDelegate(T _Asset);
        private event OpenAssetDelegate m_OnOpenAsset = null;

        // Settings

        [SerializeField]
        [Tooltip("If enabled, an opened asset will automatically be selected in the editor, and pinged in the Project window")]
        private bool m_AutoSelectOpenedAsset = true;

        [SerializeField]
        [Tooltip("If enabled, draws a \"Create New Asset\" button that allow user to create new assets directly from the GUI")]
        private bool m_AllowCreate = true;

        [SerializeField]
        [Tooltip("If enabled, a new created asset will automatically be opened. Used only if Allow Create is set to true")]
        private bool m_AutoOpenCreatedAsset = true;

        [SerializeField]
        private string m_Title = null;

        [SerializeField]
        private string m_OpenAssetButtonLabel = null;

        [SerializeField]
        private string m_CreateAssetButtonLabel = null;

        [SerializeField]
        private string m_InfoMessage = "Please select the asset you want to edit.";

        [SerializeField]
        private MessageType m_InfoVerbosity = MessageType.Info;

        // Cache

        private T[] m_Assets = null;

        private string m_TypeName = null;
        private string m_DisplayableTypeName = null;

        private List<Func<T, bool>> m_Filters = new List<Func<T, bool>>();

        #endregion


        #region Initialization

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="_AutoOpenCreatedAsset">If true, an opened asset will automatically be selected in the editor, and pinged in the Project window.</param>
        /// <param name="_AllowCreate">If true, draws a "Create New Asset" button that allow user to create new assets directly from the GUI.</param>
        /// <param name="_AutoSelectOpenedAsset">If true, a new created asset will automatically be opened. Used only if Allow Create is set to true.</param>
        public EditableAssetsList(bool _AutoOpenCreatedAsset = true, bool _AllowCreate = true, bool _AutoSelectOpenedAsset = true)
        {
            m_AllowCreate = _AllowCreate;
            m_AutoSelectOpenedAsset = _AutoSelectOpenedAsset;
            m_AutoOpenCreatedAsset = _AutoOpenCreatedAsset;
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="_OnOpenAsset">The method to call when an asset is opened.</param>
        /// <param name="_AutoOpenCreatedAsset">If true, an opened asset will automatically be selected in the editor, and pinged in the Project window.</param>
        /// <param name="_AllowCreate">If true, draws a "Create New Asset" button that allow user to create new assets directly from the GUI.</param>
        /// <param name="_AutoSelectOpenedAsset">If true, a new created asset will automatically be opened. Used only if Allow Create is set to true.</param>
        public EditableAssetsList(OpenAssetDelegate _OnOpenAsset, bool _AutoOpenCreatedAsset = true, bool _AllowCreate = true, bool _AutoSelectOpenedAsset = true)
        {
            m_AllowCreate = _AllowCreate;
            m_AutoSelectOpenedAsset = _AutoSelectOpenedAsset;
            m_AutoOpenCreatedAsset = _AutoOpenCreatedAsset;
            m_OnOpenAsset += _OnOpenAsset;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Draws the Assets List GUI, using Layout methods.
        /// </summary>
        public void DrawLayout()
        {
            if (!string.IsNullOrEmpty(InfoMessage))
                EditorGUILayout.HelpBox(InfoMessage, InfoMessageVerbosity);

            // Draw title and "Refresh" button
            EditorGUILayout.Space();
            using (new GUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(Title, EditorStyles.largeLabel);
                if (GUILayout.Button(EditorGUIUtility.IconContent("Refresh"), GUILayout.Width(EditorGUIUtility.singleLineHeight * 2)))
                    Refresh();
            }

            // Draw the assets list
            T assetToOpen = null;
            using (new GUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // Draw table headers
                using (new GUILayout.HorizontalScope())
                {
                    EditorGUILayout.GetControlRect(GUILayout.Width(BUTTONS_WIDTH));
                    EditorGUILayout.LabelField("Name", GUILayout.MaxWidth(ASSET_NAME_MAX_WIDTH));
                    EditorGUILayout.LabelField("Path");
                }
                MuffinDevGUI.HorizontalLine();

                // For each asset of the target type in the project...
                foreach (T asset in Assets)
                {
                    // Draw the "Open Asset" button, the name and the project path of the current asset
                    using (new GUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button(OpenAssetButtonLabel, GUILayout.Width(BUTTONS_WIDTH)))
                            assetToOpen = asset;

                        EditorGUILayout.LabelField(asset.name, EditorStyles.boldLabel, GUILayout.MaxWidth(ASSET_NAME_MAX_WIDTH));
                        EditorGUILayout.LabelField(AssetDatabase.GetAssetPath(asset));
                    }
                }

                // If "Allow Create" option is enabled, draw "Create New Asset" button
                if (m_AllowCreate && GUILayout.Button(CreateAssetButtonLabel, GUILayout.Width(BUTTONS_WIDTH)))
                {
                    AssetCreationResult result = EditorHelpers.CreateAssetPanel<T>($"Create new {DisplayableTypeName} asset", $"New{TypeName}", EditorHelpers.ASSETS_FOLDER, EditorHelpers.DEFAULT_ASSET_EXTENSION, false);
                    if (result && m_AutoOpenCreatedAsset)
                    {
                        assetToOpen = result.GetAsset<T>();
                        Refresh();
                    }
                }
            }

            // Open the selected asset if required
            if (assetToOpen != null)
            {
                if (m_AutoSelectOpenedAsset)
                {
                    EditorUtility.FocusProjectWindow();
                    EditorHelpers.FocusAsset(assetToOpen);
                }

                if(OnOpenAsset != null)
                    OnOpenAsset.Invoke(assetToOpen);
            }
        }

        /// <summary>
        /// Refreshes the displayed assets list, and apply the registered filters.
        /// </summary>
        public void Refresh()
        {
            m_Assets = EditorHelpers.FindAllAssetsOfType<T>();

            List<T> filteredAssets = new List<T>();
            foreach(Func<T, bool> filter in m_Filters)
            {
                filteredAssets.Clear();
                foreach (T asset in m_Assets)
                {
                    if (filter(asset))
                        filteredAssets.Add(asset);
                }
                m_Assets = filteredAssets.ToArray();
            }
        }

        /// <summary>
        /// Adds a filter method to filter the assets list when refreshed.
        /// </summary>
        /// <param name="_FilterMethod">The filter method used to check if an asset can be included in the assets list.</param>
        public void AddFilter(Func<T, bool> _FilterMethod)
        {
            if (_FilterMethod != null)
            {
                m_Filters.Add(_FilterMethod);
                Refresh();
            }
        }

        #endregion


        #region Accessors

        /// <summary>
        /// If true, an opened asset will automatically be selected in the editor, and pinged in the Project window.
        /// </summary>
        public bool AutoOpenCreatedAsset
        {
            get { return m_AutoOpenCreatedAsset; }
            set { m_AutoOpenCreatedAsset = value; }
        }

        /// <summary>
        /// If true, draws a "Create New Asset" button that allow user to create new assets directly from the GUI.
        /// </summary>
        public bool AllowCreate
        {
            get { return m_AllowCreate; }
            set { m_AllowCreate = value; }
        }

        /// <summary>
        /// If true, a new created asset will automatically be opened. Used only if Allow Create is set to true.
        /// </summary>
        public bool AutoSelectOpenedAsset
        {
            get { return m_AutoSelectOpenedAsset; }
            set { m_AutoSelectOpenedAsset = value; }
        }

        /// <summary>
        /// Invoked when an asset is opened.
        /// </summary>
        public OpenAssetDelegate OnOpenAsset
        {
            get { return m_OnOpenAsset; }
        }

        /// <summary>
        /// Gets the list of all assets of the target type in this project.
        /// </summary>
        public T[] Assets
        {
            get
            {
                if (m_Assets == null)
                    Refresh();
                return m_Assets;
            }
        }

        /// <summary>
        /// Gets/sets the title of the assets list.
        /// </summary>
        public string Title
        {
            get { return string.IsNullOrEmpty(m_Title) ? $"{DisplayableTypeName} assets list" : m_Title; }
            set { m_Title = value; }
        }

        /// <summary>
        /// Gets/sets the label of the "Open Asset" button.
        /// </summary>
        public string OpenAssetButtonLabel
        {
            get { return string.IsNullOrEmpty(m_OpenAssetButtonLabel) ? DEFAULT_OPEN_ASSET_BUTTON_LABEL : m_OpenAssetButtonLabel; }
            set { m_OpenAssetButtonLabel = value; }
        }

        /// <summary>
        /// Gets/sets the label of the "Create Asset..." button.
        /// </summary>
        public string CreateAssetButtonLabel
        {
            get { return string.IsNullOrEmpty(m_CreateAssetButtonLabel) ? DEFAULT_CREATE_ASSET_BUTTON_LABEL : m_CreateAssetButtonLabel; }
            set { m_CreateAssetButtonLabel = value; }
        }

        /// <summary>
        /// Gets/sets the content of the info message displayed above the list.
        /// </summary>
        public string InfoMessage
        {
            get { return m_InfoMessage; }
            set { m_InfoMessage = value; }
        }

        /// <summary>
        /// Gets/sets the icon of the message displayed above the list.
        /// </summary>
        public MessageType InfoMessageVerbosity
        {
            get { return m_InfoVerbosity; }
            set { m_InfoVerbosity = value; }
        }

        /// <summary>
        /// Gets the name of the target type.
        /// </summary>
        private string TypeName
        {
            get
            {
                if (m_TypeName == null)
                    m_TypeName = typeof(T).Name;
                return m_TypeName;
            }
        }

        /// <summary>
        /// Gets the "displayable" name of the target type.
        /// </summary>
        private string DisplayableTypeName
        {
            get
            {
                if (m_DisplayableTypeName == null)
                    m_DisplayableTypeName = ObjectNames.NicifyVariableName(typeof(T).Name);
                return m_DisplayableTypeName;
            }
        }

        #endregion

    }

}