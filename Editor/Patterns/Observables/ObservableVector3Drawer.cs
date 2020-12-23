using UnityEngine;
using UnityEditor;

namespace MuffinDev.Core
{

    [CustomPropertyDrawer(typeof(ObservableVector3))]
    public class ObservableVector3Drawer : ObservableDrawer<Vector3>
	{

        /// <summary>
        /// Called if value property is changed in the inspector.
        /// </summary>
        /// <param name="_Observable">The Observable shown in the inspector.</param>
        /// <param name="_ValueProperty">Property containing the updated value.</param>
        protected override void OnValueChange(Observable<Vector3> _Observable, SerializedProperty _ValueProperty)
        {
            _Observable.Value = _ValueProperty.vector3Value;
        }

    }

}