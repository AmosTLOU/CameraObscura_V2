using Characters;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace Gameplay {
    public class Clue : MonoBehaviour {

        [SerializeField] private GameEvent clueFoundEvent;
        [SerializeField] private Sprite clueImage;
        [SerializeField] private BaseCharacter characterBelongsTo;
        
        public Photo Photo { get; private set; }
        
        private bool _clueEnabled = false;
        private bool _found;
        

        private void Start(){
            
        }
        
        public void OnDetected(CameraClickEventData eData){
            if (!_clueEnabled || _found) return;
            _found = true;
            Photo = eData.Photo;
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