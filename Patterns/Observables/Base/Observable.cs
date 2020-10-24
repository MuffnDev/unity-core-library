namespace MuffinDev
{

    /// <summary>
    /// Base class for making observable values properties.
    /// This implementation of observable pattern is based on the work of Fajlworks on Unity forums:
    /// https://forum.unity.com/threads/observable-variables-in-editor.499528/
    /// </summary>
    /// <typeparam name="T">Type of the property to observe.</typeparam>
    [System.Serializable]
    public abstract class Observable<T>
    {

        #region Properties

        // Non-serialized value
        private T m_ObservableValue = default(T);

        #endregion


        #region Public Methods

        /// <summary>
        /// Triggers the observable's changes event.
        /// </summary>
        public virtual void Notify()
        {
            SetValue(m_ObservableValue, true);
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// Called when changes event is triggered.
        /// </summary>
        protected abstract void HandleChanges(ObservableChanges<T> _Changes);

        #endregion


        #region Accessors

        /// <summary>
        /// Gets this observable's value.
        /// Sets this observable's value, and triggers changes event if the value has really changed.
        /// </summary>
        public T Value
        {
            get { return m_ObservableValue; }
            set { SetValue(value); }
        }

        /// <summary>
        /// Sets this observable's value.
        /// </summary>
        /// <param name="_NewValue">The new value of this observable.</param>
        /// <param name="_ForceEvent">If true, the changes event will be triggered even if the given new value is the same as the current
        /// one.</param>
        protected virtual void SetValue(T _NewValue, bool _ForceEvent = false)
        {
            if (!_ForceEvent && _NewValue.Equals(m_ObservableValue))
            {
                return;
            }

            T previousValue = m_ObservableValue;
            m_ObservableValue = _NewValue;

            ObservableChanges<T> changes = new ObservableChanges<T>(previousValue, m_ObservableValue);
            HandleChanges(changes);
        }

        #endregion

    }

}