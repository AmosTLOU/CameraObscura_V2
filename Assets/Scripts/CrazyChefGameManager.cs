using System;
using System.Collections;
using Characters;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using UI;
using UnityEngine;
using Utility;

public class CrazyChefGameManager : SingletonBehaviour<CrazyChefGameManager> {

    [Header("Events")]
    [SerializeField] private GameEvent gameStartEvent;
    [SerializeField] private GameEvent beatStartEvent;

    [Header("Audio")] 
    [SerializeField] private AudioClip killerTheme;

    [Header("Killer Paths")] 
    [SerializeField] private KillerPath killerPath1;
    [SerializeField] private KillerPath killerPath2;
    [SerializeField] private KillerPath killerPath3;

    [Header("Config")]
    [SerializeField] private float suspectPhaseTimeout = 120f;
    
    private Coroutine activeRoutine;
    private Killer _killer;
    private GameBeat _currentBeat;
    private bool _suspectPhaseEnded = false;

    [SerializeField] private GameBeat _startGameBeat = GameBeat.KillingAct1;

    private IEnumerator Start(){
        _killer = CharacterManager.Instance.GetKiller();
        yield return new WaitForSeconds(Constants.GameStartToWakeUpDelay);
        gameStartEvent.Raise();
        yield return new WaitForSeconds(Constants.Beat1EndDelay);
        beatStartEvent.Raise(new BeatStartEventData{Beat = _startGameBeat});
    }

    public void OnEventKillerRanAway(IGameEventData data) {
        _currentBeat  = _currentBeat switch{
            GameBeat.KillingAct1 => GameBeat.KillingAct2,
            GameBeat.KillingAct2 => GameBeat.KillingAct3,
            GameBeat.KillingAct3 => GameBeat.Suspect,
            GameBeat.Suspect => GameBeat.Conclusion,
            GameBeat.Conclusion => GameBeat.Conclusion,
                _ => throw new ArgumentOutOfRangeException()
        };
        beatStartEvent.Raise(new BeatStartEventData{Beat = _currentBeat});
    }

    public void OnEventBeatStart(IGameEventData eventData){
        if (!Utils.TryConvertVal(eventData, out BeatStartEventData data)){
            Log.Err("Error Converting Type");
            return;
        };
        _currentBeat = data.Beat;
        IEnumerator call = data.Beat switch{
            GameBeat.KillingAct1 => KillingAct1(),
            GameBeat.KillingAct2 => KillingAct2(),
            GameBeat.KillingAct3 => KillingAct3(),
            GameBeat.Suspect => SuspectAct(),
            GameBeat.Conclusion => ConclusionAct(),
            _ => throw new ArgumentOutOfRangeException()
        };
        if(activeRoutine!= null) StopCoroutine(activeRoutine);
        activeRoutine = StartCoroutine(call);
    }

    private IEnumerator KillingAct1(){
        AudioManager.Instance.PlayMusic(killerTheme);
        yield return new WaitForSeconds(Constants.BeatEndDelay);
        Log.Info("Starting Killer Act 1".Size(20).Color("White"));
        _killer.SetKillPathAndMove(killerPath1);
    }
    
    private IEnumerator KillingAct2(){
        yield return new WaitForSeconds(Constants.BeatEndDelay);
        Log.Info("Starting Killer Act 2".Size(20).Color("White"));
        _killer.SetKillPathAndMove(killerPath2);
    }
    
    private IEnumerator KillingAct3(){
        yield return new WaitForSeconds(Constants.BeatEndDelay);
        Log.Info("Starting Killer Act 3".Size(20).Color("White"));
        _killer.SetKillPathAndMove(killerPath3);
    }
    
    private IEnumerator SuspectAct(){
        yield return null;
        Log.Info("Starting Suspect Act".Size(20).Color("White"));
        // Wait for the windows to open
        yield return new WaitForSeconds(5f);
        HUDManager.Instance.StartTimer(suspectPhaseTimeout, SuspectPhaseTimedOut);
    }
    
    private void SuspectPhaseTimedOut() {
        // Timeout Queue
        if (_currentBeat != GameBeat.Suspect || _suspectPhaseEnded) return;
        _suspectPhaseEnded = true;
        HUDManager.Instance.StopTimer();
        beatStartEvent.Raise(new BeatStartEventData{Beat = GameBeat.Conclusion});
    }

    public void OnEventAllCluesFound() {
        // All Clues Found
        if (_currentBeat != GameBeat.Suspect || _suspectPhaseEnded) return;
        beatStartEvent.Raise(new BeatStartEventData{Beat = GameBeat.Conclusion});
    }
    
    /// <summary>
    /// Conclusion Beat
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConclusionAct(){
        yield return null;
        Log.Info("Starting Conclusion Act".Size(20).Color("White"));
        // Transition to game camera
    }
}

public enum GameBeat {
    IntroBeat,
    KillingAct1,
    KillingAct2,
    KillingAct3,
    Suspect,
    Conclusion
}