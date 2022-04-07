using EventSystem.Data;
using UnityEngine;
using World.Data;

namespace World {
    public class BaseHouse : MonoBehaviour {
        [Header("House information object")] 
        [SerializeField] private HouseInfo info;
        
        public HouseInfo Info => info;

        public void OnEventGameStart(IGameEventData eventData){
            UpdateWindowState(true);
        }

        private void UpdateWindowState(bool open){
            // Todo: Add Implementation
        }
    }
}