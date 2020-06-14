using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Represents an observable Vector3 property.
    /// </summary>
    [System.Serializable]
    public class ObservableVector3 : ObservableSerialized<Vector3, Vector3Event>
    {

        /// <summary>
        /// Creates an instance of this Observable.
        /// </summary>
        public ObservableVector3() : base() { }

        /// <summary>
        /// Creates an instance of this Observable, and initializes its value.
        /// </summary>
        public ObservableVector3(Vector3 _Value) : base(_Value) { }

    }

}