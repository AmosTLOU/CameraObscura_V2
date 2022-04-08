using System;
using System.Collections.Generic;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;
using World.Data;

namespace World {
    public class BaseHouse : MonoBehaviour {
        [Header("House information object")] 
        [SerializeField] protected HouseInfo info;
        [SerializeField] protected bool suspect;
        [SerializeField] protected List<RoomWindow> windows = new List<RoomWindow>();

        public HouseInfo Info => info;

        public void OnEventGameStart(IGameEventData eventData){
            UpdateWindowState(true);
        }

        public void OnEventBeatStart(IGameEventData eventData){
            if (!Utils.TryConvertVal(eventData, out BeatStartEventData data)){
                Log.Err("Error Converting Type");
                return;
            };
            if (!suspect) return;
            switch (data.Beat){
                case GameBeat.KillingAct1:
                    UpdateWindowState(false);
                    break;
                case GameBeat.Suspect:
                    UpdateWindowState(true);
                    break;
                default:
                    return;
            }
        }

        private void UpdateWindowState(bool open){
            // Todo: Add Implementation
            foreach (var window in windows){
                window.UpdateState(open);
            }
        }
    }
}