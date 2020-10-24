using UnityEngine;

namespace MuffinDev
{

    ///<summary>
    ///	The MaxAttribute avoids a value to be over a given number.
    ///</summary>
    public class MaxAttribute : PropertyAttribute
    {

        private float m_Max = 0.0f;

        private MaxAttribute()
        {

        }

        public MaxAttribute(float _Max)
        {
            m_Max = _Max;
        }

        public MaxAttribute(int _Max)
        {
            m_Max = _Max;
        }

        public float Max
        {
            get { return m_Max; }
        }

    }

    ///<summary>
    ///	The MaxAttribute avoids a value to be over a given number.
    ///	Unity 2018 added a Min attribute, so we created Minimum attribute to avoid conflicts.
    ///	We reproduced that scheme with Max and Maximum attributes to avoid incoherences.
    ///</summary>
    public class MaximumAttribute : MaxAttribute
    {

        public MaximumAttribute(float _Max) : base (_Max) { }

        public MaximumAttribute(int _Max) : base (_Max) { }

    }

}