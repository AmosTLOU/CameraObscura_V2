using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger_anim : MonoBehaviour
{
    public GameObject man;

    void Update()
    {
        if(Input.GetKeyDown("c"))
        {
            man.GetComponent<Animator>().Play("demo_trail");
        }
    }
}
