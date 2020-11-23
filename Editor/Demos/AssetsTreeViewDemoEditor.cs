using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace MuffinDev.Core.EditorUtils.Demos
{

    /// <summary>
    /// 
    /// </summary>
    public class AssetsTreeViewDemoEditor : EditorWindow
    {

        #region Properties

        // Constants

        private const string MENU_ITEM = "Tools/Muffin Dev/Demos/Assets Tree View";
        private const string WINDOW_TITLE = "Assets Tree View";
        private const int WINDOW_ORDER = 50;

        [SerializeField]
        private TreeViewState m_TreeViewState = null;

        private AssetsTreeView m_AssetsTree = null;

        #endregion


        #region Initialization

        private void OnEnable()
        {
            if(m_TreeViewState == null)
            {
                m_TreeViewState = new TreeViewState();
            }
            m_AssetsTree = new AssetsTreeView(m_TreeViewState);

            m_AssetsTree.OnSelectionChanges += OnSelectFolder;
        }

        private void OnDisable()
        {
            m_AssetsTree.OnSelectionChanges -= OnSelectFolder;
        }

        #endregion


        #region UI

        private void OnGUI()
        {
            m_AssetsTree.OnGUI(new Rect(Vector2.zero, position.size));
        }

        #endregion


        #region Public Methods
        #endregion


        #region Private Methods

        /// <summary>
        /// Gets the window of this tool, or create it if it's not already open.
        /// </summary>
        [MenuItem(MENU_ITEM, false, WINDOW_ORDER)]
        private static void ShowWindow()
        {
            AssetsTreeViewDemoEditor window = EditorWindow.GetWindow<AssetsTreeViewDemoEditor>(true, WINDOW_TITLE, true) as AssetsTreeViewDemoEditor;
            window.Show();
        }

        private void OnSelectFolder(List<AssetsTreeView.FolderInfos> _SelectedFolders)
        {
            if (_SelectedFolders.Count > 0)
            {
                foreach(AssetsTreeView.FolderInfos info in _SelectedFolders)
                {
                    Debug.Log("Selected folder: " + info);
                }
            }
        }

        #endregion

    }

}