using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

    /// <summary>
    /// Defines a common editor icon.
    /// </summary>
    public enum EEditorIcon
    {
        // Items
        Add,
        AddDropdown,
        Remove,

        // Windows
        Select,
        Focus,
        Open,
        WindowOpen,
        Close,
        WindowClose,
        Minimize,
        WindowMaximize,
        Maximize,
        WindowMinimize,

        // Files
        File,
        Folder,
        FolderOpened,
        Import,
        Refresh,

        // Statement
        Fold,
        Unfold,
        Lock,
        Unlock,
        
        // Menus & options
        Options,
        MoreOptions,
        Settings,
        Tools,
        Infos,
        Help,
        Search,
        VerticalMenu,
        HorizontalMenu,
        BurgerMenu,

        // Directions
        Left,
        Right,
        Up,
        Down,
        Previous,
        Next,
        Play,
        Pause,
        Stop,

        // Log
        Log,
        Warning,
        Error,

        // Others
        Unity,
        Paint,
        Pencil,
        Screen,
        View,
        Cloud,
        Favorite,
        Label,
        Checkmark,
        Shapes,
        Snapshot,
        Link,
        Unlink,
        Camera,
    }

    ///<summary>
    /// Utility class to get and draw editor icons.
    ///</summary>
    public static class EditorIcons
	{

        private static readonly Texture[] LOADING_SPINNER_ICONS = null;
        private static readonly Dictionary<EEditorIcon, Texture> ICONS = new Dictionary<EEditorIcon, Texture>();

        /// <summary>
        /// Initializes static values.
        /// </summary>
        static EditorIcons()
        {
            ICONS = new Dictionary<EEditorIcon, Texture>();
            // Items management
            ICONS.Add(EEditorIcon.Add, FindIcon("Toolbar Plus@2x"));
            ICONS.Add(EEditorIcon.AddDropdown, FindIcon("Toolbar Plus More@2x"));
            ICONS.Add(EEditorIcon.Remove, FindIcon("Toolbar Minus@2x"));

            // Window management
            ICONS.Add(EEditorIcon.Select, FindIcon("ViewToolZoom@2x"));
            ICONS.Add(EEditorIcon.Focus, FindIcon("Search Icon"));
            ICONS.Add(EEditorIcon.Open, FindIcon("winbtn_win_restore@2x"));
            ICONS.Add(EEditorIcon.Close, FindIcon("winbtn_win_close@2x"));
            ICONS.Add(EEditorIcon.Maximize, FindIcon("winbtn_win_max@2x"));
            ICONS.Add(EEditorIcon.Minimize, FindIcon("winbtn_win_min"));
#if UNITY_EDITOR_OSX
            ICONS.Add(EEditorIcon.WindowOpen, GetIcon("winbtn_win_restore@2x"));
            ICONS.Add(EEditorIcon.WindowClose, GetIcon("winbtn_mac_close_h@2x"));
            ICONS.Add(EEditorIcon.WindowMaximize, GetIcon("winbtn_mac_max_h@2x"));
            ICONS.Add(EEditorIcon.WindowMinimize, GetIcon("winbtn_mac_min_h@2x"));
#else
            ICONS.Add(EEditorIcon.WindowOpen, FindIcon("winbtn_win_restore@2x"));
            ICONS.Add(EEditorIcon.WindowClose, FindIcon("winbtn_win_close@2x"));
            ICONS.Add(EEditorIcon.WindowMaximize, FindIcon("winbtn_win_max@2x"));
            ICONS.Add(EEditorIcon.WindowMinimize, FindIcon("winbtn_win_min"));
#endif

            // Files
            ICONS.Add(EEditorIcon.File, FindIcon("TextAsset Icon"));
            ICONS.Add(EEditorIcon.Folder, FindIcon("Folder Icon"));
            ICONS.Add(EEditorIcon.FolderOpened, FindIcon("OpenedFolder Icon"));
            ICONS.Add(EEditorIcon.Import, FindIcon("Import@2x"));
            ICONS.Add(EEditorIcon.Refresh, FindIcon("Refresh@2x"));

            // Statement
            ICONS.Add(EEditorIcon.Fold, FindIcon("IN foldout act on@2x"));
            ICONS.Add(EEditorIcon.Unfold, FindIcon("IN foldout act@2x"));
            ICONS.Add(EEditorIcon.Lock, FindIcon("IN LockButton on@2x"));
            ICONS.Add(EEditorIcon.Unlock, FindIcon("IN LockButton@2x"));

            // Menus & options
            ICONS.Add(EEditorIcon.Options, FindIcon("_Popup@2x"));
            ICONS.Add(EEditorIcon.MoreOptions, FindIcon("MoreOptions@2x"));
            ICONS.Add(EEditorIcon.Settings, FindIcon("Settings@2x"));
            ICONS.Add(EEditorIcon.Tools, FindIcon("CustomTool@2x"));
            ICONS.Add(EEditorIcon.Infos, FindIcon("UnityEditor.InspectorWindow@2x"));
            ICONS.Add(EEditorIcon.Help, FindIcon("_Help@2x"));
            ICONS.Add(EEditorIcon.Search, FindIcon("Search Icon"));
            ICONS.Add(EEditorIcon.VerticalMenu, FindIcon("_Menu@2x"));
            //ICONS.Add(EEditorIcon.HorizontalMenu, GetIcon("_Menu"));
            //ICONS.Add(EEditorIcon.BurgerMenu, GetIcon("_Menu"));

            // Directions
            ICONS.Add(EEditorIcon.Left, FindIcon("back@2x"));
            ICONS.Add(EEditorIcon.Right, FindIcon("forward@2x"));
            //ICONS.Add(EEditorIcon.Up, GetIcon("back@2x"));
            ICONS.Add(EEditorIcon.Down, FindIcon("icon dropdown@2x"));
            ICONS.Add(EEditorIcon.Next, FindIcon("tab_next@2x"));
            ICONS.Add(EEditorIcon.Previous, FindIcon("tab_prev@2x"));
            ICONS.Add(EEditorIcon.Play, FindIcon("PlayButton@2x"));
            ICONS.Add(EEditorIcon.Pause, FindIcon("PauseButton@2x"));
            ICONS.Add(EEditorIcon.Stop, FindIcon("PreMatQuad@2x"));

            // Logs
            ICONS.Add(EEditorIcon.Log, FindIcon("console.infoicon@2x"));
            ICONS.Add(EEditorIcon.Warning, FindIcon("console.warnicon@2x"));
            ICONS.Add(EEditorIcon.Error, FindIcon("console.erroricon@2x"));

            // Others
            ICONS.Add(EEditorIcon.Unity, FindIcon("UnityLogo"));
            //ICONS.Add(EEditorIcon.Paint, GetIcon("Grid.PaintTool@2x"));
            //ICONS.Add(EEditorIcon.Pencil, GetIcon("In-Development@2x"));
            ICONS.Add(EEditorIcon.Screen, FindIcon("BuildSettings.Standalone@2x"));
            ICONS.Add(EEditorIcon.View, FindIcon("ClothInspector.ViewValue"));
            ICONS.Add(EEditorIcon.Cloud, FindIcon("CloudConnect@2x"));
            ICONS.Add(EEditorIcon.Favorite, FindIcon("Favorite@2x"));
            ICONS.Add(EEditorIcon.Label, FindIcon("FilterByLabel@2x"));
            ICONS.Add(EEditorIcon.Checkmark, FindIcon("FilterSelectedOnly@2x"));
            ICONS.Add(EEditorIcon.Shapes, FindIcon("FilterByType@2x"));
            //ICONS.Add(EEditorIcon.Snapshot, GetIcon("FrameCapture@2x"));
            //ICONS.Add(EEditorIcon.Link, GetIcon("Linked@2x"));
            //ICONS.Add(EEditorIcon.Unlink, GetIcon("UnLinked@2x"));
            ICONS.Add(EEditorIcon.Camera, FindIcon("SceneViewCamera@2x"));

             // Loading spinner
             LOADING_SPINNER_ICONS = new Texture[12];
            for(int i = 0; i < LOADING_SPINNER_ICONS.Length; i++)
                LOADING_SPINNER_ICONS[i] = FindIcon($"WaitSpin{i.AddLeading0(2)}");
        }

        /// <summary>
        /// Gets an editor icon by name (from built-in resources and /Editor Default Resources/Icons directory).
        /// </summary>
        public static Texture FindIcon(string _IconName)
        {
            try
            {
                GUIContent content = EditorGUIUtility.IconContent(_IconName);
                if (content != null && content.image != null)
                    return content.image;
            }
            catch(Exception) { }

            return null;
        }

        /// <summary>
        /// Gets an editor icon by type.
        /// </summary>
        public static Texture FindIcon(EEditorIcon _IconType)
        {
            if (ICONS.TryGetValue(_IconType, out Texture icon))
                return icon;
            return null;
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the named icon.
        /// List of all built'in icons: https://unitylist.com/p/5c3/Unity-editor-icons
        /// </summary>
        /// <param name="_IconName">The name of the icon (from built-in resources).</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(string _IconName, string _Tooltip)
        {
            return IconContent(FindIcon(_IconName), null, _Tooltip);
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the named icon.
        /// List of all built'in icons: https://unitylist.com/p/5c3/Unity-editor-icons
        /// </summary>
        /// <param name="_IconName">The name of the icon (from built-in resources).</param>
        /// <param name="_Text">The optional text of the content.</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(string _IconName, string _Text, string _Tooltip)
        {
            return IconContent(FindIcon(_IconName), _Text, _Tooltip);
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the given icon.
        /// </summary>
        /// <param name="_IconType">The type of the icon.</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(EEditorIcon _IconType, string _Tooltip)
        {
            return IconContent(FindIcon(_IconType), null, _Tooltip);
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the given icon.
        /// </summary>
        /// <param name="_IconType">The type of the icon.</param>
        /// <param name="_Text">The optional text of the given content.</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(EEditorIcon _IconType, string _Text, string _Tooltip)
        {
            return IconContent(FindIcon(_IconType), _Text, _Tooltip);
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the given icon.
        /// </summary>
        /// <param name="_Icon">The icon to use for this content.</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(Texture _Icon, string _Tooltip)
        {
            return IconContent(_Icon, null, _Tooltip);
        }

        /// <summary>
        /// Creates a GUIContent instance that contains the given icon.
        /// </summary>
        /// <param name="_Icon">The icon to use for this content.</param>
        /// <param name="_Text">The optional text of the content.</param>
        /// <param name="_Tooltip">The optional hovering tooltip of the content.</param>
        /// <returns>Returns the created GUIContent instance.</returns>
        public static GUIContent IconContent(Texture _Icon, string _Text, string _Tooltip)
        {
            return new GUIContent(_Text, _Icon, _Tooltip);
        }

        /// <summary>
        /// Gets an array containing all the icons to animate a loading spinner.
        /// </summary>
        public static Texture[] LoadingSpinnerIcons
        {
            get { return LOADING_SPINNER_ICONS; }
        }

    }

}