using Gameplay.Interactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Seed : MonoBehaviour, IColectReward
    {
        public int Reward => Config.SEED_REWARD;
        public GameObject GameObject => gameObject;
    }
}