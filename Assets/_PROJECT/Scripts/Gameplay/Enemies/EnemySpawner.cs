using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private List<Transform> firstLevelSpawnPoints;
        [SerializeField]
        private List<Transform> secondLevelSpawnPoints;
        [SerializeField]
        private GameObject enemy;



        public List<Enemy> SpawnNormalMode()
        {
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < firstLevelSpawnPoints.Count; i++)
            {
                var enemyObject = Instantiate(enemy, firstLevelSpawnPoints[i].position, Quaternion.identity);
                enemies.Add(enemyObject.GetComponent<Enemy>());
            }
            return enemies;
        }

        public List<Enemy> SpawnHardMode()
        {
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < secondLevelSpawnPoints.Count; i++)
            {
                var enemyObject = Instantiate(enemy, secondLevelSpawnPoints[i].position, Quaternion.identity);
                enemies.Add(enemyObject.GetComponent<Enemy>());
            }
            return enemies;
        }
    }
}