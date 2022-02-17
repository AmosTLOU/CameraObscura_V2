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

    Phase m_phase;
    int m_indexRoom;
    float m_progress;
    bool[] m_isClueFound;
    int m_cntCluesFound;
    int m_cntTotalClues;

    bool m_onTransition;
    float m_time_AboutToKill_begin;
    float m_time_AboutToKill;

    private void Start()
    {
        m_phase = Phase.Opening;
        m_indexRoom = 0;
        m_progress = 0f;
        if(m_indexRoom < CluesList.Length)
        {
            m_isClueFound = new bool[CluesList[m_indexRoom].Clues.Length];
            for(int i = 0; i < m_isClueFound.Length; i++)
            {
                m_isClueFound[i] = false;
            }
        }
        m_cntCluesFound = 0;
        m_cntTotalClues = m_isClueFound.Length;

        m_onTransition = false;
        m_time_AboutToKill_begin = -1f;
        m_time_AboutToKill = 3f;
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

        // Debug.Log("Phase is " + m_phase);

        if (m_onTransition)
            return;

        //if (m_phase == Phase.Killing1)
        //    WaitToMovePhaseForward(Phase.Room1, 5f);
        //else if (m_phase == Phase.KillerMoveTo2)
        //    WaitToMovePhaseForward(Phase.AboutToKill2, 5f);
        //// Special Situation. May be interrupted, so we cannot use IEnumerator here
        //else if (m_phase == Phase.AboutToKill2)
        //{
        //    if (m_time_AboutToKill_begin == -1f)
        //        m_time_AboutToKill_begin = Time.time;
        //    if (m_time_AboutToKill + m_time_AboutToKill_begin < Time.time)
        //        WaitToMovePhaseForward(Phase.Killing2, 0f);
        //}
        //else if (m_phase == Phase.Killing2)
        //    WaitToMovePhaseForward(Phase.Room2, 5f);
        //else if (m_phase == Phase.Flee2)
        //    WaitToMovePhaseForward(Phase.Room2, 5f);
        //else if (m_phase == Phase.KillerMoveTo3)
        //    WaitToMovePhaseForward(Phase.AboutToKill3, 30f);
        //// Special Situation. May be interrupted, so we cannot use IEnumerator here
        //else if (m_phase == Phase.AboutToKill3)
        //{
        //    if (m_time_AboutToKill_begin == -1f)
        //        m_time_AboutToKill_begin = Time.time;
        //    if (m_time_AboutToKill + m_time_AboutToKill_begin < Time.time)
        //        WaitToMovePhaseForward(Phase.Killing3, 0f);
        //}
        //else if (m_phase == Phase.Killing3)
        //    WaitToMovePhaseForward(Phase.Room3, 5f);
        //else if (m_phase == Phase.Flee3)
        //    WaitToMovePhaseForward(Phase.Room3, 5f);


        if (m_phase == Phase.Opening)
            WaitToMovePhaseForward(m_phase+1, 3f);
        else if (m_phase == Phase.Killing1)
            WaitToMovePhaseForward(m_phase + 1, 3f);
        else if (m_phase == Phase.Flee1)
            WaitToMovePhaseForward(m_phase + 1, 2f);
        else if (m_phase == Phase.Tran1_2)
            WaitToMovePhaseForward(m_phase+1, 5f);
        else if (m_phase == Phase.Tran2_3)
            WaitToMovePhaseForward(m_phase + 1, 5f);    
    }
    
    public Phase GetPhase()
    {
        return m_phase;
    }

    public float GetProgress()
    {
        return m_progress;
    }

    public int GetRoomIndex()
    {
        return m_indexRoom;
    }
    

    public float UpdateProgress(string nameNewClueFound)
    {
        int sz = CluesList.Length;
        if (Phase.StartOfEnum <= m_phase && m_phase < Phase.Room2 && sz < 1)
        {
            return -1f;
        }
        else if (Phase.Room2 <= m_phase && m_phase < Phase.Room3 && sz < 2)
        {
            return -1f;
        }
        else if (Phase.Room3 <= m_phase && m_phase < Phase.EndOfEnum && sz < 3)
        {
            return -1f;
        }

        int cnt = 0;
        foreach(GameObject clue in CluesList[m_indexRoom].Clues)
        {
            if(!m_isClueFound[cnt] && clue.name == nameNewClueFound)
            {
                m_isClueFound[cnt] = true;
                m_cntCluesFound++;
            }
            cnt++;
        }
        m_progress = 1f * m_cntCluesFound / m_cntTotalClues;
        if(m_cntCluesFound == m_cntTotalClues)
        {
            if(m_phase == Phase.Room1)
                WaitToMovePhaseForward(m_phase + 1, 1f);
            else
                WaitToMovePhaseForward(m_phase+1, 5f);

            // assgin 1f to it directly to avoid the float accuracy problem
            m_progress = 1f;
            Debug.Log("All clues are found!");
        }
        return m_progress;
    }

    public void WaitToMovePhaseForward(Phase nextPhase, float timeToWait)
    {
        StartCoroutine(I_WaitToMovePhaseForward(nextPhase, timeToWait));
    }

    IEnumerator I_WaitToMovePhaseForward(Phase nextPhase, float timeToWait)
    {
        Assert.IsTrue(m_phase < nextPhase);
        m_onTransition = true;
        yield return new WaitForSeconds(timeToWait);
        // Updated Phase
        // The function of triggering game events(like killer exit, killer killing) could be added here
        m_phase = nextPhase;
        m_onTransition = false;
        

        bool roomChanged = true;
        if (m_phase == Phase.Room1)
            m_indexRoom = 0;
        else if (m_phase == Phase.Room2)
            m_indexRoom = 1;
        else if (m_phase == Phase.Room3)
            m_indexRoom = 2;
        else
            roomChanged = false;


        // Update related variables if moving to a new room
        if (roomChanged)
        {
            m_progress = 0f;
            if (m_indexRoom < CluesList.Length)
            {
                m_isClueFound = new bool[CluesList[m_indexRoom].Clues.Length];
                for (int i = 0; i < m_isClueFound.Length; i++)
                {
                    m_isClueFound[i] = false;
                }
            }
            m_cntCluesFound = 0;
            m_cntTotalClues = m_isClueFound.Length;
        }
        
    }
}


