using System.Collections.Generic;
using Core;
using EventSystem;
using EventSystem.Data;
using Gameplay;
using UnityEngine;

public class ClueManager : SingletonBehaviour<ClueManager> {
    [SerializeField] private List<Clue> clues;

    public void OnEventBeatStart(IGameEventData eventData){
        if (!Utils.TryConvertVal(eventData, out BeatStartEventData data)){
            Log.Err("Error Converting Type");
            return;
        };

        if (data.Beat == GameBeat.Suspect){
            EnableClues();
        }
    }

    private void EnableClues(){
        foreach (var clue in clues){
            clue.EnableDetection(true);
        }
    }
}