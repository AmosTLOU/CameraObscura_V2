using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgAudioSource;
    public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip endScream;
    public AudioClip heartbeats;
    public AudioClip suspensePiano;
    public AudioClip mysterySuspense;
    public AudioClip creepyTensionBuildup;

    bool suspensePianoPlayed = false;
    bool mysterySuspensePlayed = false;
    bool creepyTensionBuildubPlayed = false;
    bool heartbeatsPlayed = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlayEndScream()
    {
        audioSource.clip = endScream;
        audioSource.Play();
    }

    public void PlayHeartBeats()
    {
        if (!heartbeatsPlayed)
        {
            audioSource.clip = heartbeats;
            audioSource.Play();
            heartbeatsPlayed = true;
        }
    }

    public void PlaySuspensePiano()
    {
        if(!suspensePianoPlayed)
        {
            audioSource.clip = suspensePiano;
            audioSource.Play();
            suspensePianoPlayed = true;
        }
    }

    public void PlayMysterySuspense()
    {
        if (!mysterySuspensePlayed)
        {
            audioSource.clip = mysterySuspense;
            audioSource.Play();
            mysterySuspensePlayed = true;
        }
    }

    public void PlayCreepyTensionBuildup(float delay)
    {
        if (!creepyTensionBuildubPlayed)
        {
            StartCoroutine("PlayAfterDelay", delay);
            creepyTensionBuildubPlayed = true;
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
