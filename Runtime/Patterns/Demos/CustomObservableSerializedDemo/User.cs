using UnityEngine;
using MuffinDev.Core.Demos;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Demos/User")]
    public class User : MonoBehaviour
    {

        [SerializeField]
        private ObservableUserProfile m_UserProfile = new ObservableUserProfile();

        private void Start()
        {
            // Trigger user profile changes at initialization, and so initialize all observers
            m_UserProfile.Notify();
        }

    }

}

