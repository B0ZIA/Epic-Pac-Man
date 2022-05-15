using Gameplay.Interactions;
using Gameplay.Interactive;
using Gameplay.Player;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, ICharacter
    {
        public Characters CharacterType => Characters.Enemy;

        private NavMeshAgent agent;
        private Vector3 startPosition;
        private Transform playerTransform;



        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            startPosition = transform.position;
        }

        private void Start()
        {
            playerTransform = PlayerCharacter.Instance.transform;
        }

        private void LateUpdate()
        {
            agent.destination = playerTransform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent(out ICharacter visible);

            if (visible != null && visible.CharacterType == Characters.Player)
            {
                GameEvents.PublishHurtPlayer();
            }
        }

        public void Load(EnemyData _data)
        {
            agent.speed = _data.speed;
            agent.angularSpeed = _data.angularSpeed;
        }

        public void ResetPosition()
        {
            transform.position = startPosition;
        }
    }
}