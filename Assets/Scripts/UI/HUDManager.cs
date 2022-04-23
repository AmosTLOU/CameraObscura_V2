using System;
using System.Collections.Generic;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HUDManager : SingletonBehaviour<HUDManager> {
        [SerializeField] private GameTimer timer;
        
        [SerializeField] private GameEvent timeoutEvent;
        [SerializeField] private GameEvent gameOverEvent;

        [SerializeField] private GameObject livesParent;
        [SerializeField] private LifeDisplay lifePrefab;
        [SerializeField] private Slider zoomLevelSlider;

        [SerializeField] private int numLives;

        private List<LifeDisplay> _lives = new List<LifeDisplay>(); 
        private int _livesLost = 0;
        private void Start() {
            for (var i = 0; i < numLives; i++){
                var life = Instantiate(lifePrefab, livesParent.transform);
                _lives.Add(life);
            }
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
            _lives[_livesLost].Dead();
            if (++_livesLost >= _lives.Count) {
                gameOverEvent.Raise();
            }
        }

        private void Update()
        {
            zoomLevelSlider.value = (Camera.main.fieldOfView - CameraControl.Instance.MinFOV) / (CameraControl.Instance.MaxFOV - CameraControl.Instance.MinFOV);
        }
    }
}