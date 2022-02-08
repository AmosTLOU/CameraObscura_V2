using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{

    public GameObject GoBloodOnWindow1;
    public GameObject GoBloodOnWindow3;
    public GameObject[] points;
    public GameObject opening_cam; //opening
    public GameObject main_cam; //opening
    public Transform KillerTransform;
    public Animator KillerAnimator;
    public Animator Victim1Animator;
    public Animator Victim3Animator;
    public EndScreenScript endScreenScript;
    //float rotspeed;ef
    public float speed;
    public int a;
    public GameObject[] room1Lights;

    PhaseManager _phaseManager;
    AudioManager audioManager;

    int current = 0;
    float radius = 0.5f;
    bool _killing = false;


    private void Start()
    {
        _phaseManager = FindObjectOfType<PhaseManager>();
        audioManager = FindObjectOfType<AudioManager>();
        KillerAnimator.SetInteger("StateIndex", 0);
        Victim1Animator.SetInteger("StateIndex", 0);
        GoBloodOnWindow1.SetActive(false);
        GoBloodOnWindow3.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Current is " + current);

        if (_phaseManager.GetPhase() == Phase.Killing1)
            Killing();
        else if (Phase.Flee1 <= _phaseManager.GetPhase() && _phaseManager.GetPhase() < Phase.Tran2_3 && current <= 3)
            RunawayAfterKilling1();
        else if (Phase.Tran1_2 <= _phaseManager.GetPhase() && _phaseManager.GetPhase() < Phase.Tran2_3)
            TransitionBetween1and2();
        else if (Phase.Tran2_3 <= _phaseManager.GetPhase() && _phaseManager.GetPhase() < Phase.Killing3 && current <= 12)
            TransitionBetween2and3();
        else if (Phase.Killing3 <= _phaseManager.GetPhase() && current <= 18)
            Killing3();

    }

    IEnumerator DelayBeforeFallDown()
    {
        opening_cam.SetActive(true);    //opening
        yield return new WaitForSeconds(2f);  //opening
        opening_cam.SetActive(false);  //opening
        main_cam.SetActive(true);  //opening

        KillerAnimator.SetInteger("StateIndex", 1);
        yield return new WaitForSeconds(3.5f);
        GoBloodOnWindow1.SetActive(true);
        Victim1Animator.SetInteger("StateIndex", 1);
    }

    void Killing()
    {
        //KillerAnimator.SetInteger("StateIndex", 1);
        
        StartCoroutine(DelayBeforeFallDown());
    }

    void RunawayAfterKilling1()
    {
        audioManager.PlaySuspensePiano();
        KillerAnimator.SetInteger("StateIndex", 2);
        if (Vector3.Distance(points[current].transform.position, KillerTransform.position) < radius)
            current++;
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[current].transform.position, Time.deltaTime * speed);

        if (current == 0)
        {
            a = 0;
        }
        if (current == 1)
        {
            a = 0;
            GameObject light = GameObject.FindWithTag("Flickering_light");
            //light.GetComponent<light_flickering>().enabled = false;
            light.GetComponent<Light>().intensity = 0;
        }
        if (current == 2)
        {
            a = -180;
        }
        if (current == 3)
        {
            a = 90;
        }
        Vector3 newRotation = new Vector3(0, a, 0);
        KillerTransform.eulerAngles = newRotation;
    }

    void TransitionBetween1and2()
    {
        // Flickering lights and Sound of progress
        audioManager.PlayMysterySuspense();
        foreach (GameObject go in room1Lights)
        {
            go.SetActive(false);
        }
    }


    IEnumerator DelayBeforeFlee()
    {
        _killing = true;
        KillerAnimator.SetInteger("StateIndex", 3);
        yield return new WaitForSeconds(2f);
        _killing = false;
    }

    void TransitionBetween2and3()
    {
        audioManager.PlayCreepyTensionBuildup(8.5f);
        if (_killing)
            return;

        KillerAnimator.SetInteger("StateIndex", 2);
        // Killer runs, killer tries to kill but fails, killer flees
        if (Vector3.Distance(points[current].transform.position, KillerTransform.position) < radius)
        {
            current++;
            if (current == 12) { 
                StartCoroutine(DelayBeforeFlee());
                return;
            }
        }
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[current].transform.position, Time.deltaTime * speed);

        if (current == 4)
        {
            a = 90;
        }
        if (current == 5)
        {
            a = -90;
        }
        if (current == 6)
        {
            a = -180;
        }
        if (current == 7)
        {
            a = 90;
        }
        if (current == 10)
        {
            a = -90;
        }
        if (current == 11)
        {
            a = -180;
        }
        Vector3 newRotation = new Vector3(0, a, 0);
        KillerTransform.eulerAngles = newRotation;

    }

    IEnumerator FinalKilling()
    {
        KillerAnimator.SetInteger("StateIndex", 1);
        yield return new WaitForSeconds(3f);
        GoBloodOnWindow3.SetActive(true);
        audioManager.PlayEndScream();
        Victim3Animator.SetInteger("StateIndex", 1);
        KillerAnimator.SetInteger("StateIndex", 4);
        endScreenScript.StartAppearing(0);
    }


    void Killing3()
    {
        // Killing, victim died, killer face towards the player
        audioManager.PlayHeartBeats();
        KillerAnimator.SetInteger("StateIndex", 2);
        // Killer runs, killer tries to kill but fails, killer flees
        if (Vector3.Distance(points[current].transform.position, KillerTransform.position) < radius)
        {
            current++;
            if (current == 19)
            {
                StartCoroutine(FinalKilling());
                return;
            }
        }
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[current].transform.position, Time.deltaTime * speed);

        if (current == 13)
        {
            a = 0;
        }
        if (current == 14)
        {
            a = 0;
        }
        if (current == 15)
        {
            a = 90;
        }
        if (current == 16)
        {
            a = 90;
        }
        if (current == 17)
        {
            a = -90;
        }
        if (current == 18)
        {
            a = 0;
            GameObject light = GameObject.FindWithTag("Flickering_light2");
            //light.GetComponent<light_flickering>().enabled = false;
            light.GetComponent<Light>().intensity = 7.7f;
        }
        Vector3 newRotation = new Vector3(0, a, 0);
        KillerTransform.eulerAngles = newRotation;
    }

}