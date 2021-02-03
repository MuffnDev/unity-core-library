using UnityEngine;

namespace MuffinDev.Core.Demos
{

    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("Muffin Dev/AI/Demos/Blackboards User")]
    public class BlackboardsUser : MonoBehaviour
    {

        [SerializeField]
        private BlackboardAssetDemo m_BlackboardAsset = null;

        [SerializeField]
        private Blackboard m_RawBlackboardProperty = new Blackboard();
        public Blackboard RawBlackboardProperty => m_RawBlackboardProperty;

        private void Start()
        {
            if (m_BlackboardAsset != null && m_BlackboardAsset.Blackboard.Count > 0)
            {
                Debug.Log("----- BLACKBOARD ASSET -----");

                Debug.Log("String: " + m_BlackboardAsset.GetValue<string>("String"));
                Debug.Log("Null: " + m_BlackboardAsset.GetValue<object>("Null"));
                Debug.Log("Vector3: " + m_BlackboardAsset.GetValue<Vector3>("Vector3"));
                Debug.Log("Int: " + m_BlackboardAsset.GetValue<int>("Int"));
                Debug.Log("Player data: " + m_BlackboardAsset.GetValue<BlackboardAssetDemo.PlayerData>("Player data"));
                Debug.Log("Unknown: " + m_BlackboardAsset.GetValue<GameObject>("Unknown"));
            }
            
            if(m_RawBlackboardProperty.Count > 0)
            {
                Debug.Log("----- BLACKBOARD PROPERTY -----");

                int i = 0;
                foreach(object data in m_RawBlackboardProperty)
                {
                    Debug.Log($"Raw Blackboard Property [{i}]: {data}");
                    i++;
                }
            }
        }

    }

}