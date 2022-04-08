using System.Collections;
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

    PhaseManager m_phaseManager;
    AudioManager m_audioManager;

    int m_current = 0;
    float m_radius = 0.5f;
    bool m_killing = false;


    private void Start()
    {
        m_phaseManager = FindObjectOfType<PhaseManager>();
        m_audioManager = FindObjectOfType<AudioManager>();
        KillerAnimator.SetInteger("StateIndex", 0);
        Victim1Animator.SetInteger("StateIndex", 0);
        GoBloodOnWindow1.SetActive(false);
        GoBloodOnWindow3.SetActive(false);
    }

    private void Update()
    {
        // Debug.Log("Current is " + m_current);

        if (m_phaseManager.GetPhase() == Phase.Killing1)
            Killing();
        else if (Phase.Flee1 <= m_phaseManager.GetPhase() && m_phaseManager.GetPhase() < Phase.Tran2_3 && m_current <= 3)
            RunawayAfterKilling1();
        else if (Phase.Tran1_2 <= m_phaseManager.GetPhase() && m_phaseManager.GetPhase() < Phase.Tran2_3)
            TransitionBetween1and2();
        else if (Phase.Tran2_3 <= m_phaseManager.GetPhase() && m_phaseManager.GetPhase() < Phase.Killing3 && m_current <= 12)
            TransitionBetween2and3();
        else if (Phase.Killing3 <= m_phaseManager.GetPhase() && m_current <= 18)
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
        m_audioManager.PlaySuspensePiano();
        KillerAnimator.SetInteger("StateIndex", 2);
        if (Vector3.Distance(points[m_current].transform.position, KillerTransform.position) < m_radius)
            m_current++;
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[m_current].transform.position, Time.deltaTime * speed);

        if (m_current == 0)
        {
            a = 0;
        }
        if (m_current == 1)
        {
            a = 0;
            GameObject light = GameObject.FindWithTag("Flickering_light");
            //light.GetComponent<light_flickering>().enabled = false;
            light.GetComponent<Light>().intensity = 0;
        }
        if (m_current == 2)
        {
            a = -180;
        }
        if (m_current == 3)
        {
            a = 90;
        }
        Vector3 newRotation = new Vector3(0, a, 0);
        KillerTransform.eulerAngles = newRotation;
    }

    void TransitionBetween1and2()
    {
        // Flickering lights and Sound of progress
        m_audioManager.PlayMysterySuspense();
        foreach (GameObject go in room1Lights)
        {
            go.SetActive(false);
        }
    }


    IEnumerator DelayBeforeFlee()
    {
        m_killing = true;
        KillerAnimator.SetInteger("StateIndex", 3);
        yield return new WaitForSeconds(2f);
        m_killing = false;
    }

    void TransitionBetween2and3()
    {
        m_audioManager.PlayCreepyTensionBuildup(8.5f);
        if (m_killing)
            return;

        KillerAnimator.SetInteger("StateIndex", 2);
        // Killer runs, killer tries to kill but fails, killer flees
        if (Vector3.Distance(points[m_current].transform.position, KillerTransform.position) < m_radius)
        {
            m_current++;
            if (m_current == 12) { 
                StartCoroutine(DelayBeforeFlee());
                return;
            }
        }
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[m_current].transform.position, Time.deltaTime * speed);

        if (m_current == 4)
        {
            a = 90;
        }
        if (m_current == 5)
        {
            a = -90;
        }
        if (m_current == 6)
        {
            a = -180;
        }
        if (m_current == 7)
        {
            a = 90;
        }
        if (m_current == 10)
        {
            a = -90;
        }
        if (m_current == 11)
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
        m_audioManager.PlayEndScream();
        Victim3Animator.SetInteger("StateIndex", 1);
        KillerAnimator.SetInteger("StateIndex", 4);
        endScreenScript.StartAppearing(0);
    }


    void Killing3()
    {
        // Killing, victim died, killer face towards the player
        m_audioManager.PlayHeartBeats();
        KillerAnimator.SetInteger("StateIndex", 2);
        // Killer runs, killer tries to kill but fails, killer flees
        if (Vector3.Distance(points[m_current].transform.position, KillerTransform.position) < m_radius)
        {
            m_current++;
            if (m_current == 19)
            {
                StartCoroutine(FinalKilling());
                return;
            }
        }
        KillerTransform.position = Vector3.MoveTowards(KillerTransform.position, points[m_current].transform.position, Time.deltaTime * speed);

        if (m_current == 13)
        {
            a = 0;
        }
        if (m_current == 14)
        {
            a = 0;
        }
        if (m_current == 15)
        {
            a = 90;
        }
        if (m_current == 16)
        {
            a = 90;
        }
        if (m_current == 17)
        {
            a = -90;
        }
        if (m_current == 18)
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