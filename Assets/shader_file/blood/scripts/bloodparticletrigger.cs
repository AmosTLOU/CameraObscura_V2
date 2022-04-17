using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodparticletrigger : MonoBehaviour
{
    // Start is called before the first frame update
    float time;
    float timedelay;

    public ParticleSystem blood;
    void Start()
    {
        blood = GetComponent<ParticleSystem>();
        time = 0f;
        timedelay = 2.0f;
        //blood.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Example());
        time = time + 1f * Time.deltaTime;
        if(time>=timedelay)
        {
            time = 0f;
            blood.Play();
        }
    }

}

    
//   IEnumerator Example()
//    {
//        blood.Play();
//        yield return new WaitForSeconds(2);
//        blood.Play();
//    }
//}
