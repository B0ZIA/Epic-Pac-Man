using Gameplay.Player;
using Pattern;
using System.Collections;
using System.Collections.Generic;
using UI.Machine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private Transform spawnPoint;



        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.LoadScene(1);
        }

        public void ResetPlayerPosition()
        {
            PlayerCharacter.Instance.MovePlayer(spawnPoint.position);
        }
    }
    public enum Level
    {
        First,
        Second
    }
}
