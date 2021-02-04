using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    ///	This attribute draws a MinMaxSlider field in editor.
    ///	Since this type of field is used to set 2 values, this attribute can only be applied on Vector2 properties.
    ///	As output, the X value of Vector2 property is "min", and Y value is "max".
    ///</summary>
    public class RangeMinMaxAttribute : PropertyAttribute
    {

        #region Members

        private float m_MinLimit = 0.0f;
        private float m_MaxLimit = 0.0f;

        private bool m_ForceInt = false;

        #endregion


        #region Initialisation / Destruction

        private RangeMinMaxAttribute()
        {

        }

        public RangeMinMaxAttribute(float _MinLimit, float _MaxLimit)
        {
            m_MinLimit = _MinLimit;
            m_MaxLimit = _MaxLimit;
        }

        public RangeMinMaxAttribute(float _MinLimit, float _MaxLimit, bool _ForceIntegerValues)
        {
            m_MinLimit = _MinLimit;
            m_MaxLimit = _MaxLimit;
            m_ForceInt = _ForceIntegerValues;
        }

        public RangeMinMaxAttribute(int _MinLimit, int _MaxLimit, bool _ForceIntegerValues = true)
        {
            m_MinLimit = (float)_MinLimit;
            m_MaxLimit = (float)_MaxLimit;
            m_ForceInt = _ForceIntegerValues;
        }

        #endregion


        #region Accessors

        public float MinLimit
        {
            get { return m_MinLimit; }
        }

        public float MaxLimit
        {
            get { return m_MaxLimit; }
        }

        public bool ForceInt
        {
            get { return m_ForceInt; }
        }

        #endregion

    }

}