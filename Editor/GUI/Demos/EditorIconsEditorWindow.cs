using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly.Demos
{

	///<summary>
	/// Demo editor window to show all available icons, using EditorIcons utility.
	///</summary>
	public class EditorIconsEditorWindow : EditorWindow
	{

		#region Properties

		private const string MENU_ITEM = "Tools/Muffin Dev/Demos/Editor Icons List";
        private const string WINDOW_TITLE = "Editor Icons";

        private const float ICON_SIZE = 32;
        private const float HEADER_WIDTH = 140f;

        [SerializeField]
        private Vector2 m_ScrollPosition = Vector2.zero;

        private Dictionary<string, EEditorIcon[]> m_CategorizedIcons = new Dictionary<string, EEditorIcon[]>();

        #endregion


        #region Lifecycle

        private void OnEnable()
        {
            m_CategorizedIcons = new Dictionary<string, EEditorIcon[]>();
            m_CategorizedIcons.Add("Items management", new EEditorIcon[]
            {
                EEditorIcon.Add, EEditorIcon.AddDropdown, EEditorIcon.Remove
            });

            m_CategorizedIcons.Add("Windows", new EEditorIcon[]
            {
                EEditorIcon.Select,
                EEditorIcon.Focus,
                EEditorIcon.Open,
                EEditorIcon.Close,
                EEditorIcon.Maximize,
                EEditorIcon.Minimize,
                EEditorIcon.WindowOpen,
                EEditorIcon.WindowClose,
                EEditorIcon.WindowMaximize,
                EEditorIcon.WindowMinimize
            });

            m_CategorizedIcons.Add("Files", new EEditorIcon[]
            {
                EEditorIcon.File,
                EEditorIcon.Folder,
                EEditorIcon.FolderOpened,
                EEditorIcon.Import,
                EEditorIcon.Refresh
            });

            m_CategorizedIcons.Add("Statement", new EEditorIcon[]
            {
                EEditorIcon.Fold,
                EEditorIcon.Unfold,
                EEditorIcon.Lock,
                EEditorIcon.Unlock,
            });

            m_CategorizedIcons.Add("Menus & options", new EEditorIcon[]
            {
                EEditorIcon.Options,
                EEditorIcon.MoreOptions,
                EEditorIcon.Settings,
                EEditorIcon.Tools,
                EEditorIcon.Infos,
                EEditorIcon.Help,
                EEditorIcon.Search,
                EEditorIcon.VerticalMenu,
                EEditorIcon.HorizontalMenu,
                EEditorIcon.BurgerMenu
            });

            m_CategorizedIcons.Add("Directions", new EEditorIcon[]
            {
                EEditorIcon.Left,
                EEditorIcon.Right,
                EEditorIcon.Up,
                EEditorIcon.Down,
                EEditorIcon.Next,
                EEditorIcon.Previous,
                EEditorIcon.Play,
                EEditorIcon.Pause,
                EEditorIcon.Stop
            });

            m_CategorizedIcons.Add("Log", new EEditorIcon[]
            {
                EEditorIcon.Log,
                EEditorIcon.Warning,
                EEditorIcon.Error
            });

            m_CategorizedIcons.Add("Unity", new EEditorIcon[]
            {
                EEditorIcon.Unity,
                EEditorIcon.Paint,
                EEditorIcon.Pencil,
                EEditorIcon.Screen,
                EEditorIcon.View,
                EEditorIcon.Cloud,
                EEditorIcon.Favorite,
                EEditorIcon.Label,
                EEditorIcon.Checkmark,
                EEditorIcon.Shapes,
                EEditorIcon.Snapshot,
                EEditorIcon.Link,
                EEditorIcon.Unlink,
                EEditorIcon.Camera
            });
        }

        #endregion


        #region Public Methods

        [MenuItem(MENU_ITEM, false)]
        public static void ShowWindow()
        {
            EditorIconsEditorWindow window = GetWindow<EditorIconsEditorWindow>(false, WINDOW_TITLE, true);
            window.Show();
        }

        #endregion


        #region UI

        private void OnGUI()
        {
            m_ScrollPosition = EditorGUILayout.BeginScrollView(m_ScrollPosition);
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    foreach(KeyValuePair<string, EEditorIcon[]> icons in m_CategorizedIcons)
                    {
                        using (new EditorGUILayout.VerticalScope())
                        {
                            EditorGUILayout.LabelField(icons.Key, EditorStyles.boldLabel, GUILayout.Width(HEADER_WIDTH));
                            EditorGUILayout.Space();

                            foreach (EEditorIcon icon in icons.Value)
                            {
                                using (new EditorGUILayout.HorizontalScope())
                                {
                                    Rect rect = EditorGUILayout.GetControlRect(false, ICON_SIZE, GUILayout.Width(ICON_SIZE));
                                    Texture iconTexture = EditorIcons.FindIcon(icon);
                                    if (iconTexture != null)
                                    {
                                        EditorGUI.DrawTextureTransparent(rect, iconTexture, ScaleMode.ScaleToFit);
                                        GUILayout.Label(icon.ToString(), GUILayout.Height(ICON_SIZE));
                                    }
                                    else
                                    {
                                        GUILayout.Label(icon.ToString(), EditorStyles.label.FontColor(Color.red), GUILayout.Height(ICON_SIZE));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            EditorGUILayout.EndScrollView();
        }

        #endregion

    }

}