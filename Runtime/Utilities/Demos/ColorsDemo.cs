using UnityEngine;

namespace MuffinDev.Core.Demos
{

	[AddComponentMenu("Muffin Dev/Demos/Colors Demo")]
	public class ColorsDemo : MonoBehaviour
	{

        #region Subclasses

        /// <summary>
        /// Represents a named group of ColorDemoBox instances.
        /// </summary>
        private class ColorDemoBoxesGroup
        {
            private string m_GroupName = string.Empty;
            private ColorDemoBox[] m_ColorBoxes = null;

            /// <summary>
            /// Creates an instance of ColorDemoBoxesGroup, with the given name and the given ColorDemoBox collection.
            /// </summary>
            public ColorDemoBoxesGroup(string _GroupName, ColorDemoBox[] _ColorBoxes)
            {
                m_GroupName = _GroupName;
                m_ColorBoxes = _ColorBoxes;
            }

            /// <summary>
            /// Displays the GUI for this group.
            /// You must use this method in an OnGUI() context.
            /// </summary>
            public void DisplayGroup(float _ColorBoxWidth, float _ColorBoxHeight)
            {
                GUILayout.BeginVertical(GUI.skin.box);
                {
                    GUILayout.Label(m_GroupName);
                    for (int i = 0; i < m_ColorBoxes.Length; i++)
                    {
                        GUILayout.Box(m_ColorBoxes[i].ColorName, m_ColorBoxes[i].Style, GUILayout.Width(_ColorBoxWidth), GUILayout.Height(_ColorBoxHeight));
                    }
                }
                GUILayout.EndVertical();
            }
        }

        /// <summary>
        /// Represents a GUI box for a color.
        /// </summary>
        private class ColorDemoBox
        {
            // Name of the color. If not specified, the enum value label will be used.
            private string m_ColorName = string.Empty;
            private EColor m_Color = EColor.Black;
            private bool m_WhiteText = false;
            // Cache
            private GUIStyle m_Style = null;

            /// <summary>
            /// Creates an instance of ColorDemoBox for the given color.
            /// </summary>
            /// <param name="_WhiteText">Should the label content be white?</param>
            public ColorDemoBox(EColor _Color, bool _WhiteText = false)
            {
                m_Color = _Color;
                m_WhiteText = _WhiteText;
            }

            /// <summary>
            /// Creates an instance of ColorDemoBox for the given color, using the given name as color label.
            /// </summary>
            /// <param name="_WhiteText">Should the label content be white?</param>
            public ColorDemoBox(string _ColorName, EColor _Color, bool _WhiteText = false)
            {
                m_ColorName = _ColorName;
                m_Color = _Color;
                m_WhiteText = _WhiteText;
            }

            /// <summary>
            /// Gets this color name.
            /// Uses the color name if defined, otherwise the enum value label.
            /// </summary>
            public string ColorName
            {
                get { return string.IsNullOrEmpty(m_ColorName) ? m_Color.ToString() : m_ColorName; }
            }

            /// <summary>
            /// Gets this color box style.
            /// Note that the GUIStyle will be computed only the first time this accessor is called.
            /// </summary>
            public GUIStyle Style
            {
                get
                {
                    if (m_Style == null)
                    {
                        m_Style = new GUIStyle(GUI.skin.box);

                        m_Style.normal.background = new Texture2D(1, 1);
                        m_Style.normal.background.SetPixel(0, 0, Colors.Get(m_Color));
                        m_Style.normal.background.Apply();

                        m_Style.normal.textColor = (m_WhiteText) ? Color.white : Color.black;
                    }

                    return m_Style;
                }
            }
        }

        #endregion


        #region Properties

        private static readonly ColorDemoBoxesGroup[] COLOR_BOX_GROUPS = null;

        [SerializeField]
        private float m_BoxWidth = 110f;

        [SerializeField]
        private float m_BoxHeight = 24f;

        #endregion


        #region Initialization

        /// <summary>
        /// Initializes the ColorBoxDemo instances, grouped by category.
        /// </summary>
        static ColorsDemo()
        {
            COLOR_BOX_GROUPS = new ColorDemoBoxesGroup[]
            {
                new ColorDemoBoxesGroup("RVB components", new ColorDemoBox[]
                {
                    new ColorDemoBox(EColor.Red, true),
                    new ColorDemoBox(EColor.Maroon, true),
                    new ColorDemoBox(EColor.Lime, false),
                    new ColorDemoBox(EColor.Green, true),
                    new ColorDemoBox(EColor.Blue, true),
                    new ColorDemoBox(EColor.Navy, true)
                }),

                new ColorDemoBoxesGroup("Tints", new ColorDemoBox[]
                {
                    new ColorDemoBox(EColor.Black, true),
                    new ColorDemoBox(EColor.Grey),
                    new ColorDemoBox(EColor.White)
                }),

                new ColorDemoBoxesGroup("Other colors", new ColorDemoBox[]
                {
                    new ColorDemoBox(EColor.Orange, true),
                    new ColorDemoBox(EColor.Yellow, false),
                    new ColorDemoBox(EColor.Olive, true),
                    new ColorDemoBox(EColor.Purple, true),
                    new ColorDemoBox("Magenta", EColor.Magenta, true),
                    new ColorDemoBox(EColor.Pink, true),
                    new ColorDemoBox(EColor.Teal, true),
                    new ColorDemoBox(EColor.Azure, true),
                    new ColorDemoBox("Cyan", EColor.Cyan, false)
                }),

                new ColorDemoBoxesGroup("Aliases", new ColorDemoBox[]
                {
                    new ColorDemoBox("Fuchsia", EColor.Fuchsia, true),
                    new ColorDemoBox("Aqua", EColor.Aqua, false)
                }),

                new ColorDemoBoxesGroup("Special", new ColorDemoBox[]
                {
                    new ColorDemoBox("Alpha 0%", EColor.Black | EColor.Alpha0, true),
                    new ColorDemoBox("Alpha 14%", EColor.Black | EColor.Alpha14, true),
                    new ColorDemoBox("Alpha 25%", EColor.Black | EColor.Alpha25, true),
                    new ColorDemoBox("Alpha 50%", EColor.Black | EColor.Alpha50, true),
                    new ColorDemoBox("Alpha 75%", EColor.Black | EColor.Alpha75, true),
                    new ColorDemoBox("Alpha 87%", EColor.Black | EColor.Alpha87, true),
                    new ColorDemoBox("Alpha 100%", EColor.Black, true),
                }),
            };
        }

        #endregion


        #region GUI

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                for (int i = 0; i < COLOR_BOX_GROUPS.Length; i++)
                {
                    COLOR_BOX_GROUPS[i].DisplayGroup(m_BoxWidth, m_BoxHeight);
                }
            }
            GUILayout.EndHorizontal();
        }

        #endregion

    }

}