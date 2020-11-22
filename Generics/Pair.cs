using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Represents an element that contains two values.
    /// </summary>
	public class Pair<T1, T2>
	{
        [SerializeField]
        private T1 m_First;

        [SerializeField]
        private T2 m_Second;

        public Pair() { }

        public Pair(T1 _First, T2 _Second)
        {
            m_First = _First;
            m_Second = _Second;
        }

        public T1 First
        {
            get { return m_First; }
            set { m_First = value; }
        }

        public T2 Second
        {
            get { return m_Second; }
            set { m_Second = value; }
        }
    }

}