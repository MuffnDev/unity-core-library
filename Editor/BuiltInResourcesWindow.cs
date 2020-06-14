﻿using System;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace MuffinDev.EditorUtils
{

    /// <summary>
    /// Displays all built-in styles and icons.
    /// Original script from http://wiki.unity3d.com/index.php/Show_Built_In_Resources
    /// </summary>
    public class BuiltInResourcesViewer : EditorWindow
    {

        #region Subcasses

        private struct Drawing
        {
            public Rect Rect;
            public Action Draw;
        }

        #endregion


        #region Properties

        // Constants

        private const string MENU_ITEM = "Tools/Muffin Dev/Built-In Resources Viewer";
        private const string WINDOW_TITLE = "Built-In Resources Viewer";

        // Other properties

        private List<Drawing> m_Drawings;

        private List<UnityEngine.Object> m_Objects;
        private float m_ScrollPos;
        private float m_MaxY;
        private Rect m_OldPosition;

        private bool m_ShowingStyles = true;
        private bool m_ShowingIcons = false;

        private string m_Search = "";

        #endregion


        #region Public Methods

        [MenuItem(MENU_ITEM, false)]
        public static void ShowWindow()
        {
            BuiltInResourcesViewer window = GetWindow<BuiltInResourcesViewer>(false, WINDOW_TITLE, true) as BuiltInResourcesViewer;
            window.Show();
        }

        #endregion


        #region UI

        private void OnGUI()
        {
            if (position.width != m_OldPosition.width && Event.current.type == EventType.Layout)
            {
                m_Drawings = null;
                m_OldPosition = position;
            }

            GUILayout.BeginHorizontal();

            if (GUILayout.Toggle(m_ShowingStyles, "Styles", EditorStyles.toolbarButton) != m_ShowingStyles)
            {
                m_ShowingStyles = !m_ShowingStyles;
                m_ShowingIcons = !m_ShowingStyles;
                m_Drawings = null;
            }

            if (GUILayout.Toggle(m_ShowingIcons, "Icons", EditorStyles.toolbarButton) != m_ShowingIcons)
            {
                m_ShowingIcons = !m_ShowingIcons;
                m_ShowingStyles = !m_ShowingIcons;
                m_Drawings = null;
            }

            GUILayout.EndHorizontal();

            string newSearch = GUILayout.TextField(m_Search);
            if (newSearch != m_Search)
            {
                m_Search = newSearch;
                m_Drawings = null;
            }

            float top = 36;

            if (m_Drawings == null)
            {
                string lowerSearch = m_Search.ToLower();

                m_Drawings = new List<Drawing>();

                GUIContent inactiveText = new GUIContent("inactive");
                GUIContent activeText = new GUIContent("active");

                float x = 5.0f;
                float y = 5.0f;

                if (m_ShowingStyles)
                {
                    foreach (GUIStyle ss in GUI.skin.customStyles)
                    {
                        if (lowerSearch != "" && !ss.name.ToLower().Contains(lowerSearch))
                            continue;

                        GUIStyle thisStyle = ss;

                        Drawing draw = new Drawing();

                        float width = Mathf.Max(
                            100.0f,
                            GUI.skin.button.CalcSize(new GUIContent(ss.name)).x,
                            ss.CalcSize(inactiveText).x + ss.CalcSize(activeText).x
                                          ) + 16.0f;

                        float height = 60.0f;

                        if (x + width > position.width - 32 && x > 5.0f)
                        {
                            x = 5.0f;
                            y += height + 10.0f;
                        }

                        draw.Rect = new Rect(x, y, width, height);

                        width -= 8.0f;

                        draw.Draw = () =>
                        {
                            if (GUILayout.Button(thisStyle.name, GUILayout.Width(width)))
                                CopyText("(GUIStyle)\"" + thisStyle.name + "\"");

                            GUILayout.BeginHorizontal();
                            GUILayout.Toggle(false, inactiveText, thisStyle, GUILayout.Width(width / 2));
                            GUILayout.Toggle(false, activeText, thisStyle, GUILayout.Width(width / 2));
                            GUILayout.EndHorizontal();
                        };

                        x += width + 18.0f;

                        m_Drawings.Add(draw);
                    }
                }
                else if (m_ShowingIcons)
                {
                    if (m_Objects == null)
                    {
                        m_Objects = new List<UnityEngine.Object>(Resources.FindObjectsOfTypeAll(typeof(Texture)));
                        m_Objects.Sort((pA, pB) => System.String.Compare(pA.name, pB.name, System.StringComparison.OrdinalIgnoreCase));
                    }

                    float rowHeight = 0.0f;

                    foreach (UnityEngine.Object oo in m_Objects)
                    {
                        Texture texture = (Texture)oo;

                        if (texture.name == "")
                            continue;

                        if (lowerSearch != "" && !texture.name.ToLower().Contains(lowerSearch))
                            continue;

                        Drawing draw = new Drawing();

                        float width = Mathf.Max(
                            GUI.skin.button.CalcSize(new GUIContent(texture.name)).x,
                            texture.width
                        ) + 8.0f;

                        float height = texture.height + GUI.skin.button.CalcSize(new GUIContent(texture.name)).y + 8.0f;

                        if (x + width > position.width - 32.0f)
                        {
                            x = 5.0f;
                            y += rowHeight + 8.0f;
                            rowHeight = 0.0f;
                        }

                        draw.Rect = new Rect(x, y, width, height);

                        rowHeight = Mathf.Max(rowHeight, height);

                        width -= 8.0f;

                        draw.Draw = () =>
                        {
                            if (GUILayout.Button(texture.name, GUILayout.Width(width)))
                                CopyText("EditorGUIUtility.FindTexture( \"" + texture.name + "\" )");

                            Rect textureRect = GUILayoutUtility.GetRect(texture.width, texture.width, texture.height, texture.height, GUILayout.ExpandHeight(false), GUILayout.ExpandWidth(false));
                            EditorGUI.DrawTextureTransparent(textureRect, texture);
                        };

                        x += width + 8.0f;

                        m_Drawings.Add(draw);
                    }
                }

                m_MaxY = y;
            }

            Rect r = position;
            r.y = top;
            r.height -= r.y;
            r.x = r.width - 16;
            r.width = 16;

            float areaHeight = position.height - top;
            m_ScrollPos = GUI.VerticalScrollbar(r, m_ScrollPos, areaHeight, 0.0f, m_MaxY);

            Rect area = new Rect(0, top, position.width - 16.0f, areaHeight);
            GUILayout.BeginArea(area);

            int count = 0;
            foreach (Drawing draw in m_Drawings)
            {
                Rect newRect = draw.Rect;
                newRect.y -= m_ScrollPos;

                if (newRect.y + newRect.height > 0 && newRect.y < areaHeight)
                {
                    GUILayout.BeginArea(newRect, GUI.skin.textField);
                    draw.Draw();
                    GUILayout.EndArea();

                    count++;
                }
            }

            GUILayout.EndArea();
        }

        private void CopyText(string pText)
        {
            TextEditor editor = new TextEditor();

            #if UNITY_4
            //editor.content = new GUIContent(pText); // Unity 4.x code
            #else
            editor.text = pText;
            #endif

            editor.SelectAll();
            editor.Copy();
        }

        #endregion
    }

}