using System.IO;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

namespace MuffinDev.EditorUtils
{

	public class AssetsTreeView : TreeView
	{

        #region Enums & Subclasses

        public struct FolderInfos
        {
            public string name;
            public string path;

            public override string ToString()
            {
                return string.Format("{0} ({1})", name, path);
            }
        }

        #endregion


        #region Properties

        // Delegates

        public delegate void DSelectFolders(List<FolderInfos> _SelectedFolders);
        public event DSelectFolders OnSelectionChanges;

        // Constants

        private bool m_EnableMultiSelection = false;
        private List<FolderInfos> m_FolderInfos = new List<FolderInfos>();

        #endregion


        #region Initialisation

        public AssetsTreeView(TreeViewState _State, bool _EnableMultiSelection = false)
            : base(_State)
        {
            Reload();

            m_EnableMultiSelection = _EnableMultiSelection;
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// Called each time Reload() is called to rebuild the tree.
        /// </summary>
        /// <returns>Returns the root node.</returns>
        protected override TreeViewItem BuildRoot()
        {
            TreeViewItem root = new TreeViewItem { depth = -1 };

            int id = 0;
            DirectoryInfo dirInfos = new DirectoryInfo(Application.dataPath);
            TreeViewItem assets = new TreeViewItem { id = id, depth = 0, displayName = dirInfos.Name };
            AddFolderInfo(dirInfos);
            //Debug.Log(string.Format("Tree item: ID = {0}, Name = {1}, Path {2}", id, dirInfos.Name, dirInfos.FullName));
            id++;

            BindChildren(assets, dirInfos, ref id);

            root.AddChild(assets);
            SetupDepthsFromParentsAndChildren(root);

            return root;
        }

        /// <summary>
        /// Checks if the selected Tree item can be used in multi-selection context.
        /// </summary>
        protected override bool CanMultiSelect(TreeViewItem _Item)
        {
            return m_EnableMultiSelection;
        }

        /// <summary>
        /// Called each tme selection changes.
        /// </summary>
        protected override void SelectionChanged(IList<int> _SelectedIds)
        {
            base.SelectionChanged(_SelectedIds);
            List<FolderInfos> selectedFoldersInfos = GetFoldersInfosByID(_SelectedIds);

            if (OnSelectionChanges != null)
            {
                OnSelectionChanges.Invoke(selectedFoldersInfos);
            }
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Binds all sub-directories as child for the given parent directory.
        /// It also binds sub-directories of each found child.
        /// </summary>
        private void BindChildren(TreeViewItem _ParentDirectoryTreeNode, DirectoryInfo _ParentDirectory, ref int _ID)
        {
            // Gets sub directories
            DirectoryInfo[] childrenDirectories = _ParentDirectory.GetDirectories();

            // For each sub directory
            foreach (DirectoryInfo dir in childrenDirectories)
            {
                // If the current directory has special attributes, skip it
                if (dir.Attributes != FileAttributes.Directory) { continue; }

                // Makes TreeViewItem for the current directory
                TreeViewItem dirNode = new TreeViewItem { id = _ID, displayName = dir.Name, icon = Icon };
                //Debug.Log(string.Format("Tree item: ID = {0}, Name = {1}, Path {2}", _ID, dir.Name, dir.FullName));
                AddFolderInfo(dir);
                // Adds it as child of the parent node
                _ParentDirectoryTreeNode.AddChild(dirNode);
                _ID++;

                // Recursive call to repeat the operation for all the sub directories of the current one
                BindChildren(dirNode, dir, ref _ID);
            }
        }

        /// <summary>
        /// Adds a new value in folder infos list, ensuring the path contains only '/' and no '\'.
        /// </summary>
        /// <param name="_Info"></param>
        private void AddFolderInfo(DirectoryInfo _Info)
        {
            string path = _Info.FullName;
            path = path.Replace('\\', '/');
            m_FolderInfos.Add(new FolderInfos { name = _Info.Name, path = path });
        }

        #endregion


        #region Accessors

        /// <summary>
        /// Gets the FolderInfos instance in the list that has the same path as the given one.
        /// </summary>
        /// <returns>Returns the found FolderInfos instance index, otherwise -1.</returns>
        public int GetMatchingPathID(string _Path)
        {
            _Path = _Path.Replace('\\', '/');
            int count = m_FolderInfos.Count;
            for(int i = 0; i < count; i++)
            {
                if (m_FolderInfos[i].path == _Path)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the FolderInfos at the given id.
        /// </summary>
        public FolderInfos GetFolderInfos(int _ID)
        {
            return m_FolderInfos[_ID];
        }

        /// <summary>
        /// Gets the folder infos relative to the given ids.
        /// </summary>
        private List<FolderInfos> GetFoldersInfosByID(IList<int> _SelectedIds)
        {
            List<FolderInfos> folderInfos = new List<FolderInfos>();

            foreach(int id in _SelectedIds)
            {
                folderInfos.Add(m_FolderInfos[id]);
            }

            return folderInfos;
        }

        private Texture2D Icon
        {
            get { return (Texture2D)EditorGUIUtility.IconContent("Folder Icon").image; }
        }

        #endregion

    }

}