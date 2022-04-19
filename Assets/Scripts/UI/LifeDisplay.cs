using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class LifeDisplay : MonoBehaviour {
        [SerializeField] private Sprite aliveSprite;
        [SerializeField] private Sprite deadSprite;
        [SerializeField] private Image displayImage;
        [SerializeField] private Image crossedOffSprite;
        // [SerializeField] private 
        
        private bool dead = false;

        private void Start() {
           // throw new NotImplementedException();
        }

        public void Dead() {
            displayImage.sprite = deadSprite;
            crossedOffSprite.gameObject.SetActive(true);
            crossedOffSprite.GetComponent<Animation>().Play();
        }
        
    }
}