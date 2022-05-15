using Gameplay.Interactive;
using System;

namespace Gameplay
{
    public static class GameEvents
    {
        public static event Action OnLoadGameScene;
        public static event Action OnStartGame;
        public static event Action OnHurtPlayer;
        public static event Action OnKillPlayer;
        public static event Action<IColectReward> OnCollectSeed;
        public static event Action OnLevelUp;
        public static event Action<int> OnChangeLivesCount;
        public static event Action OnWinGame;



        public static void PublishLoadGameScene()
        {
            OnLoadGameScene?.Invoke();
        }

        public static void PublishStartGame()
        {
            OnStartGame?.Invoke();
        }

        public static void PublishHurtPlayer()
        {
            OnHurtPlayer?.Invoke();
        }

        public static void PublishKillPlayer()
        {
            OnKillPlayer?.Invoke();
        }

        public static void PublishCollectReward(IColectReward _reward)
        {
            OnCollectSeed?.Invoke(_reward);
        }

        public static void PublishLevelUp()
        {
            OnLevelUp?.Invoke();
        }

        public static void PublishChangeLivesCount(int _newLiveCount)
        {
            OnChangeLivesCount?.Invoke(_newLiveCount);
        }

        public static void PublishWinGame()
        {
            OnWinGame?.Invoke();
        }
    }
}