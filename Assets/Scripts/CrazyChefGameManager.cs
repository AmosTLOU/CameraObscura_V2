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
        switch (data.Beat){
            case GameBeat.KillingAct1:
                AudioManager.Instance.PlayMusic(killerTheme);
                break;
            case GameBeat.KillingAct2:
                break;
            case GameBeat.KillingAct3:
                break;
            case GameBeat.Suspect:
                break;
            case GameBeat.Conclusion:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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