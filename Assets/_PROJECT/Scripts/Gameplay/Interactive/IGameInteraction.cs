using UnityEngine;

namespace Gameplay.Interactions
{

    public interface IGameInteraction
    {
        public float DistanceToInteraction { get; }
        public Transform Point { get; }

        public void Interaction();
    }
}
