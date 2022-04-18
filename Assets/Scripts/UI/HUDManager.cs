using System;
using Core;
using EventSystem;
using UnityEngine;

namespace UI {
    public class HUDManager : SingletonBehaviour<HUDManager> {
        [SerializeField] private int numLives = 3;
        [SerializeField] private GameTimer timer;
        
        [SerializeField] private GameEvent timeoutEvent;

        public void StartTimer(float seconds, Action cb) {
            timer.StartTimer(seconds, cb);
        }

        public void StopTimer() {
            // TODO: Implementation
            timer.Stop();
        }
    }
}