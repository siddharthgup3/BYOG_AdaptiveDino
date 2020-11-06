using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

namespace MyArchitecture
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent eventToBeRaised;
        public UnityEvent response;

        private void OnEnable()
        {
            eventToBeRaised.RegisterListener(this);
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }

        private void OnDisable()
        {
            eventToBeRaised.DeregisterListener(this);
        }
    }
}