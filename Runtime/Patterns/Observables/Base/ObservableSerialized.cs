using UnityEngine;
using UnityEngine.Events;

namespace MuffinDev.Core
{

    /// <summary>
    /// Shortcut for making an Observable for a SerializedProperty.
    /// </summary>
    /// <typeparam name="TPropertyType">Type of the property to observe.</typeparam>
    /// <typeparam name="TEventType">Type of the event to trigger on changes.</typeparam>
    [System.Serializable]
    public abstract class ObservableSerialized<TPropertyType, TEventType> : Observable<TPropertyType>
        where TEventType : UnityEvent<TPropertyType>
    {

        #region Properties

        [SerializeField]
        private TPropertyType m_Value = default(TPropertyType);

        [SerializeField]
        private TEventType m_OnChange = default(TEventType);

        #endregion


        #region Initialization

        /// <summary>
        /// Creates an instance of ObservableSimple.
        /// </summary>
        public ObservableSerialized() { }

        /// <summary>
        /// Creates an instance of ObservableSimple, and initializes its value.
        /// </summary>
        public ObservableSerialized(TPropertyType _Value)
        {
            m_Value = _Value;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Triggers the observable's changes event.
        /// </summary>
        public override void Notify()
        {
            SetValue(m_Value, true);
        }

        #endregion


        #region Protected Methods

        /// <summary>
        /// Called when changes event is triggered.
        /// </summary>
        protected override void HandleChanges(ObservableChanges<TPropertyType> _Changes)
        {
            m_Value = _Changes.NewValue;
            m_OnChange.Invoke(m_Value);
        }

        #endregion


        #region Accessors

        /// <summary>
        /// Gets the event triggered at each changes of this observable's value.
        /// </summary>
        public TEventType OnChange
        {
            get { return m_OnChange; }
        }

        #endregion

    }

}