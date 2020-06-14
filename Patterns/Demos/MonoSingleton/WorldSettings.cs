using UnityEngine;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/World Settings")]
    public class WorldSettings : MonoSingleton<WorldSettings>
    {

        [SerializeField]
        private GameObject[] m_EnemyPrefabs = { };

        public GameObject[] EnemyPrefabs
        {
            get { return m_EnemyPrefabs; }
        }

        protected override void MonoSingletonOnInit()
        {
            Debug.LogFormat("The object {0} is now defined as only WorldSettings singleton instance.", name);
        }

        protected override void MonoSingletonRejected()
        {
            Debug.LogWarning(string.Format("There's more than 1 instance of WorldSettings in the scene. The object {0} is rejected.", name));
        }

        protected override void MonoSingletonOnAwake()
        {
            Debug.LogFormat("The object {0} awakes.", name);
        }

        protected override void MonoSingletonOnDestroy()
        {
            Debug.LogFormat("The object {0} is destroyed.", name);
        }

    }

}