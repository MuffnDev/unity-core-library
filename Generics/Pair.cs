using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Represents an element that contains two values.
    /// </summary>
	public class Pair<T, U>
	{
        [SerializeField]
        private T m_First;

        [SerializeField]
        private U m_Second;

        public Pair() { }

        public Pair(T _First, U _Second)
        {
            m_First = _First;
            m_Second = _Second;
        }

        public T First
        {
            get { return m_First; }
            set { m_First = value; }
        }

        public U Second
        {
            get { return m_Second; }
            set { m_Second = value; }
        }
    }

}