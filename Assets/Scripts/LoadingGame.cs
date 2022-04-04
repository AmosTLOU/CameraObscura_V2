using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingGame : MonoBehaviour
{
    public GameObject CanvasMenu;
    public GameObject CanvasShoot;
    public Animator AnimatorClickToStart;
    public RawImage RawImageCamera;
    public RenderTexture renderTextureCamera;
    public Camera SecondCamera;

    //public float SpeedSmoothing;

    void Start()
    {
        //CanvasMenu.SetActive(true);
        SecondCamera.gameObject.SetActive(true);
        RawImageCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        //RawImageCamera.texture = renderTextureCamera;
    }

    IEnumerator EnlargeScreen()
    {
        yield return new WaitForSeconds(1.5f);

        RawImageCamera.gameObject.SetActive(true);
        Vector2 ScreenSize = new Vector2(Screen.width, Screen.height);
        while (RawImageCamera.rectTransform.sizeDelta != ScreenSize)
        {
            RawImageCamera.texture = renderTextureCamera;
            RawImageCamera.rectTransform.sizeDelta = Vector2.MoveTowards(RawImageCamera.rectTransform.sizeDelta, ScreenSize, Time.deltaTime * 650);
            Vector2 v2_centerOfPicture = new Vector2(RawImageCamera.rectTransform.anchoredPosition.x, RawImageCamera.rectTransform.anchoredPosition.y);
            RawImageCamera.rectTransform.anchoredPosition = Vector2.MoveTowards(v2_centerOfPicture, Vector2.zero, Time.deltaTime * 65);
            yield return null;
        }
        CanvasMenu.SetActive(false);
        CanvasShoot.SetActive(true);
        SecondCamera.gameObject.SetActive(false);
    }
    public void LoadGame()
    {
        AnimatorClickToStart.SetBool("Start", true);
        StartCoroutine(EnlargeScreen());                
    }
}
