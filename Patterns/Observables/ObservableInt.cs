namespace MuffinDev.Core
{

    /// <summary>
    /// Represents an observable integer property.
    /// </summary>
    [System.Serializable]
    public class ObservableInt : ObservableSerialized<int, IntEvent>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableInt() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableInt(int _Value) : base(_Value) { }

    }

}