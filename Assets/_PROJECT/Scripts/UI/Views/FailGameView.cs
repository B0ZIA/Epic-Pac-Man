using System.Collections;
using System.Collections.Generic;
using UI.Machine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Views
{
    public class FailGameView : State
    {
        protected override void Awake()
        {
            state = MachineState.FailGameView;
            StateMachineManager.Instance.currentState = this;
            base.Awake();
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.GoMainMenuTrigger, MachineState.MainMenuView);
        }

        public void Restart()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.GoMainMenuTrigger);
            SceneManager.LoadScene(1);
        }
    }
}
