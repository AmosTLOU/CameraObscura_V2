using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFlicker : MonoBehaviour
{
    public float FlickerRate;
    public GameObject TargetGo;

    float m_lastTimeFlick;
    bool m_selfActive;
    
    // Start is called before the first frame update
    void Start()
    {
        m_lastTimeFlick = float.NegativeInfinity;
        TargetGo.SetActive(true);
        m_selfActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_lastTimeFlick + FlickerRate < Time.time)
        {
            m_lastTimeFlick = Time.time;
            m_selfActive = !m_selfActive;
            TargetGo.SetActive(m_selfActive);
        }
    }
}
