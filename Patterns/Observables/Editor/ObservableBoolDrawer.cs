using UnityEditor;

namespace MuffinDev
{

    [CustomPropertyDrawer(typeof(ObservableBool))]
    public class ObservableBoolDrawer : ObservableDrawer<bool>
	{

        /// <summary>
        /// Called if value property is changed in the inspector.
        /// </summary>
        /// <param name="_Observable">The Observable shown in the inspector.</param>
        /// <param name="_ValueProperty">Property containing the updated value.</param>
        protected override void OnValueChange(Observable<bool> _Observable, SerializedProperty _ValueProperty)
        {
            _Observable.Value = _ValueProperty.boolValue;
        }

    }

}