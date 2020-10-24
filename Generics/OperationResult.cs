namespace MuffinDev
{

	///<summary>
	/// Represents the result of an operation, that contains the operation state and the expected informations.
	///</summary>
	public struct OperationResult<T>
	{

        #region Properties

        private OperationState m_State;
        private T m_Result;

        #endregion


        #region Public Methods

        /// <summary>
        /// Adds a log to the list.
        /// </summary>
        /// <param name="_AvailableForUser">Can this message be displayed to user?</param>
        public void AddLog(string _Content, ELogType _Type = ELogType.Log, bool _AvailableForUser = false)
        {
            m_State.AddLog(_Content, _Type, _AvailableForUser);
        }

        /// <summary>
        /// Display all logs in the list.
        /// </summary>
        public void DisplayLogs()
        {
            m_State.DisplayLogs();
        }

        #endregion


        #region Accessors

        public OperationState State
        {
            get { return m_State; }
            set { m_State = value; }
        }

        public T Result
        {
            get { return m_Result; }
            set { m_Result = value; }
        }

        public bool IsOk
        {
            get { return m_State.IsOk; }
            set { m_State.IsOk = value; }
        }

        public Log[] Logs
        {
            get { return m_State.Logs; }
            set { m_State.Logs = value; }
        }

        #endregion


        #region Operators

        /// <summary>
        /// Implicit conversion into a boolean, using the operation state as boolean.
        /// </summary>
        public static implicit operator bool(OperationResult<T> _Operation)
        {
            return _Operation.State;
        }

        #endregion

    }

}