using System.Collections.Generic;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

namespace World {
    public class WorldManager : SingletonBehaviour<WorldManager> {
        [SerializeField] private List<BaseHouse> houses = new List<BaseHouse>();
        
        private Dictionary<string, BaseHouse> _houses = new Dictionary<string, BaseHouse>();

        [SerializeField] private GameEvent gameStartEvent;
        [SerializeField] private GameEvent nightStartEvent;

        private void Start(){
            foreach (var house in houses){
                var id = house.Info.ID;
                if (_houses.ContainsKey(id)){
                    Log.Err($"Two houses have the same id {id}");
                }
                _houses[id] = house;
            }

            gameStartEvent.Raise(new MiniGameStateEventData{NewGame = true});
            // nightStartEvent.Raise(new NightStartEventData{dayNumber = 1});
        }
        
        public void ResetWorld() {}

        public BaseHouse GetHouse(string id){
            return _houses[id];
        }
    }
}