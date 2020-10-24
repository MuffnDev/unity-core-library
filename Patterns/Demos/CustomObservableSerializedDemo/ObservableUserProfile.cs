using UnityEngine.Events;

namespace MuffinDev.Core.Demos
{

    [System.Serializable]
    public class UserProfileEvent : UnityEvent<UserProfile> { }

    [System.Serializable]
    public class ObservableUserProfile : ObservableSerialized<UserProfile, UserProfileEvent> { }

}