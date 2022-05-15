using Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Gameplay.Interactive;

namespace Gameplay.Player
{
    public class PlayerCharacter : Singleton<PlayerCharacter>, ICharacter
    {
        public Characters CharacterType => Characters.Player;
        public int Lives { get; private set; }

        private StarterAssetsInputs starterAssetsInputs;
        private CharacterController controller;
        private ThirdPersonController thirdPersonController;


        private void Awake()
        {
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            controller = GetComponent<CharacterController>();
            thirdPersonController = GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = false;

            GameEvents.OnHurtPlayer += HurtPlayer;
            GameEvents.OnStartGame += PreparePlayer;
            GameEvents.OnKillPlayer += KillPlayer;
            GameEvents.OnLevelUp += OnLevelUp;
            GameEvents.OnWinGame += OnWinGame;
        }

        private void OnDestroy()
        {
            GameEvents.OnHurtPlayer -= HurtPlayer;
            GameEvents.OnStartGame -= PreparePlayer;
            GameEvents.OnKillPlayer -= KillPlayer;
            GameEvents.OnLevelUp -= OnLevelUp;
            GameEvents.OnWinGame -= OnWinGame;
        }

        private void HurtPlayer()
        {
            Lives--;
            GameEvents.PublishChangeLivesCount(Lives);
            GameManager.Instance.ResetPlayerPosition();

            if (Lives <= 0)
                GameEvents.PublishKillPlayer();
        }

        private void PreparePlayer()
        {
            SetPlayerLocked(true);
            transform.gameObject.SetActive(true);
            Lives = Config.MAX_PLAYER_LIVES;
            GameEvents.PublishChangeLivesCount(Lives);
        }

        private void KillPlayer()
        {
            SetPlayerLocked(false);
            transform.gameObject.SetActive(false);
        }

        private void OnLevelUp()
        {
            GameManager.Instance.ResetPlayerPosition();
        }

        private void OnWinGame()
        {
            KillPlayer();
        }

        private void SetPlayerLocked(bool _value)
        {
            Cursor.lockState = _value ? CursorLockMode.Locked : CursorLockMode.None;
            starterAssetsInputs.cursorInputForLook = _value;
            thirdPersonController.enabled = _value;
        }

        public void MovePlayer(Vector3 _pos)
        {
            controller.enabled = false;
            transform.position = _pos;
            controller.enabled = true;
        }
    }
}