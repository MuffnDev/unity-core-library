using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Custom attribute for AnimationCurve properties.
    /// It allows you to make a custom curve editor in the inspector view, with min and max time or value, and a custom color.
    /// </summary>
    public class AnimCurveAttribute : PropertyAttribute
    {

        #region Properties

        private float m_MinTime = 0.0f;
        private float m_MaxTime = 1.0f;

        private float m_MinValue = 0.0f;
        private float m_MaxValue = 1.0f;

        private Color m_CurveColor = Color.green;

        #endregion


        #region Initialisation / Destruction

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time and value clamped between 0 and 1.
        /// </summary>
        public AnimCurveAttribute()
        {

        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time and value clamped between 0 and 1.
        /// </summary>
        /// <param name="_CurveColor">The color of the curve in the editor.</param>
        public AnimCurveAttribute(EColor _CurveColor)
        {
            m_CurveColor = GetCurveColor(_CurveColor);
        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time clamped between 0 and the given max time, and value clamped between 0 and
        /// given max value.
        /// </summary>
        public AnimCurveAttribute(float _MaxTime, float _MaxValue)
        {
            m_MaxTime = _MaxTime;
            m_MaxValue = _MaxValue;
        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time clamped between 0 and the given max time, and value clamped between 0 and
        /// the given max value.
        /// </summary>
        /// <param name="_CurveColor">The color of the curve in the editor.</param>
        public AnimCurveAttribute(float _MaxTime, float _MaxValue, EColor _CurveColor)
        {
            m_MaxTime = _MaxTime;
            m_MaxValue = _MaxValue;

            m_CurveColor = GetCurveColor(_CurveColor);
        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min
        /// and max value.
        /// </summary>
        public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue)
        {
            m_MinTime = _MinTime;
            m_MinValue = _MinValue;

            m_MaxTime = _MaxTime;
            m_MaxValue = _MaxValue;
        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min
        /// and max value.
        /// </summary>
        /// <param name="_CurveColor">The color of the curve in the editor.</param>
        public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue, EColor _CurveColor)
        {
            m_MinTime = _MinTime;
            m_MinValue = _MinValue;

            m_MaxTime = _MaxTime;
            m_MaxValue = _MaxValue;

            m_CurveColor = GetCurveColor(_CurveColor);
        }

        /// <summary>
        /// Creates an AnimCurveAttribute instance with time clamped between the given min and max time, and value clamped between the min
        /// and max value.
        /// </summary>
        /// <param name="_ColorR">Red component of the curve's color (must be between 0 and 1).</param>
        /// <param name="_ColorG">Green component of the curve's color (must be between 0 and 1).</param>
        /// <param name="_ColorB">Blue component of the curve's color (must be between 0 and 1).</param>
        public AnimCurveAttribute(float _MinTime, float _MaxTime, float _MinValue, float _MaxValue, float _ColorR, float _ColorG, float _ColorB)
        {
            m_MinTime = _MinTime;
            m_MinValue = _MinValue;

            m_MaxTime = _MaxTime;
            m_MaxValue = _MaxValue;

            m_CurveColor = new Color(_ColorR, _ColorG, _ColorB);
        }

        #endregion


        #region Accessors

        public float MinTime
        {
            get { return m_MinTime; }
        }

        public float MaxTime
        {
            get { return m_MaxTime; }
        }

        public float MinValue
        {
            get { return m_MinValue; }
        }

        public float MaxValue
        {
            get { return m_MaxValue; }
        }

        public Color CurveColor
        {
            get { return m_CurveColor; }
        }

        private Color GetCurveColor(EColor _Color)
        {
            return Colors.Get(_Color);
        }

        /// <summary>
        /// Creates a Rect instance using the defined curve editor values:
        ///     - x is min time
        ///     - y is min value
        ///     - width is max time
        ///     - height is max value
        /// </summary>
        /// <returns>Returns the Rect instance.</returns>
        public Rect GetRanges()
        {
            return new Rect(m_MinTime, m_MinValue, m_MaxTime, m_MaxValue);
        }

        #endregion

    }

}