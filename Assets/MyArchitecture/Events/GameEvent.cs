using System.Collections.Generic;
using UnityEngine;

namespace MyArchitecture
{
    [CreateAssetMenu(fileName = "new GameEvent", menuName = "SO/Events", order = 0)]
    public class GameEvent : BaseDataScriptableObject
    {
        private readonly List<GameEventListener> listeners = new List<GameEventListener>();

        public void RaiseEvent()
        {
            int listenerCount = listeners.Count;

            for (int i = listenerCount - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener newListener)
        {
            if (!listeners.Contains(newListener)) listeners.Add(newListener);
        }

        public void DeregisterListener(GameEventListener listenerToRemove)
        {
            if (listeners.Contains(listenerToRemove)) listeners.Remove(listenerToRemove);
        }
    }
}