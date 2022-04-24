using System.Collections;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using UnityEngine;

namespace Characters {
    public class Killer : BaseCharacter {
        [SerializeField] private GameEvent victimKilledEvent;
        [SerializeField] private GameEvent killerRanAwayEvent;

        private Coroutine rampageRoutine;
        private Coroutine killingRoutine;
        private bool isHiding = true;
        private bool _killing = false;
        [SerializeField] private KillerPath _currentPath;

        private PathFollower _follower;
        private KillerState _state;
        private Coroutine _killingRoutine;

        private void Start(){
            _follower = GetComponent<PathFollower>();
            // gameObject.SetActive(false);
            // _follower.pathCreator = _currentPath.sneakPath;
        }
        
        public void OnNightStart(IGameEventData data){
            // killingRoutine = StartCoroutine(KillVictim());
        }

        public void OnEventCameraFlash(IGameEventData d){
            Utils.TryConvertVal(d, out CameraClickEventData data);
            if (!(_state == KillerState.Sneak || _state == KillerState.Killing)){
                Log.Debug("Camera Flash during non-killing phase", Constants.TagTimeline);
                return;
            }

            Log.Info("Conditional checking camera flash data at right place");
            if (killingRoutine != null){
                StopCoroutine(killingRoutine);
                killingRoutine = null;
                StartCoroutine(RunAway());
            }
            // _killing = false;
            // Log.Info("Stopped Killing, Killer Running Away", Constants.TagTimeline);
        }

        public void Interrupted(){
            Log.Info("Checking for interrupted");
            // Add check
            
            if (_state == KillerState.Sneak){
                // StopCoroutine(killingRoutine);
                killingRoutine = null;
                _follower.reachedEnd.RemoveAllListeners();
                StartCoroutine(RunAway());
                Log.Info("Successfully interrupted");
            }
        }

        public void SetKillPathAndMove(KillerPath path){
            if (_state != KillerState.Idle){
                Log.Err($"Killer in unexpected state; Current={_state}, Expected = {KillerState.Idle}");
                return;
            }
            Log.Info("killer sneaking in!", Constants.TagTimeline);
            gameObject.SetActive(true);
            _state = KillerState.Sneak;
            _animator.SetBool("idle", false);
            _currentPath = path;
            _follower.pathCreator = _currentPath.sneakPath;
            _follower.reachedEnd.AddListener(() => {
                _killingRoutine = StartCoroutine(StartKilling());
            });
        }

        private IEnumerator StartKilling(){
            _state = KillerState.Killing;
            yield return null;
            _follower.reachedEnd.RemoveAllListeners();
            _animator.SetTrigger("kill");
            
            // Change to animation end callbacks
            //yield return new WaitForSeconds(1f);
            _killingRoutine = null;
            _currentPath.victim.Kill();
            Log.Info("killer killed the victim", Constants.TagTimeline);
            victimKilledEvent.Raise(new VictimKilledEventData{victimId = _currentPath.victim.Info.ID});
            _state = KillerState.RunningAway;
            yield return new WaitForSeconds(2f);
            StartCoroutine(RunAway());
            yield return null;
        }

        private IEnumerator RunAway(){
            Log.Info("killer running away", Constants.TagTimeline);
            _follower.UpdatePath(_currentPath.runAwayPath);
            _animator.SetTrigger("run");
            yield return new WaitForSeconds(3f);
            _animator.SetBool("idle", true);
            _state = KillerState.Idle;
            killerRanAwayEvent.Raise();
            gameObject.SetActive(false);
        }
    }

    internal enum KillerState {
        Idle,
        Sneak,
        Killing,
        RunningAway
    }
}