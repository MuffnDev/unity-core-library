using UnityEngine;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Demos/Observables")]
    public class ObservablesDemo : MonoBehaviour
    {

        #region Properties

        [SerializeField]
        private ObservableString m_StringValue = new ObservableString("Test");

        [SerializeField]
        private ObservableInt m_IntValue = new ObservableInt(30);

        [SerializeField]
        private ObservableFloat m_FloatValue = new ObservableFloat(50f);

        [SerializeField]
        private ObservableVector2 m_Vector2Value = new ObservableVector2(Vector2.right);

        [SerializeField]
        private ObservableVector3 m_Vector3Value = new ObservableVector3(Vector3.one);

        [SerializeField]
        private ObservableBool m_BoolValue = new ObservableBool(false);

        #endregion


        #region Lifecycle

        private void Start()
        {
            m_StringValue.Notify();
            m_IntValue.Notify();
            m_FloatValue.Notify();
            m_Vector2Value.Notify();
            m_Vector3Value.Notify();
            m_BoolValue.Notify();
        }

        #endregion

    }

}