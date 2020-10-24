namespace MuffinDev
{

    /// <summary>
    /// Represents an observable boolean property.
    /// </summary>
    [System.Serializable]
    public class ObservableBool : ObservableSerialized<bool, BoolEvent>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableBool() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableBool(bool _Value) : base(_Value) { }

    }

}