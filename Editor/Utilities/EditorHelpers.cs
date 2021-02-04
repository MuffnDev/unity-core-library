using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

	// Aliases
    using Object = UnityEngine.Object;

    ///<summary>
    ///	Bundle of utility methods for Editor operations.
    ///</summary>
    public static class EditorHelpers
    {

        #region Properties

        public const string ASSETS_FOLDER = "Assets";
        public const string RESOURCES_FOLDER = "Resources";
        public const string DEFAULT_ASSET_EXTENSION = "asset";

        // Default Muffin Dev' editor tools horizontal margin.
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static readonly float HORIZONTAL_MARGIN = 2f;
        // Default Muffin Dev' editor tools vertical margin.
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static readonly float VERTICAL_MARGIN = 2f;
        // Default Muffin Dev' editor tools property line height.
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static readonly float LINE_HEIGHT = EditorGUIUtility.singleLineHeight;
        // Default Muffin Dev' editor windows padding.
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public const float EDITOR_WINDOW_PADDING = 2f;
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public const float INSPECTOR_FOLDOUT_LEFT_OFFSET = 14f;

        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public const string DEFAULT_BACK_BUTTON_LABEL = "Back";
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public const float DEFAULT_BACK_BUTTON_WIDTH = 64f;

        private const string PROPERTY_ARRAY_MEMBER_PATH = "Array.data";

        private static Object s_ObjectToFocus = null;
        private static Object s_ObjectToSelect = null;

        #endregion


        #region Helpers

        /// <summary>
        /// Focus the given Object in the project view, and select it.
        /// </summary>
        public static void FocusAsset(Object _Object, bool _SelectAsset = true, bool _PingAsset = true)
        {
            if (_PingAsset)
            {
                s_ObjectToFocus = _Object;
                EditorApplication.delayCall += PingAsset;
            }

            if(_SelectAsset)
            {
                s_ObjectToSelect = _Object;
                EditorApplication.delayCall += SelectAsset;
            }
        }

        /// <summary>
        /// Focus the Project window, and highlights the stored asset.
        /// </summary>
        private static void PingAsset()
        {
            EditorUtility.FocusProjectWindow();
            EditorGUIUtility.PingObject(s_ObjectToFocus);
            s_ObjectToFocus = null;
        }

        /// <summary>
        /// Selects the stored asset.
        /// </summary>
        private static void SelectAsset()
        {
            Selection.activeObject = s_ObjectToSelect;
            s_ObjectToSelect = null;
        }

        /// <summary>
        /// Creates a folder in this project if it doesn't exist.
        /// </summary>
        /// <param name="_AssetsRelativePath">Path from this project's Assets folder.</param>
        public static void CreateProjectFolder(string _AssetsRelativePath)
        {
            string absolutePathToAssets = Application.dataPath;

            // Remove eventual "Assets" folder at the beginning of the path
            if(_AssetsRelativePath.StartsWith(ASSETS_FOLDER))
            {
                _AssetsRelativePath = _AssetsRelativePath.Substring(ASSETS_FOLDER.Length);
            }

            // Remove eventual "/" at the beginning of the path
            if(!_AssetsRelativePath.StartsWith("/"))
            {
                _AssetsRelativePath = "/" + _AssetsRelativePath;
            }

            // Remove eventual "/" at the end of the path
            if(_AssetsRelativePath.EndsWith("/"))
            {
                _AssetsRelativePath = _AssetsRelativePath.Substring(0, _AssetsRelativePath.Length - 1);
            }

            string finalAbsPath = absolutePathToAssets + _AssetsRelativePath;
            if(!Directory.Exists(finalAbsPath))
            {
                Directory.CreateDirectory(finalAbsPath);
            }
        }

        /// <summary>
        /// Gets the EditorWindow instance of the named window.
        /// </summary>
        public static EditorWindow FindEditorWindow(string _WindowTitle)
        {
            EditorWindow[] windows = Resources.FindObjectsOfTypeAll<EditorWindow>() as EditorWindow[];

            foreach (EditorWindow w in windows)
            {
                if (w.titleContent.text == _WindowTitle)
                {
                    return w;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the C# type of the given SerializedProperty.
        /// </summary>
        /// <returns>Returns the found property type, or null if the property path can't be used to get final type.</returns>
        public static Type GetPropertyType(SerializedProperty _Property)
        {
            Type parentType = _Property.serializedObject.targetObject.GetType();
            if (parentType == null)
                return null;

            FieldInfo propertyField = parentType.GetField(_Property.propertyPath, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            return propertyField != null ? propertyField.FieldType : null;
        }

        /// <summary>
        /// Checks if the given SerializedProperty is contained in an array.
        /// </summary>
        /// <param name="_Property">The property you want to check.</param>
        public static bool IsPropertyAnArrayEntry(SerializedProperty _Property)
        {
            return _Property.propertyPath.Contains(PROPERTY_ARRAY_MEMBER_PATH);
        }

        #endregion


        #region Assets

        /// <summary>
        /// Creates an asset of the given type.
        /// </summary>
        /// <param name="_AssetType">The type of the asset to create</param>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_RelativePath">The path to the asset, from "Assets" folder (excluded)</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAsset
        (
            Type _AssetType,
            string _FileName = "",
            string _RelativePath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
        {
            // Setup default values
            if(_FileName == string.Empty) { _FileName = "New" + _AssetType.Name; }
            if(_RelativePath != string.Empty) { _RelativePath = _RelativePath + "/"; }

            if (_AssetType != null)
            {
                // Define asset path
                string assetPath = ASSETS_FOLDER + "/";
                assetPath += _RelativePath;

                // Create the instance of the object to store as an asset
                ScriptableObject newAsset = ScriptableObject.CreateInstance(_AssetType);

                // If the asset has been created successfully
                if(newAsset != null)
                {
                    CreateProjectFolder(assetPath);
                    assetPath += _FileName + "." + _FileExtension;
                    // Creates the asset in project
                    AssetDatabase.CreateAsset(newAsset, assetPath);
                    AssetDatabase.SaveAssets();

                    // If we want to focus and highlight the asset in the editor after its creation
                    if(_FocusAsset)
                    {
                        // Force asset selection and highlight asset in editor
                        FocusAsset(newAsset);
                    }

                    string absolutePath = Application.dataPath + assetPath.Substring(ASSETS_FOLDER.Length);
                    return new AssetCreationResult(absolutePath, newAsset);
                }

                // If the new asset has not been successfully created
                else
                {
                    Debug.LogWarning(string.Format("Impossible to create an asset of type \"{0}\".", _AssetType));
                }
            }

            else
            {
                Debug.LogWarning("The given asset type is not valid.");
            }

            return AssetCreationResult.FAILED_ASSET_CREATION;
        }

        /// <summary>
        /// Creates an asset of the given type.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the asset to create</typeparam>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_RelativePath">The path to the asset, from "Assets" folder (excluded)</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAsset<TAssetType>
        (
            string _FileName = "",
            string _RelativePath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
        {
            return CreateAsset
            (
                typeof(TAssetType),
                _FileName,
                _RelativePath,
                _FileExtension,
                _FocusAsset
            );
        }

        /// <summary>
        /// Creates an asset of the given type, using a Save File Panel to define the path to that new asset.
        /// </summary>
        /// <param name="_AssetType">The type of the asset to create</param>
        /// <param name="_PanelTitle">Title of the panel window</param>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_DefaultPath">Path to focus when the panel will open</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAssetPanel
        (
            Type _AssetType,
            string _PanelTitle = "Save new asset",
            string _FileName = "",
            string _DefaultPath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
        {
            if (_FileName == string.Empty) { _FileName = "New" + _AssetType.Name; }

            // Open a SaveFilePanel with the given settings.
            // We'll get an absolute path that will be the selected path to the file to create.
            string absolutePath = EditorUtility.SaveFilePanel(_PanelTitle, _DefaultPath, _FileName, _FileExtension);

            // If the user close the Save File Panel, the path will be Empty.
            // Then, we can stop this method.
            if (absolutePath == string.Empty)
                return AssetCreationResult.FAILED_ASSET_CREATION;

            // If the selected path is in the current project's folder.
            if (absolutePath.StartsWith(Application.dataPath))
            {
                // The path becomes relative, from the excluded Assets folder.
                string relativePath = absolutePath.Substring(Application.dataPath.Length - ASSETS_FOLDER.Length);

                // Create the asset of the given type, then save it.
                ScriptableObject newAsset = ScriptableObject.CreateInstance(_AssetType);

                AssetDatabase.CreateAsset(newAsset, relativePath);
                AssetDatabase.SaveAssets();

                // If we want to focus the created asset after its creation :
                // Set it as the active selection, focus the Project window and highlight (ping) it.
                if (_FocusAsset)
                {
                    Selection.activeObject = newAsset;
                    EditorUtility.FocusProjectWindow();
                    EditorGUIUtility.PingObject(newAsset);
                }

                return new AssetCreationResult(absolutePath, newAsset);
            }

            else
            {
                Debug.LogWarning("You must select a folder relative to the \"Assets\" folder of this project.");
                return AssetCreationResult.FAILED_ASSET_CREATION;
            }
        }

        /// <summary>
        /// Creates an asset of the given type, using a Save File Panel to define the path to that new asset.
        /// </summary>
        /// <param name="_AssetType">The type of the asset to create</param>
        /// <param name="_CreatedAsset">The created asset</param>
        /// <param name="_PanelTitle">Title of the panel window</param>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_DefaultPath">Path to focus when the panel will open</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAssetPanel
        (
            Type _AssetType,
            out Object _CreatedAsset,
            string _PanelTitle = "Save new asset",
            string _FileName = "",
            string _DefaultPath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
        {
            AssetCreationResult result = CreateAssetPanel
            (
                _AssetType,
                _PanelTitle,
                _FileName,
                _DefaultPath,
                _FileExtension,
                _FocusAsset
            );
            _CreatedAsset = result.AssetObject;
            return result;
        }

        /// <summary>
        /// Creates an asset of the specified type, using a Save File Panel to define the path to that new asset.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the Scriptable Object to create.</param>
        /// <param name="_PanelTitle">Title of the panel window</param>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_DefaultPath">Path to focus when the panel will open</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAssetPanel<TAssetType>
        (
            string _PanelTitle,
            string _FileName = "",
            string _DefaultPath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
            where TAssetType : Object
        {
            return CreateAssetPanel
            (
                typeof(TAssetType),
                _PanelTitle,
                _FileName,
                _DefaultPath,
                _FileExtension,
                _FocusAsset
            );
        }

        /// <summary>
        /// Creates an asset of the specified type, using a Save File Panel to define the path to that new asset.
        /// </summary>
        /// <typeparam name="TAssetType">The type of the Scriptable Object to create.</param>
        /// <param name="_PanelTitle">Title of the panel window</param>
        /// <param name="_CreatedAsset">The created asset</param>
        /// <param name="_FileName">The asset file's name. If empty, the asset name will be New[_AssetType]</param>
        /// <param name="_DefaultPath">Path to focus when the panel will open</param>
        /// <param name="_FileExtension">The asset file's extension</param>
        /// <param name="_FocusAsset">Should the asset be selected and highlighted after its creation ?</param>
        public static AssetCreationResult CreateAssetPanel<TAssetType>
        (
            out TAssetType _CreatedAsset,
            string _PanelTitle,
            string _FileName = "",
            string _DefaultPath = "",
            string _FileExtension = DEFAULT_ASSET_EXTENSION,
            bool _FocusAsset = true
        )
            where TAssetType : Object
        {
            AssetCreationResult result = CreateAssetPanel
            (
                typeof(TAssetType),
                _PanelTitle,
                _FileName,
                _DefaultPath,
                _FileExtension,
                _FocusAsset
            );
            _CreatedAsset = result.AssetObject ? result.AssetObject as TAssetType : null;
            return result;
        }

        /// <summary>
        /// Finds all assets of the given type in the project.
        /// </summary>
        /// <typeparam name="T">The type of the asset you want to find.</typeparam>
        /// <returns>Returns the cast assets you want to find.</returns>
        public static T[] FindAllAssetsOfType<T>()
            where T : Object
        {
            Object[] assets = FindAllAssetsOfType(typeof(T));
            T[] castAssets = new T[assets.Length];
            for (int i = 0; i < assets.Length; i++)
                castAssets[i] = assets[i] as T;
            return castAssets;
        }

        /// <summary>
        /// Finds all assets of the given type in the project.
        /// </summary>
        /// <param name="_Type">The type of the asset you want to find.</param>
        /// <returns>Returns the cast assets you want to find.</returns>
        public static Object[] FindAllAssetsOfType(Type _Type)
        {
            string[] guids = AssetDatabase.FindAssets($"t:{_Type.Name}");
            Object[] assets = new Object[guids.Length];
            for (int i = 0; i < guids.Length; i++)
                assets[i] = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), _Type);
            return assets;
        }

        #endregion


        #region Paths

        /// <summary>
        /// Gets the absolute path to an asset.
        /// </summary>
        /// <param name="_Asset">The asset of which you want to get the absolute path.</param>
        /// <returns>Returns the absolute path, or string.Empty if the asset doesn't exist.</returns>
        public static string GetAssetAbsolutePath(Object _Asset)
        {
            return GetAssetAbsolutePath(_Asset.GetInstanceID());
        }

        /// <summary>
        /// Gets the absolute path to an asset.
        /// </summary>
        /// <param name="_InstanceID">The InstanceID of the asset you want the absolute path.</param>
        /// <returns>Returns the absolute path, or string.Empty if the asset doesn't exist.</returns>
        public static string GetAssetAbsolutePath(int _InstanceID)
        {
            string relPath = AssetDatabase.GetAssetPath(_InstanceID);
            return GetAssetAbsolutePath(relPath);
        }

        /// <summary>
        /// Gets the absolute path, from a given relative path to the current Unity project.
        /// </summary>
        /// <param name="_RelativePath">The relative path (from /Assets directory) of the asset you want the absolute path.</param>
        /// <returns>Returns the absolute path, or string.Empty if the asset doesn't exist.</returns>
        public static string GetAssetAbsolutePath(string _RelativePath)
        {
            if (_RelativePath == string.Empty)
            {
                return string.Empty;
            }

            string absPath = Application.dataPath;
            absPath = absPath.Substring(0, absPath.Length - ASSETS_FOLDER.Length);

            return absPath + _RelativePath;
        }

        /// <summary>
        /// Checks if the gievn absolute path points to this project's folder.
        /// </summary>
        public static bool IsPathRelativeToCurrentProjectFolder(string _AbsolutePath)
        {
            return !string.IsNullOrEmpty(_AbsolutePath) && _AbsolutePath.StartsWith(Application.dataPath);
        }

        /// <summary>
        /// Gets a path relative to this project's Assets folder from a given absolute path.
        /// NOTE: This method will write a log if the given path is not valid.
        /// </summary>
        /// <param name="_IncludeAssetsFolder">If true, the relative path will start with "Assets". If false, the Assets folder will be
        /// ignorded.</param>
        /// <returns>Returns the relative path if the given absolute path points to this project's folder, otherwise returns
        /// string.Empty</returns>
        public static string GetPathRelativeToCurrentProjectFolder(string _AbsolutePath, bool _IncludeAssetsFolder = true)
        {
            // Check if the given path is in the current project's folder.
            if (_AbsolutePath.StartsWith(Application.dataPath))
            {
                int substringStartIndex = Application.dataPath.Length;

                if (_IncludeAssetsFolder)
                {
                    substringStartIndex -= ASSETS_FOLDER.Length;
                }

                return _AbsolutePath.Substring(substringStartIndex);
            }

            else
            {
                Debug.LogWarning("The given path is not pointing on this current project's \"Assets\" folder (" + _AbsolutePath + ").");
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the path to the asset pointed by the given absolute path, from Resources folder of this project.
		/// </summary>
		/// <returns>Returns the asset path from Resources folder. But if the given absolute path dosen't point to the current project's
        /// Assets folder, or if there's no Resources folder in this project, returns string.Empty.</returns>
		public static string GetResourcesPath(string _AbsolutePath, string _ExpectedExtension, bool _IncludeResourcesFolder = false)
        {
            // Gets a relative path from Assets folder.
            // This allow us to know if the given absolute path points to this project's Assets folder.
            string relPath = GetPathRelativeToCurrentProjectFolder(_AbsolutePath, false);
            string resourcesPath = string.Empty;

            // If the given absolute path points to this project's Assets folder
            if (relPath != string.Empty)
            {
                // Splits the path and init loop
                string[] splitPath = relPath.Split('/');
                int count = splitPath.Length;
                bool resourcesFound = false;

                // For each part of the path
                for (int i = 0; i < count; i++)
                {
                    // If the Resources folder hasn't been found
                    if (!resourcesFound)
                    {
                        // If the current part is the Resources folder
                        if (splitPath[i] == RESOURCES_FOLDER)
                        {
                            // If we want to include "Resources" at the beginning of the output path
                            if (_IncludeResourcesFolder)
                            {
                                // Add "Resources" to the output path
                                resourcesPath += RESOURCES_FOLDER;
                            }
                            resourcesFound = true;
                        }
                    }
                    // Else, if the Resources folder has been found
                    else
                    {
                        // Add the current path part to the output path
                        resourcesPath += (resourcesPath == string.Empty) ? splitPath[i] : "/" + splitPath[i];
                    }
                }

                if (resourcesFound)
                {
                    // Removes the asset extension of the output path (when using Resources.Load(), Unity doesn't expect the asset extension)
                    if (_ExpectedExtension != string.Empty)
                    {
                        return resourcesPath.Substring(0, resourcesPath.Length - (_ExpectedExtension.Length + 1));
                    }
                }

                // Else, if the Resources folder hasn't been found (the output path is still empty)
                else
                {
                    Debug.LogWarning("The given path doesn't point to a Resources folder.");
                }
            }

            return resourcesPath;
        }

        #endregion


        #region GUI

        /// <summary>
        /// Makes a text field for entering floats, but allows you to define the "drag hot zone" of the control.
        /// </summary>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static float FloatField(Rect _Position, Rect _DragHotZone, float _Value)
        {
            return MuffinDevGUI.FloatField(_Position, _DragHotZone, _Value);
        }

        /// <summary>
        /// Draws an horizontal line.
        /// </summary>
        /// <param name="_Rect">The position and size of the line.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void HorizontalLine(Rect _Rect)
        {
            MuffinDevGUI.HorizontalLine(_Rect);
        }

        /// <summary>
        /// Draws an horizontal line.
        /// </summary>
        /// <param name="_Rect">The position and size of the line.</param>
        /// <param name="_Color">The color of the line.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void HorizontalLine(Rect _Rect, Color _Color)
        {
            MuffinDevGUI.HorizontalLine(_Rect, _Color);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void HorizontalLineLayout(bool _Wide = false)
        {
            MuffinDevGUI.HorizontalLine(_Wide);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Height">The height of the line.</param>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void HorizontalLineLayout(float _Height, bool _Wide = false)
        {
            MuffinDevGUI.HorizontalLine(_Height, _Wide);
        }

        /// <summary>
        /// Draws an horizontal line using layout GUI.
        /// </summary>
        /// <param name="_Height">The height of the line.</param>
        /// <param name="_Color">The color of the line.</param>
        /// <param name="_Wide">If true, the line will have the exact same with as the window where it's drawn.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void HorizontalLineLayout(float _Height, Color _Color, bool _Wide = false)
        {
            MuffinDevGUI.HorizontalLine(_Height, _Color, _Wide);
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string SearchBar(string _Text, Action _OnCancel = null)
        {
            return MuffinDevGUI.SearchBar(_Text, _OnCancel);
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Label">The label of the search bar field.</param>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string SearchBar(string _Label, string _Text, Action _OnCancel = null)
        {
            return MuffinDevGUI.SearchBar(_Label, _Text, _OnCancel);
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <param name="_SearchBarStyle">The GUIStyle to apply on the search bar field.</param>
        /// <param name="_CancelButtonStyle">The GUIStyle to apply on the cancel button.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string SearchBar(string _Text, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null)
        {
            return MuffinDevGUI.SearchBar(_Text, _SearchBarStyle, _CancelButtonStyle, _OnCancel);
        }

        /// <summary>
        /// Draws a search bar field using GUI Layout, with a "cancel" button on the right of the field.
        /// </summary>
        /// <param name="_Label">The label of the search bar field.</param>
        /// <param name="_Text">The content of the search bar.</param>
        /// <param name="_OnCancel">The action to call aftern clicking on the "cancel" button. If null, the button won't appear.</param>
        /// <param name="_SearchBarStyle">The GUIStyle to apply on the search bar field.</param>
        /// <param name="_CancelButtonStyle">The GUIStyle to apply on the cancel button.</param>
        /// <returns>Returns the changed content of the search bar.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string SearchBar(string _Label, string _Text, GUIStyle _SearchBarStyle, GUIStyle _CancelButtonStyle, Action _OnCancel = null)
        {
            return MuffinDevGUI.SearchBar(_Label, _Label, _SearchBarStyle, _CancelButtonStyle, _OnCancel);
        }

        /// <summary>
        /// Draws a "On/Off" switch field.
        /// </summary>
        /// <param name="_Value">The current property value.</param>
        /// <returns>Returns true if "On" is selected, otherwise false.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static bool SwitchField(bool _Value)
        {
            return MuffinDevGUI.SwitchField(_Value);
        }

        /// <summary>
        /// Draws a "On/Off" switch field.
        /// </summary>
        /// <param name="_Label">The label of the property.</param>
        /// <param name="_Value">The current property value.</param>
        /// <returns>Returns true if "On" is selected, otherwise false.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static bool SwitchField(string _Label, bool _Value)
        {
            return MuffinDevGUI.SwitchField(_Label, _Value);
        }

        /// <summary>
        /// Draws an Object field with a "Create new" button on the right.
        /// </summary>
        /// <typeparam name="TObjectType">The type of the object that can be passed to the Object field.</typeparam>
        /// <param name="_Position">The position and size of the field.</param>
        /// <param name="_Label">The label to display on the left of the field.</param>
        /// <param name="_Property">The property on which you want to set the field value.</param>
        /// <param name="_PanelTitle">The title of the SavePanel utility.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to pass scene object in the object field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ObjectField<TObjectType>(Rect _Position, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true)
            where TObjectType : Object
        {
            MuffinDevGUI.ObjectField<TObjectType>(_Position, _Label, _Property, _PanelTitle, _AllowSceneObjects);
        }

        /// <summary>
        /// Draws an Object field with a "Create new" button on the right.
        /// </summary>
        /// <param name="_Position">The position and size of the field.</param>
        /// <param name="_ObjectType">The type of the object that can be passed to the Object field.</param>
        /// <param name="_Label">The label to display on the left of the field.</param>
        /// <param name="_Property">The property on which you want to set the field value.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to pass scene object in the object field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ObjectField(Rect _Position, Type _ObjectType, GUIContent _Label, SerializedProperty _Property, string _PanelTitle = null, bool _AllowSceneObjects = true)
        {
            MuffinDevGUI.ObjectField(_Position, _ObjectType, _Label, _Property, _PanelTitle, _AllowSceneObjects);
        }

        /// <summary>
        /// Draws a "Back" button.
        /// </summary>
        /// <param name="_Label">The content of the button.</param>
        /// <param name="_Tooltip">The tooltip of the button.</param>
        /// <param name="_Width">The optional width of the button.</param>
        /// <param name="_ButtonStyle">The GUIStyle of the button.</param>
        /// <returns>Returns true if the button has been clicked this frame, otherwise false.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static bool BackButton(string _Label = null, string _Tooltip = null, float _Width = MuffinDevGUI.DEFAULT_BACK_BUTTON_WIDTH, GUIStyle _ButtonStyle = null)
        {
            return MuffinDevGUI.BackButton(_Label, _Tooltip, _Width, _ButtonStyle);
        }

        /// <summary>
        /// Draws a "Back" button.
        /// </summary>
        /// <param name="_Rect">The Rect container of the button.</param>
        /// <param name="_Label">The content of the button.</param>
        /// <param name="_Tooltip">The tooltip of the button.</param>
        /// <param name="_ButtonStyle">The GUIStyle of the button.</param>
        /// <param name="_Width">The optional width of the button.</param>
        /// <returns>Returns true if the button has been clicked this frame, otherwise false.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static bool BackButton(Rect _Rect, string _Label = null, string _Tooltip = null, GUIStyle _ButtonStyle = null, float _Width = MuffinDevGUI.DEFAULT_BACK_BUTTON_WIDTH)
        {
            return MuffinDevGUI.BackButton(_Rect, _Label, _Tooltip, _ButtonStyle, _Width);
        }

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Asset">The asset of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void DrawDefaultInspector(Object _Asset, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            MuffinDevGUI.DrawDefaultInspector(_Asset, _IncludeScriptProperty, _CustomLabelWidth);
        }

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Object">The SerializedObject of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void DrawDefaultInspector(SerializedObject _Object, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            MuffinDevGUI.DrawDefaultInspector(_Object, _IncludeScriptProperty, _CustomLabelWidth);
        }

        /// <summary>
        /// Draws the default inspector of the given object.
        /// </summary>
        /// <param name="_Property">The SerializedProperty of which you want to draw the inspector.</param>
        /// <param name="_IncludeScriptProperty">If enabled, skip the first "Script" property of the asset.</param>
        /// <param name="_CustomLabelWidth">If more than 0 given, set the Editor's label width for all the object properties.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void DrawDefaultInspector(SerializedProperty _Property, bool _IncludeScriptProperty = false, float _CustomLabelWidth = -1f)
        {
            MuffinDevGUI.DrawDefaultInspector(_Property, _IncludeScriptProperty, _CustomLabelWidth);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_NbElements">The total number of elements in your paginated list.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Pagination PaginationBar(int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return MuffinDevGUI.PaginationBar(_NbElements, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Pagination PaginationBar<T>(IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return MuffinDevGUI.PaginationBar(_List, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_Rect">The position and size of the element to draw.</param>
        /// <param name="_List">The list that is paginated.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Pagination PaginationBar<T>(Rect _Rect, IList<T> _List, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return MuffinDevGUI.PaginationBar(_Rect, _List, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a pagination bar, with "Previous" and "Next" buttons, and an int field to set the page number.
        /// </summary>
        /// <param name="_Rect">The position and size of the element to draw.</param>
        /// <param name="_NbElements">The total number of elements in your paginated list.</param>
        /// <param name="_Page">The current page.</param>
        /// <param name="_NbElementsPerPage">The number of elements to display per page.</param>
        /// <returns>Returns the pagination data.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Pagination PaginationBar(Rect _Rect, int _NbElements, int _Page, int _NbElementsPerPage = Pagination.DEFAULT_NB_ELEMENTS_PER_PAGE)
        {
            return MuffinDevGUI.PaginationBar(_Rect, _NbElements, _Page, _NbElementsPerPage);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void FolderPathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            MuffinDevGUI.FolderPathField(_Property, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FolderPathField(string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FolderPathField(_Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FolderPathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FolderPathField(_Label, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void FolderPathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            MuffinDevGUI.FolderPathField(_Rect, _Property, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FolderPathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FolderPathField(_Rect, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a directory.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a directory inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FolderPathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FolderPathField(_Rect, _Label, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void FilePathField(SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            MuffinDevGUI.FilePathField(_Property, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FilePathField(string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FilePathField(_Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        /// <returns>Returns the selected path.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FilePathField(string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FilePathField(_Label, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The string property to set.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void FilePathField(Rect _Rect, SerializedProperty _Property, bool _Editable = false, bool _InProject = true)
        {
            MuffinDevGUI.FilePathField(_Rect, _Property, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FilePathField(Rect _Rect, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FilePathField(_Rect, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws a text field with a "Browse..." button which allow the user to select a file.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field.</param>
        /// <param name="_Path">The current path value.</param>
        /// <param name="_Editable">If true, the text field of the path can be edited manually. Otherwise, the path can only be set using
        /// the "Browse..." button.</param>
        /// <param name="_InProject">If true, forces the path to target a file inside the current project.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static string FilePathField(Rect _Rect, string _Label, string _Path, bool _Editable = false, bool _InProject = true)
        {
            return MuffinDevGUI.FilePathField(_Rect, _Label, _Path, _Editable, _InProject);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static T ExtendedObjectField<T>(T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            return MuffinDevGUI.ExtendedObjectField(_Object, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static T ExtendedObjectField<T>(string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            return MuffinDevGUI.ExtendedObjectField(_Label, _Object, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws the input field for an Extended Object Field, and the additional controls before and after that field.
        /// </summary>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        /// <returns>Returns the selected object.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Object ExtendedObjectField(Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            return MuffinDevGUI.ExtendedObjectField(_Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws the input field for an Extended Object Field, and the additional controls before and after that field.
        /// </summary>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        /// <returns>Returns the selected object.</returns>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Object ExtendedObjectField(string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            return MuffinDevGUI.ExtendedObjectField(_Label, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ExtendedObjectField(SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            MuffinDevGUI.ExtendedObjectField(_Property, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ExtendedObjectField(SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            MuffinDevGUI.ExtendedObjectField(_Property, _Label, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static T ExtendedObjectField<T>(Rect _Rect, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            return MuffinDevGUI.ExtendedObjectField(_Rect, _Object, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <typeparam name="T">The type of object that can be selected.</typeparam>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static T ExtendedObjectField<T>(Rect _Rect, string _Label, T _Object, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
            where T : Object
        {
            return MuffinDevGUI.ExtendedObjectField(_Rect, _Label, _Object, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Object ExtendedObjectField(Rect _Rect, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            return MuffinDevGUI.ExtendedObjectField(_Rect, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Object">The currently selected object.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static Object ExtendedObjectField(Rect _Rect, string _Label, Object _Object, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            return MuffinDevGUI.ExtendedObjectField(_Rect, _Label, _Object, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            MuffinDevGUI.ExtendedObjectField(_Rect, _Property, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        /// <summary>
        /// Draws an Object Field with additional controls.
        /// </summary>
        /// <param name="_Rect">The position and size of the field to draw.</param>
        /// <param name="_Label">The label of the field to draw.</param>
        /// <param name="_Property">The serialized property to use and assign that contains the selected object data.</param>
        /// <param name="_ObjectType">The type of object that can be selected.</param>
        /// <param name="_AllowSceneObjects">If true, allow user to select scene objects. Otherwise, only assets can be selected.</param>
        /// <param name="_Buttons">The list of all controls you want to add to this field.</param>
        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static void ExtendedObjectField(Rect _Rect, SerializedProperty _Property, string _Label, Type _ObjectType, bool _AllowSceneObjects, ExtendedObjectFieldButton[] _Buttons)
        {
            MuffinDevGUI.ExtendedObjectField(_Rect, _Property, _Label, _ObjectType, _AllowSceneObjects, _Buttons);
        }

        #endregion


        #region Editor Styles

        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static GUIStyle HelpBoxStyle
        {
            get { return MuffinDevGUI.HelpBoxStyle; }
        }

        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static GUIStyle ReorderableListHeaderStyle
        {
            get { return MuffinDevGUI.ReorderableListHeaderStyle; }
        }

        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static GUIStyle ReorderableListBoxStyle
        {
            get { return MuffinDevGUI.ReorderableListBoxStyle; }
        }

        [Obsolete("GUI drawers from EditorHelpers are deprecated. Please use MuffinDevGUI instead")]
        public static GUIStyle PropertyFieldButtonStyle
        {
            get { return MuffinDevGUI.PropertyFieldButtonStyle; }
        }

        #endregion

    }

}