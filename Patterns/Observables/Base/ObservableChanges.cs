namespace MuffinDev
{

    /// <summary>
    /// Represents an observable value change.
    /// </summary>
    /// <typeparam name="T">Type of the observed property.</typeparam>
    public struct ObservableChanges<T>
    {

        private T m_PreviousValue;
        private T m_NewValue;

        /// <summary>
        /// Creates an instance of ObservableChanges.
        /// </summary>
        /// <param name="_PreviousValue">The previous value of the observable property.</param>
        /// <param name="_NewValue">The new value of the observable property.</param>
        public ObservableChanges(T _PreviousValue, T _NewValue)
        {
            m_PreviousValue = _PreviousValue;
            m_NewValue = _NewValue;
        }

        /// <summary>
        /// Gets the previous value of the observable property.
        /// </summary>
        public T PreviousValue { get { return m_PreviousValue; } }

        /// <summary>
        /// Gets the new value of the observable property.
        /// </summary>
        public T NewValue { get { return m_NewValue; } }

    }

}