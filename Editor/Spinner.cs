using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core.EditorOnly
{

    ///<summary>
    /// Utility class to update and draw a loading spinner icon.
    ///</summary>
    public class Spinner
	{

        #region Properties

        // The interval of each spinner update
        private float m_SpinInterval = .06f;

        // The current spinner icon frame (from EditorIcons.LoadingSpinnerIcons)
        private int m_SpinnerIconIndex = 0;

        // The last timestamp when this Spinner has been updated
        private double m_LastTimestamp = 0d;

        // Defines if this Spinner is paused
        private bool m_Paused = false;

        #endregion


        #region Initialization

        public Spinner()
        {
            m_LastTimestamp = EditorApplication.timeSinceStartup;
        }

        public Spinner(float _SpinInterval)
        {
            m_LastTimestamp = EditorApplication.timeSinceStartup;
            m_SpinInterval = _SpinInterval;
        }

        #endregion


        #region Public API

        public void Update()
        {
            if (m_Paused)
                return;

            double deltaTime = EditorApplication.timeSinceStartup - m_LastTimestamp;
            int steps = Mathf.FloorToInt(System.Convert.ToSingle(deltaTime / m_SpinInterval));
            for (int i = 0; i < steps; i++)
            {
                m_SpinnerIconIndex++;
                if (!EditorIcons.LoadingSpinnerIcons.IsInRange(m_SpinnerIconIndex))
                    m_SpinnerIconIndex = 0;
            }
            m_LastTimestamp += steps * m_SpinInterval;
        }

        public void Play()
        {
            m_Paused = false;
            m_LastTimestamp = EditorApplication.timeSinceStartup;
        }

        public void Pause()
        {
            m_Paused = true;
        }

        public void DrawGUI(params GUILayoutOption[] _GUILayoutOptions)
        {
            Rect rect = EditorGUILayout.GetControlRect(_GUILayoutOptions);
            DrawGUI(rect);
        }

        public void DrawGUI(Rect _Rect)
        {
            DrawGUI(_Rect, GUI.skin.label);
        }

        public void DrawGUI(Rect _Rect, GUIStyle _Style)
        {
            GUIContent content = new GUIContent { image = EditorIcons.LoadingSpinnerIcons[m_SpinnerIconIndex] };
            GUI.Label(_Rect, content, _Style);
        }

        public bool Paused
        {
            get { return m_Paused; }
            set { m_Paused = value; }
        }

        #endregion

    }

}