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
        public UnityEvent<IGameEventData> response = new UnityEvent<IGameEventData>();

        private void OnEnable() {
            // Log.Info("event listener enable func");
            if (gameEvent == null) {
                Log.Warn("Game Event is ");
            }
            gameEvent?.RegisterListener(this);
        }

        private void OnDisable() {
            gameEvent.UnregisterListener(this);
        }
        
        public void OnEventRaised(IGameEventData data = null) {
            Log.Info($"{gameObject.name.Color("red")} handling event {gameEvent.name.Color("red")}", "EventListener");
            response.Invoke(data);
        }

        public GameEventListener Init(GameEvent gEvent){
            // Log.Info("event listener init func");
            gameEvent = gEvent;
            gameEvent.UnregisterListener(this);
            gameEvent.RegisterListener(this);
            return this;
        }
    }
}