using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Represents an observable Vector2 property.
    /// </summary>
    [System.Serializable]
    public class ObservableVector2 : ObservableSerialized<Vector2, Vector2Event>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableVector2() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableVector2(Vector2 _Value) : base(_Value) { }

    }

}