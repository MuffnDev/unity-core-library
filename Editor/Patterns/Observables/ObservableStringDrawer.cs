using UnityEditor;

namespace MuffinDev.Core
{

    [CustomPropertyDrawer(typeof(ObservableString))]
    public class ObservableStringDrawer : ObservableDrawer<string>
	{

        /// <summary>
        /// Called if value property is changed in the inspector.
        /// </summary>
        /// <param name="_Observable">The Observable shown in the inspector.</param>
        /// <param name="_ValueProperty">Property containing the updated value.</param>
        protected override void OnValueChange(Observable<string> _Observable, SerializedProperty _ValueProperty)
        {
            _Observable.Value = _ValueProperty.stringValue;
        }

    }

}