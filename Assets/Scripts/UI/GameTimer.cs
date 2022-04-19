using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class GameTimer : MonoBehaviour {
        [SerializeField] private Image fillImageRef;
        [SerializeField] private Sprite bgSprite;
        [SerializeField] private Sprite fillSprite;
        [SerializeField] private Sprite fillSpriteLow;
        [SerializeField][Range(0,1)] private float lowPercentage = 0.5f;
        
        private float _maxTime;
        private Action _onCompleteAction = () => { };
        private float _elapsedTime;
        private bool _running = false;

        public void StartTimer(float timeInSec, Action onComplete) {
            _running = true;
            _maxTime = timeInSec;
            _elapsedTime = 0f;
            _onCompleteAction = onComplete;
        }

        private void Update() {
            if (!_running) return;
            _elapsedTime += Time.deltaTime;
            var frac = _elapsedTime / _maxTime;
            fillImageRef.fillAmount = 1 - frac;
            if (frac < lowPercentage) fillImageRef.sprite = fillSpriteLow;
        }
        
        public void Stop() {
            _running = false;
        }
    }
}