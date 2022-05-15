using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Interactions
{
    public class Teleport : MonoBehaviour, IGameInteraction
    {
        public Transform Point => transform;
        public float DistanceToInteraction => distance;
        [SerializeField]
        private float distance;
        [SerializeField]
        private Transform target;



        public void Interaction()
        {
            PlayerCharacter.Instance.MovePlayer(target.position);
        }
    }
}