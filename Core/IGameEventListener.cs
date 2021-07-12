using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Infovore.GameEventSystem
{
    public interface IGameEventListener<T>
    {
        void OnGameEventRaised(BaseGameEvent<T> gameEvent, T data);
    }
}
