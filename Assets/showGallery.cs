using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showGallery : MonoBehaviour
{
    public GameObject CanvasGallery;
    public GameObject CanvasShoot;
    // Start is called before the first frame update
    void Start()
    {
        CanvasGallery.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CanvasGallery.gameObject.SetActive(!CanvasGallery.gameObject.activeInHierarchy);
            if(CanvasGallery.gameObject.activeInHierarchy)
                MenuInputManager2.Instance.State = MenuState2.Credit_Move;
            CanvasShoot.gameObject.SetActive(!CanvasShoot.gameObject.activeInHierarchy);
        }
            
    }
}
