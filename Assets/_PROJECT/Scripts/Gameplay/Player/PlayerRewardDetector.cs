using Gameplay.Interactions;
using Gameplay.Interactive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerRewardDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider _other)
        {
            _other.TryGetComponent(out IColectReward _reward);

            if (_reward != null)
            {
                GameEvents.PublishCollectReward(_reward);
            }
        }
    }
}