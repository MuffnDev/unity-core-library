using UnityEngine;
using MuffinDev.Core.Demos;
public class User : MonoBehaviour
{
    public ObservableUserProfile userProfile = new ObservableUserProfile();

    private void Start()
    {
        // Trigger user profile changes at initialization, and so initialize all observers
        userProfile.Notify();
    }
}