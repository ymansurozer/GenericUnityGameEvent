using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Infovore.GameEventSystem
{
    public abstract class BaseGameEventListener<T> : MonoBehaviour, IGameEventListener<T>
    {
        [SerializeField]
        List<EventResponsePair> eventsAndResponses = new List<EventResponsePair>();

        void OnEnable()
        {
            if (eventsAndResponses.Count == 0)
            {
                Debug.LogWarning("A game event listener on '" + name +
                                 "' does not have any gameEvent assigned. Cannot register so will halt and return.");
                return;
            }

            foreach (EventResponsePair eventResponsePair in eventsAndResponses)
                eventResponsePair.gameEvent.RegisterListener(this);
        }

        void OnDisable()
        {
            if (eventsAndResponses.Count == 0)
            {
                Debug.LogWarning("A game event listener on '" + name +
                                 "' does not have gameEvent assigned. Cannot unregister so will halt and return.");
                return;
            }

            foreach (EventResponsePair eventResponsePair in eventsAndResponses)
                eventResponsePair.gameEvent.UnregisterListener(this);
        }

        public void OnGameEventRaised(BaseGameEvent<T> gameEvent, T data)
        {
            eventsAndResponses.Find(f => f.gameEvent == gameEvent).unityEventResponse?.Invoke(data);
        }
        
        [System.Serializable]
        public class EventResponsePair
        {
            [SerializeField] [Tooltip("Game event to register with.")] 
            public BaseGameEvent<T> gameEvent;
            
            [Space] 
        
            [SerializeField] [Tooltip("Response to invoke when the game event is raised.")]
            public UnityEvent<T> unityEventResponse;
        }
    }
}
