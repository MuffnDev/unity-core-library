using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    ///	Indent the following member by the given indent level in the inspector.
    ///</summary>
    public class IndentAttribute : PropertyAttribute
    {

        private int m_IndentLevel = 1;

        public IndentAttribute()
        {

        }

        public IndentAttribute(int _IndentLevel)
        {
            m_IndentLevel = _IndentLevel;
        }

        public int IndentLevel
        {
            get { return m_IndentLevel; }
        }

    }

}