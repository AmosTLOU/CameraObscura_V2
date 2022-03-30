using System.Collections;
using Core;
using EventSystem.Data;
using UnityEngine;
using Utility;

namespace Characters {
    public class Killer : BaseCharacter {
        [SerializeField] private EventSystem.GameEvent killVictimEvent;

        private Coroutine rampageRoutine;
        private Coroutine killingRoutine;
        private bool isHiding = true;
        
        public void OnNightStart(IGameEventData data){
            // killingRoutine = StartCoroutine(KillVictim());
        }

        #region Killer Actions
        public void StartRampage(IGameEventData data){
            Log.I("Killer about to go on rampage", Constants.TagTimeline);
            // Wait for some time and start for building 4;
            Log.I($"Rampage Day {data}");
            StartCoroutine(KillInBuilding1());
        }
        #region Killer Actions -> Day 1
        private void RampageDay1(IGameEventData data){
            Log.I("Killer about to go on rampage", Constants.TagTimeline);
            // Wait for some time and start for building 4;
            StartCoroutine(KillInBuilding1());
        }
        
        private IEnumerator KillInBuilding1(){
            // Move to the first location
            Log.D("Killer moving towards the first building", Constants.TagTimeline);
            
            // Play the animation and wait till it moves to the next location
            Log.D("");
            yield return null;
        }

        private IEnumerator KillInBuilding4(){
            // Move
            yield return null;
        }

        private IEnumerator KillInBuilding2(){
            // Move
            yield return null;
        }
        #endregion
        
        
        private IEnumerator KillVictim(){
            yield return new WaitForSeconds(3f);
            killVictimEvent.Raise(new VictimKilledEventData{victimId = "vic1"});
        }
        #endregion

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