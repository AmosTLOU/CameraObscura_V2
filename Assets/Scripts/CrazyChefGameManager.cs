using System;
using System.Collections;
using Core;
using EventSystem;
using EventSystem.Data;
using UnityEngine;

public class CrazyChefGameManager : SingletonBehaviour<CrazyChefGameManager> {

    [Header("Events")]
    [SerializeField] private GameEvent gameStartEvent;
    [SerializeField] private GameEvent beatStartEvent;

    [Header("Audio")] 
    [SerializeField] private AudioClip killerTheme;
    
    // [Header("Config")]
    private Coroutine activeRoutine;
    
    private IEnumerator Start(){
        yield return new WaitForSeconds(Constants.GameStartToWakeUpDelay);
        gameStartEvent.Raise();
        yield return new WaitForSeconds(Constants.Beat1EndDelay);
        beatStartEvent.Raise(new BeatStartEventData{Beat = GameBeat.KillingAct1});
    }

    public void OnEventBeatStart(IGameEventData eventData){
        if (!Utils.TryConvertVal(eventData, out BeatStartEventData data)){
            Log.Err("Error Converting Type");
            return;
        };
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
        yield return null;
    }
    
    private IEnumerator KillingAct2(){
        yield return null;
    }
    
    private IEnumerator KillingAct3(){
        yield return null;
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