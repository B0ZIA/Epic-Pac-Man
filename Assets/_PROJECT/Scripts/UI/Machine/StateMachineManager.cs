using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stateless;
using Pattern;
using System;

namespace UI.Machine
{
    public class StateMachineManager : Singleton<StateMachineManager>
    {
        public readonly StateMachine<MachineState, MachineTrigger> stateMachine = new StateMachine<MachineState, MachineTrigger>(MachineState.Initialize);

        public State currentState;
        public State previousState;
        public event Action<MachineState> OnChangeStateAction;
        public bool IsUiHidden { get; set; }
        public bool BlockControls { get; set; }



        private void Awake()
        {
            AssignInstance(this);
            DontDestroyOnLoad(gameObject);
        }

        public void SetUiVisibility(bool _immediately = false)
        {
            if (!IsUiHidden)
            {
                currentState.HideOnScreen(_immediately);
            }
            else
            {
                currentState.AppearOnScreen(_immediately);
            }
            IsUiHidden = !IsUiHidden;
        }

        public void OnChangeState(MachineState _stateToSet)
        {
            OnChangeStateAction?.Invoke(_stateToSet);
        }

        public void Update()
        {
            if (BlockControls) return;

            if (currentState != null)
            {
                currentState.DoActionInState();
            }
        }
    }
}
