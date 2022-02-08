using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFlicker : MonoBehaviour
{
    public float FlickerRate;
    public GameObject TargetGo;

    float _lastTimeFlick;
    bool _selfActive;
    
    // Start is called before the first frame update
    void Start()
    {
        _lastTimeFlick = float.NegativeInfinity;
        TargetGo.SetActive(true);
        _selfActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_lastTimeFlick + FlickerRate < Time.time)
        {
            _lastTimeFlick = Time.time;
            _selfActive = !_selfActive;
            TargetGo.SetActive(_selfActive);
        }
    }
}
