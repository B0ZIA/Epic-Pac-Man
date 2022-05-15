using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Interactive
{
    public class Door : MonoBehaviour
    {
        [SerializeField]
        private List<Characters> charactersAllowedToOpen;
        [SerializeField]
        private Transform doorElement;
        private Tweener tweener;
        private List<ICharacter> charactersInArea = new List<ICharacter>();
        private Action OnDetectMovement;
        private bool isOpen = false;



        private void Awake()
        {
            OnDetectMovement += RefreshDoorState;
            GameEvents.OnHurtPlayer += ResetState;
        }

        private void OnDestroy()
        {
            OnDetectMovement -= RefreshDoorState;
            GameEvents.OnHurtPlayer -= ResetState;
        }

        private void RefreshDoorState()
        {
            if (charactersInArea.Count > 0)
                Open();
            else
                Close();
        }

        private void ResetState()
        {
            charactersInArea.Clear();
            Close();
        }

        private void Open()
        {
            if (!isOpen)
            {
                tweener?.Kill();
                tweener = doorElement.DOMoveY(5.5f, 0.1f);
                isOpen = true;
            }
        }

        private void Close()
        {
            if (isOpen)
            {
                tweener?.Kill();
                tweener = doorElement.DOMoveY(1.25f, 0.1f);
                isOpen = false;
            }
        }

        private void OnTriggerEnter(Collider _other)
        {
            _other.TryGetComponent(out ICharacter _visible);

            if (_visible != null && charactersAllowedToOpen.Any(p => p == _visible.CharacterType))
            {
                charactersInArea.Add(_visible);
                OnDetectMovement?.Invoke();
            }
        }

        private void OnTriggerExit(Collider _other)
        {
            _other.TryGetComponent(out ICharacter _visible);

            if (_visible != null)
            {
                if(charactersInArea.Find(p => p == _visible) != null)
                charactersInArea.Remove(_visible);
                OnDetectMovement?.Invoke();
            }
        }
    }
}