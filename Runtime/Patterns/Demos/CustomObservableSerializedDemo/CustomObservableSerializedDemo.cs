using UnityEngine;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Demos/Custom Observable Serialized")]
    public class CustomObservableSerializedDemo : MonoBehaviour
    {

        [SerializeField]
        private ObservableUserProfile m_UserProfile = new ObservableUserProfile();

        private void Start()
        {
            m_UserProfile.Notify();
        }

    }

}