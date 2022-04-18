using System;
using System.Collections.Generic;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace UI {
    public class HUDManager : SingletonBehaviour<HUDManager> {
        [SerializeField] private GameTimer timer;
        
        [SerializeField] private GameEvent timeoutEvent;
        [SerializeField] private GameEvent gameOverEvent;

        [SerializeField] private List<LifeDisplay> lives;

        private int _livesLost = 0;
        private void Start() {
            
        }
        public void StartTimer(float seconds, Action cb) {
            timer.StartTimer(seconds, cb);
        }

        public void StopTimer() {
            // TODO: Implementation
            timer.Stop();
        }

        public void OnEventVictimKilled(IGameEventData data) {
            if (!Utils.TryConvertVal(data, out VictimKilledEventData vData)) {
                return;
            }
            lives[_livesLost].Dead();
            if (++_livesLost >= lives.Count) {
                gameOverEvent.Raise();
            }
        }
    }
}