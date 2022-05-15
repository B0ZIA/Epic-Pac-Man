using UnityEngine;

namespace Gameplay.Interactive
{
    public interface IColectReward
    {
        public int Reward { get; }
        public GameObject GameObject { get; }
    }
}
