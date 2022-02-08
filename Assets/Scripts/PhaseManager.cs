using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum Phase
{
    // NullPhase/StartOfEnum/EndOfEnum don't represent an actual phase, just an indicator
    NullPhase,
    StartOfEnum,

    //Killing1,
    //Room1,
    //KillerMoveTo2,
    //// The phase when players can stop the killer by flashlight
    //AboutToKill2,
    //// Only one of the following 2 would be chosen
    //Killing2,
    //Flee2,
    //Room2,
    //KillerMoveTo3,
    //// The phase when players can stop the killer by flashlight
    //AboutToKill3,
    //// Only one of the following 2 would be chosen
    //Killing3,
    //Flee3,
    //Room3,

    Opening,
    Killing1,
    StandAfterKilling1,
    Flee1,
    Room1,
    Tran1_2,
    Room2,
    Tran2_3,
    Room3,
    Killing3,

    EndOfEnum
}

[System.Serializable]
public class CluesInOnePhase
{
    public GameObject[] Clues;
}

public class PhaseManager : MonoBehaviour
{
    public CluesInOnePhase[] CluesList;

    Phase _phase;
    int _indexRoom;
    float _progress;
    bool[] _isClueFound;
    int _cntCluesFound;
    int _cntTotalClues;

    bool _onTransition;
    float _time_AboutToKill_begin;
    float _time_AboutToKill;

    private void Start()
    {
        _phase = Phase.Opening;
        _indexRoom = 0;
        _progress = 0f;
        if(_indexRoom < CluesList.Length)
        {
            _isClueFound = new bool[CluesList[_indexRoom].Clues.Length];
            for(int i = 0; i < _isClueFound.Length; i++)
            {
                _isClueFound[i] = false;
            }
        }
        _cntCluesFound = 0;
        _cntTotalClues = _isClueFound.Length;

        _onTransition = false;
        _time_AboutToKill_begin = -1f;
        _time_AboutToKill = 3f;
    }

    private void Update()
    {
        //Opening,
        //Killing1,
        //Room1,
        //Tran1_2,
        //Room2,
        //Tran2_3,
        //Room3,
        //Killing3,

        Debug.Log("Phase is " + _phase);

        if (_onTransition)
            return;

        //if (_phase == Phase.Killing1)
        //    WaitToMovePhaseForward(Phase.Room1, 5f);
        //else if (_phase == Phase.KillerMoveTo2)
        //    WaitToMovePhaseForward(Phase.AboutToKill2, 5f);
        //// Special Situation. May be interrupted, so we cannot use IEnumerator here
        //else if (_phase == Phase.AboutToKill2)
        //{
        //    if (_time_AboutToKill_begin == -1f)
        //        _time_AboutToKill_begin = Time.time;
        //    if (_time_AboutToKill + _time_AboutToKill_begin < Time.time)
        //        WaitToMovePhaseForward(Phase.Killing2, 0f);
        //}
        //else if (_phase == Phase.Killing2)
        //    WaitToMovePhaseForward(Phase.Room2, 5f);
        //else if (_phase == Phase.Flee2)
        //    WaitToMovePhaseForward(Phase.Room2, 5f);
        //else if (_phase == Phase.KillerMoveTo3)
        //    WaitToMovePhaseForward(Phase.AboutToKill3, 30f);
        //// Special Situation. May be interrupted, so we cannot use IEnumerator here
        //else if (_phase == Phase.AboutToKill3)
        //{
        //    if (_time_AboutToKill_begin == -1f)
        //        _time_AboutToKill_begin = Time.time;
        //    if (_time_AboutToKill + _time_AboutToKill_begin < Time.time)
        //        WaitToMovePhaseForward(Phase.Killing3, 0f);
        //}
        //else if (_phase == Phase.Killing3)
        //    WaitToMovePhaseForward(Phase.Room3, 5f);
        //else if (_phase == Phase.Flee3)
        //    WaitToMovePhaseForward(Phase.Room3, 5f);


        if (_phase == Phase.Opening)
            WaitToMovePhaseForward(_phase+1, 3f);
        else if (_phase == Phase.Killing1)
            WaitToMovePhaseForward(_phase + 1, 3f);
        else if (_phase == Phase.Flee1)
            WaitToMovePhaseForward(_phase + 1, 2f);
        else if (_phase == Phase.Tran1_2)
            WaitToMovePhaseForward(_phase+1, 5f);
        else if (_phase == Phase.Tran2_3)
            WaitToMovePhaseForward(_phase + 1, 5f);    
    }
    
    public Phase GetPhase()
    {
        return _phase;
    }

    public float GetProgress()
    {
        return _progress;
    }

    public int GetRoomIndex()
    {
        return _indexRoom;
    }
    

    public float UpdateProgress(string nameNewClueFound)
    {
        int sz = CluesList.Length;
        if (Phase.StartOfEnum <= _phase && _phase < Phase.Room2 && sz < 1)
        {
            return -1f;
        }
        else if (Phase.Room2 <= _phase && _phase < Phase.Room3 && sz < 2)
        {
            return -1f;
        }
        else if (Phase.Room3 <= _phase && _phase < Phase.EndOfEnum && sz < 3)
        {
            return -1f;
        }

        int cnt = 0;
        foreach(GameObject clue in CluesList[_indexRoom].Clues)
        {
            if(!_isClueFound[cnt] && clue.name == nameNewClueFound)
            {
                _isClueFound[cnt] = true;
                _cntCluesFound++;
            }
            cnt++;
        }
        _progress = 1f * _cntCluesFound / _cntTotalClues;
        if(_cntCluesFound == _cntTotalClues)
        {
            if(_phase == Phase.Room1)
                WaitToMovePhaseForward(_phase + 1, 1f);
            else
                WaitToMovePhaseForward(_phase+1, 5f);

            // assgin 1f to it directly to avoid the float accuracy problem
            _progress = 1f;
            Debug.Log("All clues are found!");
        }
        return _progress;
    }

    public void WaitToMovePhaseForward(Phase nextPhase, float timeToWait)
    {
        StartCoroutine(I_WaitToMovePhaseForward(nextPhase, timeToWait));
    }

    IEnumerator I_WaitToMovePhaseForward(Phase nextPhase, float timeToWait)
    {
        Assert.IsTrue(_phase < nextPhase);
        _onTransition = true;
        yield return new WaitForSeconds(timeToWait);
        // Updated Phase
        // The function of triggering game events(like killer exit, killer killing) could be added here
        _phase = nextPhase;
        _onTransition = false;
        

        bool roomChanged = true;
        if (_phase == Phase.Room1)
            _indexRoom = 0;
        else if (_phase == Phase.Room2)
            _indexRoom = 1;
        else if (_phase == Phase.Room3)
            _indexRoom = 2;
        else
            roomChanged = false;


        // Update related variables if moving to a new room
        if (roomChanged)
        {
            _progress = 0f;
            if (_indexRoom < CluesList.Length)
            {
                _isClueFound = new bool[CluesList[_indexRoom].Clues.Length];
                for (int i = 0; i < _isClueFound.Length; i++)
                {
                    _isClueFound[i] = false;
                }
            }
            _cntCluesFound = 0;
            _cntTotalClues = _isClueFound.Length;
        }
        
    }
}


