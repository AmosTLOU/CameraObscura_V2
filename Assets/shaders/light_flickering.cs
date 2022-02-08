using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light_flickering : MonoBehaviour
{
    public bool isflickering = false;
    public float timedelay;

    // Update is called once per frame
    void Update()
    {
        if(isflickering==false)
        {
            StartCoroutine(lightflicker());
        }
    }

    IEnumerator lightflicker()
    {
        isflickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timedelay = Random.Range(0.01f,0.2f);
        yield return new WaitForSeconds(timedelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timedelay = Random.Range(0.01f, 0.2f);
        yield return new WaitForSeconds(timedelay);
        isflickering = false;
    }
}
