using UnityEngine;

namespace MuffinDev.Core
{

	///<summary>
	/// Represents a log entry.
	///</summary>
    [System.Serializable]
	public struct Log
	{

        // Error level of the log
        [SerializeField]
        [Tooltip("Error level of the log")]
        private ELogType m_Type;

        // Content of the log message
        [SerializeField]
        [Tooltip("Content of the log message")]
        private string m_Content;

        // Defines if this log message can be display to user
        [SerializeField]
        [Tooltip("Defines if this log message can be display to user")]
        private bool m_AvailableForUser;

        /// <summary>
        /// Log constructor.
        /// </summary>
        /// <param name="_Content">Content of the log message.</param>
        /// <param name="_Type">Error level of this log entry.</param>
        /// <param name="_AvailableForUser">Can this message be displayed to user?</param>
        public Log(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
        {
            m_Content = _Content;
            m_Type = ELogType.Log;
            m_AvailableForUser = false;
        }

        public string Content
        {
            get { return m_Content; }
            set { m_Content = value; }
        }

        public ELogType Type
        {
            get { return m_Type; }
            set { m_Type = value; }
        }

        public bool AvailableForUser
        {
            get { return m_AvailableForUser; }
            set { m_AvailableForUser = value; }
        }

    }

}