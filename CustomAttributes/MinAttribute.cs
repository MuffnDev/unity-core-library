using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    ///	The MinAttribute avoids a value to be under a given number.
    ///</summary>
    public class MinAttribute : PropertyAttribute
    {

        private float m_Min = 0.0f;

        protected MinAttribute()
        {

        }

        public MinAttribute(float _Min)
        {
            m_Min = _Min;
        }

        public MinAttribute(int _Min)
        {
            m_Min = _Min;
        }

        public float Min
        {
            get { return m_Min; }
        }

    }

    ///<summary>
    ///	The MinimumAttribute avoids a value to be under a given number.
    ///	Unity 2018 added a Min attribute, so you can use Minimum attribute instead to avoid conflicts.
    ///</summary>
    public class MinimumAttribute : MinAttribute
    {

        public MinimumAttribute(float _Min) : base(_Min) { }

        public MinimumAttribute(int _Min) : base(_Min) { }

    }

}