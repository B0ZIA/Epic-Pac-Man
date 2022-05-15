using System.Collections;
using System.Collections.Generic;
using UI.Machine;
using UnityEngine;

namespace UI.Views
{
    public class Initialize : State
    {
        protected override void Awake()
        {
            state = MachineState.Initialize;
            base.Awake();
        }

        private void Start()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.GoMainMenuTrigger);
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.GoMainMenuTrigger, MachineState.MainMenuView);
        }
    }
}