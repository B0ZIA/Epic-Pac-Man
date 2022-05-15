using Gameplay;
using System.Collections;
using System.Collections.Generic;
using UI.Machine;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MainGameView : State
    {
        [SerializeField]
        private Text livesCount;

        protected override void Awake()
        {
            state = MachineState.MainGameView;
            StateMachineManager.Instance.currentState = this;
            base.Awake();

            GameEvents.OnChangeLivesCount += ChangeLivesCount;
            GameEvents.OnWinGame += Win;
            GameEvents.OnKillPlayer += Fail;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            GameEvents.OnChangeLivesCount -= ChangeLivesCount;
            GameEvents.OnWinGame -= Win;
            GameEvents.OnKillPlayer -= Fail;
        }

        protected override void ConfigureMachineTransitions()
        {
            base.ConfigureMachineTransitions();
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.GoWinGameTrigger, MachineState.WinGameView).
                OnEntry(() => GameEvents.PublishStartGame());
            StateMachineManager.Instance.stateMachine.Configure(state)
                .Permit(MachineTrigger.GoFailGameTrigger, MachineState.FailGameView);
        }

        public override void DoActionInState()
        {
            base.DoActionInState();
        }

        public void Win()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.GoWinGameTrigger);
        }

        public void Fail()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.GoFailGameTrigger);
        }

        private void ChangeLivesCount(int _newLivesCount)
        {
            livesCount.text = "Lives: " + _newLivesCount;
        }
    }
}