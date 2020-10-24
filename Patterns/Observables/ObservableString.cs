namespace MuffinDev
{

    /// <summary>
    /// Represents an observable string property.
    /// </summary>
    [System.Serializable]
    public class ObservableString : ObservableSerialized<string, StringEvent>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableString() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableString(string _Value) : base(_Value) { }

    }

}