using Core;
using EventSystem.Data;
using UnityEngine;
using UnityEngine.Events;
using Utility;

namespace EventSystem {
    public class GameEventListener : MonoBehaviour {
        [Tooltip("Event that is being listened to")] 
        public GameEvent gameEvent;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<IGameEventData> response;

        private void OnEnable() {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable() {
            gameEvent.UnregisterListener(this);
        }
        
        public void OnEventRaised(IGameEventData data = null) {
            Log.Info($"{gameObject.name.Color("red")} handling event {gameEvent.name.Color("red")}", "EventListener");
            response.Invoke(data);
        }
    }
}