using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

public class AudioManager : SingletonBehaviour<AudioManager> {
    public AudioSource bgAudioSource;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip endScream;
    public AudioClip heartbeats;
    public AudioClip suspensePiano;
    public AudioClip mysterySuspense;
    public AudioClip creepyTensionBuildup;

    bool m_suspensePianoPlayed = false;
    bool m_mysterySuspensePlayed = false;
    bool m_creepyTensionBuildubPlayed = false;
    bool m_heartbeatsPlayed = false;

    
    public void PlayEndScream()
    {
        audioSource.clip = endScream;
        audioSource.Play();
    }

    public void PlayHeartBeats()
    {
        if (!m_heartbeatsPlayed)
        {
            audioSource.clip = heartbeats;
            audioSource.Play();
            m_heartbeatsPlayed = true;
        }
    }

    public void PlaySuspensePiano()
    {
        if(!m_suspensePianoPlayed)
        {
            audioSource.clip = suspensePiano;
            audioSource.Play();
            m_suspensePianoPlayed = true;
        }
    }

    public void PlayMysterySuspense()
    {
        if (!m_mysterySuspensePlayed)
        {
            audioSource.clip = mysterySuspense;
            audioSource.Play();
            m_mysterySuspensePlayed = true;
        }
    }

    public void PlayCreepyTensionBuildup(float delay)
    {
        if (!m_creepyTensionBuildubPlayed)
        {
            StartCoroutine("PlayAfterDelay", delay);
            m_creepyTensionBuildubPlayed = true;
        }
    }

    IEnumerator PlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.clip = creepyTensionBuildup;
        audioSource.Play();
    }

    public void PlayBGMusic()
    {
        bgAudioSource.clip = bgMusic;
        bgAudioSource.Play();
    }


}
