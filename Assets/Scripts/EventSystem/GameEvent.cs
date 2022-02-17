﻿using System.Collections.Generic;
using Core;
using EventSystem.Data;
using UnityEngine;

namespace EventSystem {
    [CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent", order = 0)]
    public class GameEvent : ScriptableObject {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListener> _eventListeners = 
            new List<GameEventListener>();

        public void Raise(IGameEventData data = null) {
            Log.I($"Event={name}", "EventRaised");
            for(int i = _eventListeners.Count -1; i >= 0; i--)
                _eventListeners[i].OnEventRaised(data);
        }

        public void RegisterListener(GameEventListener listener) {
            if (!_eventListeners.Contains(listener))
                _eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener) {
            if (_eventListeners.Contains(listener))
                _eventListeners.Remove(listener);
        }
    }
}