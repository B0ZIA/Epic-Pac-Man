using Gameplay.Interactions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField]
        private Transform centerOfMass;
        private List<IGameInteraction> interactionObjects = new List<IGameInteraction>();



        private void Awake()
        {
            var interactions = FindObjectsOfType<MonoBehaviour>().OfType<IGameInteraction>();
            foreach (var interaction in interactions)
            {
                interactionObjects.Add(interaction);
            }
        }

        private void Update()
        {
            foreach (var interactionObject in interactionObjects)
            {
                if (Vector3.Distance(centerOfMass.position, interactionObject.Point.position) <= interactionObject.DistanceToInteraction)
                    interactionObject.Interaction();
            }
        }
    }
}