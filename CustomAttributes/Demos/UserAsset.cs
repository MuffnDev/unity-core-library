using UnityEngine;

namespace MuffinDev
{

    [System.Serializable]
    public class Test
    {
        public int value;
        public string[] tags;
    }

	///<summary>
	/// 
	///</summary>
	[CreateAssetMenu(fileName ="NewUserAsset", menuName = "Muffin Dev/Demos/User Asset")]
	public class UserAsset : ScriptableObject
	{

        public string username;
        public int age;
        public Vector3[] positions;
        public Test[] test;
        public string testLastField;

	}

}