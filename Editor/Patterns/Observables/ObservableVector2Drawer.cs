using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core
{

    [CustomPropertyDrawer(typeof(ObservableVector2))]
    public class ObservableVector2Drawer : ObservableDrawer<Vector2>
	{

        /// <summary>
        /// Called if value property is changed in the inspector.
        /// </summary>
        /// <param name="_Observable">The Observable shown in the inspector.</param>
        /// <param name="_ValueProperty">Property containing the updated value.</param>
        protected override void OnValueChange(Observable<Vector2> _Observable, SerializedProperty _ValueProperty)
        {
            _Observable.Value = _ValueProperty.vector2Value;
        }

    }

}