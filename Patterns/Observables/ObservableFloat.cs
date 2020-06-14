namespace MuffinDev
{

    /// <summary>
    /// Represents an observable boolean property.
    /// </summary>
    [System.Serializable]
    public class ObservableFloat : ObservableSerialized<float, FloatEvent>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableFloat() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableFloat(float _Value) : base(_Value) { }

    }



}