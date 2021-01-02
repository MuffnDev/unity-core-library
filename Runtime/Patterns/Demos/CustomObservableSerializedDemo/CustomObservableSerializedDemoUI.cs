using UnityEngine;
using UnityEngine.UI;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Demos/Custom Observable Serialized UI")]
    public class CustomObservableSerializedDemoUI : MonoBehaviour
    {

        [SerializeField]
        private Text m_Username = null;

        [SerializeField]
        private Text m_Age = null;

        public void SetUserProfile(UserProfile _Profile)
        {
            m_Username.text = _Profile.Username;
            m_Age.text = _Profile.Age.ToString();
        }

    }

}