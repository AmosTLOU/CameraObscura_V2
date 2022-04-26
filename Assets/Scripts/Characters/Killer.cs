using System.Collections;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using Sirenix.Utilities;
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

        private Clue[] _killerClue;
        private int _maxKillerClues = 1;
        private int _currentKillerClues = 0;

        private void Start(){
            _follower = GetComponent<PathFollower>();
            gameObject.SetActive(false);
            _killerClue = GetComponents<Clue>();
        }
        
        public void OnNightStart(IGameEventData data){
            // killingRoutine = StartCoroutine(KillVictim());
        }

        public void Interrupted(CameraClickEventData data){
            Log.Info("Checking for interrupted");
            // Add check
            if (_state != KillerState.Sneak) {
                Log.Debug("Camera Flash during non-killing phase", Constants.TagTimeline);
                return;
            }
            killingRoutine = null;
            _follower.reachedEnd.RemoveAllListeners();
            StartCoroutine(RunAway());
            Log.Info("Successfully interrupted");
            // StopCoroutine(killingRoutine);
        }

        public void SetKillPathAndMove(KillerPath path){
            if (_state != KillerState.Idle){
                Log.Err($"Killer in unexpected state; Current={_state}, Expected = {KillerState.Idle}");
                return;
            }
            Log.Info("killer sneaking in!", Constants.TagTimeline);
            if(_currentKillerClues++ < _maxKillerClues) _killerClue.ForEach(c => c.Reset());
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