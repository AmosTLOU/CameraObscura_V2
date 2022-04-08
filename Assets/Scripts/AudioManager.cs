using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : SingletonBehaviour<AudioManager> {
    public AudioSource sfxSource;
    public AudioSource dialogSource;
    public AudioSource musicSource;
    // public AudioClip bgMusic;
    // public AudioClip endScream;
    // public AudioClip heartbeats;
    // public AudioClip suspensePiano;
    // public AudioClip mysterySuspense;
    // public AudioClip creepyTensionBuildup;
    //
    // bool m_suspensePianoPlayed = false;
    // bool m_mysterySuspensePlayed = false;
    // bool m_creepyTensionBuildubPlayed = false;
    // bool m_heartbeatsPlayed = false;

    public void PlayMusic(AudioClip clip){
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySfx(AudioClip clip){
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void Speak(AudioClip clip){
        dialogSource.clip = clip;
        dialogSource.Play();
    }

    public void PlayEndScream()
    {
        // sfxSource.clip = endScream;
        // sfxSource.Play();
    }
    
    public void PlayHeartBeats()
    {
        // if (!m_heartbeatsPlayed)
        // {
        //     sfxSource.clip = heartbeats;
        //     sfxSource.Play();
        //     m_heartbeatsPlayed = true;
        // }
    }
    
    public void PlaySuspensePiano()
    {
        // if(!m_suspensePianoPlayed)
        // {
        //     sfxSource.clip = suspensePiano;
        //     sfxSource.Play();
        //     m_suspensePianoPlayed = true;
        // }
    }
    
    public void PlayMysterySuspense()
    {
        // if (!m_mysterySuspensePlayed)
        // {
        //     sfxSource.clip = mysterySuspense;
        //     sfxSource.Play();
        //     m_mysterySuspensePlayed = true;
        // }
    }
    
    public void PlayCreepyTensionBuildup(float delay)
    {
        // if (!m_creepyTensionBuildubPlayed)
        // {
        //     StartCoroutine("PlayAfterDelay", delay);
        //     m_creepyTensionBuildubPlayed = true;
        // }
    }
    
    IEnumerator PlayAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // sfxSource.clip = creepyTensionBuildup;
        // sfxSource.Play();
    }
    
    public void PlayBGMusic()
    {
        // bgAudioSource.clip = bgMusic;
        // bgAudioSource.Play();
    }


}
