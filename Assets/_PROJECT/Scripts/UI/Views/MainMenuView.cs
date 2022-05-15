using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UI.Machine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Views
{
    public class MainMenuView : State
    {
        protected override void Awake()
        {
            state = MachineState.MainMenuView;
            StateMachineManager.Instance.currentState = this;
            base.Awake();
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.GoMainGameTrigger, MachineState.MainGameView);
        }

        public void StartGame()
        {
            SceneManager.LoadScene(2);
        }

        private void OnLevelWasLoaded(int _level)
        {
            if(_level == 2)
            {
                StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.GoMainGameTrigger);
                GameEvents.PublishLoadGameScene();
            }
        }
    }
}
