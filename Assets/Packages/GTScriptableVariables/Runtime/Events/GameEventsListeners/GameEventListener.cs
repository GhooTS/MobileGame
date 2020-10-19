using UnityEngine;
using UnityEngine.Events;

namespace GTVariable
{
    public class GameEventListener : Listener
    {
        public GameEvent[] gameEvents;
        public UnityEvent response;

        public void OnEventRised()
        {
            response?.Invoke();
        }

        private void OnEnable()
        {
            RegisterToAll();
        }

        private void OnDisable()
        {
            UnregisterFromAll();
        }

        private void RegisterToAll()
        {
            foreach (var gameEvent in gameEvents)
            {
                gameEvent.RegisterListener(this);
            }
        }


        private void UnregisterFromAll()
        {
            foreach (var gameEvent in gameEvents)
            {
                gameEvent.UnRegisterListener(this);
            }
        }
    }
}