using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        private EnemySpawner spawner;
        private List<Enemy> enemies = new List<Enemy>();
        [SerializeField]
        private EnemyContainer container;



        private void Awake()
        {
            spawner = GetComponent<EnemySpawner>();
            GameEvents.OnStartGame += OnStartGame;
            GameEvents.OnHurtPlayer += OnHurtPlayer;
            GameEvents.OnLevelUp += OnLevelUp;
            GameEvents.OnKillPlayer += OnKillPlayer;
            GameEvents.OnWinGame += OnWinGame;
        }

        private void OnDestroy()
        {
            GameEvents.OnStartGame -= OnStartGame;
            GameEvents.OnHurtPlayer -= OnHurtPlayer;
            GameEvents.OnLevelUp -= OnLevelUp;
            GameEvents.OnKillPlayer -= OnKillPlayer;
            GameEvents.OnWinGame -= OnWinGame;
        }

        private void OnStartGame()
        {
            SpawnEnemyByLevel(Level.First);
            ResetEnemiesPosition();
        }

        private void OnHurtPlayer()
        {
            ResetEnemiesPosition();
        }

        private void OnLevelUp()
        {
            SpawnEnemyByLevel(Level.Second);
        }

        private void OnKillPlayer()
        {
            KillAllEnemies();
        }

        private void OnWinGame()
        {
            KillAllEnemies();
        }

        public void SpawnEnemyByLevel(Level _level)
        {
            KillAllEnemies();
            switch (_level)
            {
                case Level.First:
                    var normalEnemies = spawner.SpawnNormalMode();
                    enemies.AddRange(normalEnemies);
                    EnemyData normalEnemiesData = container.GetEnemyData(EnemyContainer.EnemyType.Normal);
                    enemies.ForEach(e => e.Load(normalEnemiesData));
                    break;
                case Level.Second:
                    var hardEnemies = spawner.SpawnHardMode();
                    enemies.AddRange(hardEnemies);
                    EnemyData hardEnemiesData = container.GetEnemyData(EnemyContainer.EnemyType.Hard);
                    enemies.ForEach(e => e.Load(hardEnemiesData));
                    break;
            }
        }

        public void KillAllEnemies()
        {
            foreach (var enemy in enemies)
                Destroy(enemy.gameObject);
            enemies.Clear();
        }

        public void ResetEnemiesPosition()
        {
            enemies.ForEach(e => e.ResetPosition());
        }
    }
}