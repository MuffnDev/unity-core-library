using UnityEngine;
using UnityEngine.UI;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Observables UI")]
    public class ObservablesDemoUI : MonoBehaviour
    {

        #region Properties

        [SerializeField]
        private Slider m_IntObservableSlider = null;

        [SerializeField]
        private Text m_Vector2ObservableText = null;

        [SerializeField]
        private Text m_Vector3ObservableText = null;

        #endregion


        #region Public Methods

        public void SetInt(int _Value)
        {
            m_IntObservableSlider.value = _Value;
        }

        public void SetVector2(Vector2 _Value)
        {
            m_Vector2ObservableText.text = _Value.ToString();
        }

        public void SetVector3(Vector3 _Value)
        {
            m_Vector3ObservableText.text = _Value.ToString();
        }

        #endregion

    }

}