using System.Collections.Generic;

using UnityEngine;

namespace MuffinDev.Core
{

    ///<summary>
    /// Represents the state of an operation.
    ///</summary>
    [System.Serializable]
    public struct OperationState
    {

        #region Properties

        [SerializeField]
        private bool m_IsOk;

        [SerializeField]
        private Log[] m_Logs;

        #endregion


        #region Public Methods

        /// <summary>
        /// Adds a log to the list.
        /// </summary>
        /// <param name="_AvailableForUser">Can this message be displayed to user?</param>
        public void AddLog(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
        {
            Log log = new Log(_Content, _Type, _AvailableForUser);

            if(Logs == null)
            {
                Logs = new Log[1] { log };
            }

            else
            {
                int index = Logs.Length;
                System.Array.Resize(ref m_Logs, index + 1);
                Logs[index] = log;
            }
        }

        /// <summary>
        /// Display all logs in the list.
        /// </summary>
        public void DisplayLogs()
        {
            foreach(Log log in m_Logs)
            {
                switch(log.Type)
                {
                    case ELogType.Warning:
                    Debug.LogWarning(log.Content);
                    break;

                    case ELogType.Error:
                    Debug.LogWarning(log.Content);
                    break;

                    default:
                    Debug.Log(log.Content);
                    break;
                }
            }
        }

        #endregion


        #region Accessors

        public bool IsOk
        {
            get { return m_IsOk; }
            set { m_IsOk = value; }
        }

        public Log[] Logs
        {
            get { return m_Logs; }
            set { m_Logs = value; }
        }

        /// <summary>
        /// Gets only the logs of the list available for user.
        /// </summary>
        public Log[] GetUserLogs()
        {
            List<Log> userLogs = new List<Log>();

            foreach (Log log in m_Logs)
            {
                if(log.AvailableForUser)
                {
                    userLogs.Add(log);
                }
            }

            return userLogs.ToArray();
        }

        #endregion


        #region Operators

        /// <summary>
        /// Implicit conversion into a boolean, using the IsOk property.
        /// </summary>
        /// <param name="_Operation"></param>
        public static implicit operator bool(OperationState _Operation)
        {
            return _Operation.IsOk;
        }

        #endregion

    }

}