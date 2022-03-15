using System.Collections;
using Core;
using EventSystem.Data;
using UnityEngine;
using Utility;

namespace Characters {
    public class Killer : BaseCharacter {
        [SerializeField] private EventSystem.GameEvent killVictimEvent;

        private Coroutine killingRoutine;
        
        public void OnNightStart(IGameEventData data){
            killingRoutine = StartCoroutine(KillVictim());
        }

        private IEnumerator KillVictim(){
            yield return new WaitForSeconds(3f);
            killVictimEvent.Raise(new VictimKilledEventData{victimId = "vic1"});
        }

        public void OnCameraFlash(IGameEventData d){
            Utils.TryConvertVal(d, out CameraFlashEventData data);
            Log.I("Checking for flashed at right place");
            StopCoroutine(killingRoutine);
            Log.I("Stopped Killing, Running Away".Color("green"));
        }

        public void Interrupted(){
            Log.I("Checking for interrupted");
            
            // Add check
            Log.I("Successfully interrupted");
        }
    }
}