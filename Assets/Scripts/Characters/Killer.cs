using System.Collections;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using Unity.Burst;
using UnityEngine;
using Utility;
using World;

namespace Characters {
    public class Killer : BaseCharacter {
        [SerializeField] private GameEvent victimKilledEvent;
        [SerializeField] private GameEvent killerDoneForDayEvent;
        
        

        private Coroutine rampageRoutine;
        private Coroutine killingRoutine;
        private bool isHiding = true;
        private bool _killing = false;
        private KillerPath _currentPath;

        public void OnNightStart(IGameEventData data){
            // killingRoutine = StartCoroutine(KillVictim());
        }

        #region Killer Actions
        public void OnEventPlayerWakeUp(IGameEventData data){
            Log.Info("Killer about to go on rampage", Constants.TagTimeline);
            // Wait for some time and start for building 4;
            
            Utils.TryConvertVal(data, out MiniGameStateEventData gsData);
            Log.Info($"Killer Rampage Day {gsData.DayNumber}");
            switch (gsData.DayNumber){
                case 1:
                    rampageRoutine = StartCoroutine(RampageDay1());
                    break;
                default:
                    Log.Err($"Action not found for Day {gsData.DayNumber}");
                    break;
            }
        }
        
        #region Killer Sequence -> Day 1
        
        private IEnumerator RampageDay1(){
            // Move to the first location
            Log.Debug("Killer moving towards the first building", Constants.TagTimeline);
            yield return new WaitForSeconds(2f);
            Log.Debug("Killer in position", Constants.TagTimeline);
            _killing = true;
            killingRoutine = StartCoroutine(KillBuilding1Victim());
            yield return killingRoutine;
            _killing = false;
            
            Log.Debug("Killer moving towards the second building", Constants.TagTimeline);
            yield return new WaitForSeconds(2f);
            Log.Debug("Killer in position", Constants.TagTimeline);
            _killing = true;
            killingRoutine = StartCoroutine(KillInBuilding4());
            yield return killingRoutine;
            _killing = false;
            
            Log.Debug("Killer moving towards the third building", Constants.TagTimeline);
            yield return new WaitForSeconds(2f);
            Log.Debug("Killer in position", Constants.TagTimeline);
            _killing = true;
            killingRoutine = StartCoroutine(KillInBuilding2());
            yield return killingRoutine;
            _killing = false;

            killerDoneForDayEvent.Raise();
        }

        private IEnumerator KillBuilding1Victim(){
            yield return new WaitForSeconds(3f);
            
            // yield return
            // killVictimEvent.Raise(new VictimKilledEventData{victimId = WorldManager.Instance.GetHouse()});
            victimKilledEvent.Raise(new VictimKilledEventData{victimId = "vic1"});
            Log.Debug("Victim=vic1 Killed, Restart Game!", Constants.TagTimeline);
            // StopCoroutine(rampageRoutine);
        }

        private IEnumerator KillInBuilding4(){
            yield return new WaitForSeconds(3f);
            
            // killVictimEvent.Raise(new VictimKilledEventData{victimId = WorldManager.Instance.GetHouse()});
            victimKilledEvent.Raise(new VictimKilledEventData{victimId = "vic2"});
            Log.Debug("Victim=vic2 Killed, Restart Game! vic2", Constants.TagTimeline);
            // StopCoroutine(rampageRoutine);
        }

        private IEnumerator KillInBuilding2(){
            yield return new WaitForSeconds(3f);
            
            // killVictimEvent.Raise(new VictimKilledEventData{victimId = WorldManager.Instance.GetHouse()});
            victimKilledEvent.Raise(new VictimKilledEventData{victimId = "vic3"});
            Log.Debug("Victim=vic3 Killed, Restart Game!", Constants.TagTimeline);
            // StopCoroutine(rampageRoutine);
        }
        #endregion

        #region Killer Sequence -> Day 2
        #endregion
        
        #region Killer Sequence -> Day 3
        #endregion
        
        #endregion

        public void OnEventCameraFlash(IGameEventData d){
            Utils.TryConvertVal(d, out CameraFlashEventData data);
            if (!_killing){
                Log.Debug("Camera Flash during non-killing phase", Constants.TagTimeline);
                return;
            }
            
            Log.Info("Conditional checking camera flash data at right place");
            StopCoroutine(killingRoutine);
            _killing = false;
            Log.Info("Stopped Killing, Killer Running Away", Constants.TagTimeline);
        }

        public void Interrupted(){
            Log.Info("Checking for interrupted");
            
            // Add check
            Log.Info("Successfully interrupted");
        }

        public void SetKillPathAndMove(KillerPath path){
            _currentPath = path;
            StartCoroutine(StartKilling());
        }

        private IEnumerator StartKilling(){
            yield return null;
        }
    }
}