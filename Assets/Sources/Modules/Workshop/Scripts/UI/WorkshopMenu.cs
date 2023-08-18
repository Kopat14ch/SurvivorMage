using UnityEngine;

namespace Sources.Modules.Workshop.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    internal class WorkshopMenu : MonoBehaviour
    {
        [SerializeField] private WorkshopTrigger _trigger;
        
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            TurnOff();
        }

        private void OnEnable()
        {
            _trigger.PlayerEntered += TurnOn;
            _trigger.PlayerCameOut += TurnOff;
        }

        private void OnDisable()
        {
            _trigger.PlayerEntered -= TurnOn;
            _trigger.PlayerCameOut -= TurnOff;
        }

        private void TurnOn()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void TurnOff()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
