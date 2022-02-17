using Core;
using EventSystem.Data;
using UnityEngine;
using UnityEngine.Events;

namespace EventSystem {
    public class GameEventListener : MonoBehaviour {
        [Tooltip("Event to register with.")] 
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
            Log.I($"GameObject {gameObject.name} handling event {gameEvent.name}", "EVENT");
            response.Invoke(data);
        }
    }
}