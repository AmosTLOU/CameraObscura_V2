using System;
using System.Collections;
using Characters;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using UnityEngine;

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
    
    
    // [Header("Config")]
    private Coroutine activeRoutine;
    private Killer _killer;
    private GameBeat _currentBeat;
    
    private IEnumerator Start(){
        _killer = CharacterManager.Instance.GetKiller();
        yield return new WaitForSeconds(Constants.GameStartToWakeUpDelay);
        gameStartEvent.Raise();
        yield return new WaitForSeconds(Constants.Beat1EndDelay);
        beatStartEvent.Raise(new BeatStartEventData{Beat = GameBeat.Suspect});
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
        Log.Info("Starting Killer Act 1");
        _killer.SetKillPathAndMove(killerPath1);
    }
    
    private IEnumerator KillingAct2(){
        yield return new WaitForSeconds(Constants.BeatEndDelay);
        Log.Info("Starting Killer Act 2");
        _killer.SetKillPathAndMove(killerPath2);
    }
    
    private IEnumerator KillingAct3(){
        yield return new WaitForSeconds(Constants.BeatEndDelay);
        Log.Info("Starting Killer Act 3");
        _killer.SetKillPathAndMove(killerPath3);
    }
    
    private IEnumerator SuspectAct(){
        yield return null;
    }
    
    /// <summary>
    /// Conclusion Beat
    /// </summary>
    /// <returns></returns>
    private IEnumerator ConclusionAct(){
        yield return null;
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