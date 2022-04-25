using System;
using Characters;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace Gameplay {
    public class Clue : MonoBehaviour {

        [SerializeField] private GameEvent clueFoundEvent;
        [SerializeField] private Sprite clueImage;
        [SerializeField] private BaseCharacter characterBelongsTo;

        [SerializeField] private string customPath = "";
        
        public Photo photo { get; private set; }
        public Sprite Image => clueImage;
        
        private bool _clueEnabled = false;
        private bool _found;

        public void Reset() {
            _clueEnabled = true;
            _found = false;
        }
        
        public void OnDetected(CameraClickEventData eData){
            if (!_clueEnabled || _found) return;
            Log.Info($"Clue Detected = {gameObject.name}");
            _found = true;
            photo = eData.Photo;
            if (customPath != "") photo.FileName = Application.dataPath + customPath;
            clueFoundEvent.Raise(new ClueFoundEventData{
                clue = this,
                CharacterId = characterBelongsTo.Info.ID
            });
        }

        public void EnableDetection(bool active){
            _clueEnabled = active;
        }
    }
}