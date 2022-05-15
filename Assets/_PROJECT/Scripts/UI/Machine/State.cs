using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Machine
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class State : MonoBehaviour
    {
        protected MachineState state;
        public MachineState StateType => state;
        private CanvasGroup canvasGroup;
        private Tweener canvasGroupTweener;
        protected bool instantShow;



        protected virtual void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            StateMachineManager.Instance.OnChangeStateAction += SetStateInstance;
            ConfigureMachineTransitions();
        }

        protected virtual void ConfigureMachineTransitions()
        {
            StateMachineManager.Instance.stateMachine.Configure(state)
                .OnEntry(() =>
                {
                    StateMachineManager.Instance.OnChangeState(state);
                });
            StateMachineManager.Instance.stateMachine.Configure(state).OnEntry(() =>
            {
                AppearOnScreen(instantShow);
            }).OnExit(() => HideOnScreen(instantShow));
        }

        public virtual void DoActionInState()
        {

        }

        public virtual void OnExecuteEscapeInput()
        {

        }

        public virtual void OnExecuteTabInput()
        {
        }

        protected virtual void OnDestroy()
        {
            if (StateMachineManager.Instance != null)
                StateMachineManager.Instance.OnChangeStateAction -= SetStateInstance;
        }

        private void SetStateInstance(MachineState _machineState)
        {
            if (_machineState == state)
            {
                StateMachineManager.Instance.previousState = StateMachineManager.Instance.currentState;
                StateMachineManager.Instance.currentState = this;
            }
        }

        public void InteractPanel()
        {
            canvasGroup.blocksRaycasts = true;
        }

        public void DisInteractPanel()
        {
            canvasGroup.blocksRaycasts = false;
        }

        public virtual void AppearOnScreen(bool _immediately = false)
        {
            canvasGroupTweener?.Kill();
            canvasGroupTweener = DOVirtual.Float(canvasGroup.alpha, 1f, _immediately ? 0f : Config.TIME_FOR_SHOW_GAME_PANEL, (value) => canvasGroup.alpha = value);
            canvasGroup.blocksRaycasts = true;
        }

        public virtual void HideOnScreen(bool _immediately = false)
        {
            canvasGroupTweener?.Kill();
            canvasGroupTweener = DOVirtual.Float(canvasGroup.alpha, 0f, _immediately ? 0f : Config.TIME_FOR_HIDE_GAME_PANEL, (value) =>
            {
                canvasGroup.alpha = value;
            });
            canvasGroup.blocksRaycasts = false;
        }
    }
}
