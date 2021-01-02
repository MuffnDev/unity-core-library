using UnityEngine;

namespace MuffinDev.Core.Demos
{

    [System.Serializable]
    public class UserProfile
    {
        [SerializeField]
        private string m_Username;

        [SerializeField]
        private int m_Age;

        public UserProfile(string _Username, int _Age)
        {
            m_Username = _Username;
            m_Age = _Age;
        }

        public string Username { get { return m_Username; } }
        public int Age { get { return m_Age; } }
    }

}