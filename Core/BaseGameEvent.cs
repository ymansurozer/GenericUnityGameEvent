using System.Collections.Generic;
using UnityEngine;

namespace Infovore.GameEventSystem
{
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        [SerializeField] bool debugging;
        
        // Config parameters
        public string title;
        public string description;

        // State variables
        List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

        public void Raise(T data)
        {
            if(debugging) Debug.Log($"Game event named '{title}' is raised.");
            
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnGameEventRaised(this, data);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (listeners.Contains(listener) == false)
                listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (listeners.Contains(listener) == true)
                listeners.Remove(listener);
        }
    }
}
