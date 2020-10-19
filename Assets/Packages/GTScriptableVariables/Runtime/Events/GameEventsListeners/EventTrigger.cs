using UnityEngine;

namespace GTVariable
{
    public class EventTrigger : MonoBehaviour
    {
        public GameEvent gameEvent;
        public UnityEventType onEvent;

        private void Awake()
        {
            if (onEvent == UnityEventType.Awake) gameEvent.Raise();
        }

        private void OnEnable()
        {
            if(onEvent == UnityEventType.OnEnable) gameEvent.Raise();
        }

        private void Start()
        {
            if (onEvent == UnityEventType.Start) gameEvent.Raise();
        }

        private void OnDisable()
        {
            if (onEvent == UnityEventType.OnDisable) gameEvent.Raise();
        }

        private void OnDestroy()
        {
            if (onEvent == UnityEventType.OnDestroy) gameEvent.Raise();
        }

    }
}