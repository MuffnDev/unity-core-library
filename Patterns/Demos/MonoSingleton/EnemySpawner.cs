using UnityEngine;

namespace MuffinDev.Core.Demos
{

    [AddComponentMenu("Muffin Dev/Demos/Enemy Spawner")]
    public class EnemySpawner : MonoBehaviour
    {

        [SerializeField, Min(1)]
        private int m_NumberOfEnemies = 3;

        private void Awake()
        {
            for(int i = 0; i < m_NumberOfEnemies; i++)
            {
                SpawnAt(transform.position + Vector3.right * i);
            }
        }

        public void SpawnAt(Vector3 _Position)
        {
            int count = WorldSettings.Instance.EnemyPrefabs.Length;
            if (count > 0)
            {
                int randomIndex = Random.Range(0, count);
                Instantiate(WorldSettings.Instance.EnemyPrefabs[randomIndex], _Position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("No enemy prefab set in World Settings array.");
            }
        }

    }

}