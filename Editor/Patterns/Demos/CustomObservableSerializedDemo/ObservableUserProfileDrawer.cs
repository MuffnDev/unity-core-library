using UnityEditor;

namespace MuffinDev.Core.Demos
{

    [CustomPropertyDrawer(typeof(ObservableUserProfile))]
    public class ObservableUserProfileDrawer : ObservableDrawer<UserProfile>
    {

        private const string USERNAME_PROPERTY_NAME = "m_Username";
        private const string AGE_PROPERTY_NAME = "m_Age";

        protected override void OnValueChange(Observable<UserProfile> _Observable, SerializedProperty _ValueProperty)
        {
            string username = _ValueProperty.FindPropertyRelative(USERNAME_PROPERTY_NAME).stringValue;
            int age = _ValueProperty.FindPropertyRelative(AGE_PROPERTY_NAME).intValue;

            _Observable.Value = new UserProfile(username, age);
        }

        protected override bool CanExpand
        {
            get { return true; }
        }

    }

}