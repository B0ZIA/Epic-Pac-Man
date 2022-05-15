using Gameplay.Interactive;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Machine;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class SeedsManager : MonoBehaviour
    {
        public int CollectedSeedsCount { get; private set; }
        public int Score { get; private set; }

        private List<IColectReward> seeds = new List<IColectReward>();
        private int maxSeeds;
        private bool isFirstLevelPassed;



        private void Awake()
        {
            GameEvents.OnLoadGameScene += OnLoadGameScene;
            GameEvents.OnCollectSeed += OnCollectSeed;
            GameEvents.OnLevelUp += OnLevelUp;
            GameEvents.OnStartGame += OnStartGame;
            GameEvents.OnKillPlayer += OnKillPlayer;
        }

        private void OnDestroy()
        {
            GameEvents.OnLoadGameScene -= OnLoadGameScene;
            GameEvents.OnCollectSeed -= OnCollectSeed;
            GameEvents.OnLevelUp -= OnLevelUp;
            GameEvents.OnStartGame -= OnStartGame;
            GameEvents.OnKillPlayer -= OnKillPlayer;
        }

        public void OnLoadGameScene()
        {
            seeds.Clear();
            var seedsOnLevel = FindObjectsOfType<Seed>();
            foreach (var seed in seedsOnLevel)
            {
                seeds.Add(seed);
            }
            maxSeeds = seeds.Count;
        }

        private void OnCollectSeed(IColectReward _seed)
        {
            _seed.GameObject.SetActive(false);
            Score += _seed.Reward;

            CollectedSeedsCount++;
            if (CollectedSeedsCount >= maxSeeds)
            {
                GameEvents.PublishLevelUp();
                if (isFirstLevelPassed)
                {
                    GameEvents.PublishWinGame();
                    return;
                }

                isFirstLevelPassed = true;
            }
        }

        private void OnLevelUp()
        {
            CollectedSeedsCount = 0;
            TurnOnAllSeeds();
        }

        private void TurnOnAllSeeds()
        {
            seeds.ForEach(p => p.GameObject.SetActive(true));
        }

        private void OnStartGame()
        {
            CollectedSeedsCount = 0;
            isFirstLevelPassed = false;
        }

        private void OnKillPlayer()
        {
            TurnOnAllSeeds();
        }
    }
}
