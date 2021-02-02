using UnityEngine;

namespace MuffinDev.Core.Demos
{

    ///<summary>
    /// Example usage of a Blackboard Asset. This one sets some values when loaded.
    ///</summary>
    [CreateAssetMenu(fileName = "NewBlackboardAssetDemo", menuName = "Muffin Dev/AI/Demos/Blackboard Asset Demo")]
    public class BlackboardAssetDemo : BlackboardAsset
    {

        [System.Serializable]
        public class PlayerData
        {
            public string name;
            public int score;
        }

        private void OnEnable()
        {
            //SetValue("String", "Hello World!");
            //SetValue("Null", null);
            //SetValue("Vector3", new Vector3(0, 1, 2));
            //SetValue("Int", 5);
            //SetValue("Player data", new PlayerData { name = "AAA", score = 12160 });
        }

    }

}