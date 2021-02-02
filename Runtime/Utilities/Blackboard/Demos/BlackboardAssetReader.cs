using UnityEngine;

namespace MuffinDev.Core.Demos
{

    ///<summary>
    /// 
    ///</summary>
    [AddComponentMenu("Muffin Dev/AI/Demos/Blackboard Asset Reader")]
    public class BlackboardAssetReader : MonoBehaviour
    {

        [SerializeField]
        private BlackboardAssetDemo m_BlackboardAsset = null;

        private void Start()
        {
            if(m_BlackboardAsset == null)
                return;

            Debug.Log("String: " + m_BlackboardAsset.GetValue<string>("String"));
            Debug.Log("Null: " + m_BlackboardAsset.GetValue<object>("Null"));
            Debug.Log("Vector3: " + m_BlackboardAsset.GetValue<Vector3>("Vector3"));
            Debug.Log("Int: " + m_BlackboardAsset.GetValue<int>("Int"));
            Debug.Log("Player data: " + m_BlackboardAsset.GetValue<BlackboardAssetDemo.PlayerData>("Player data"));
            Debug.Log("Unknown: " + m_BlackboardAsset.GetValue<GameObject>("Unknown"));
        }

    }

}