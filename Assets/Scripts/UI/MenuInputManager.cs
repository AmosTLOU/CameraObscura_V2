using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager : SingletonBehaviour<MenuInputManager>
{
    public GameObject CanvasHUD;
    public Animator AnimatorClickToStart;
    public RawImage RawImageCamera;
    public RenderTexture renderTextureCamera;
    public Camera SecondCamera;

    //public Texture2D TextureCursor;
    public Image ImageCursor;
    public GameObject[] Layers;   

    private int m_layer;

    public bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        CanvasHUD.gameObject.SetActive(false);
        SecondCamera.gameObject.SetActive(true);
        RawImageCamera.gameObject.SetActive(false);

        ShowLayer(0);

        //Vector2 centerOfCursor = new Vector2(TextureCursor.width * 0.5f, TextureCursor.height * 0.5f);
        //Cursor.SetCursor(TextureCursor, centerOfCursor, CursorMode.Auto);

        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x + " " + mousePos.y);        
        ImageCursor.rectTransform.anchoredPosition = new Vector2(mousePos.x, mousePos.y);
    }

    public void ShowLayer(int i_layer)
    {
        m_layer = i_layer;
        for (int i = 0; i < Layers.Length; i++)
        {
            if (i == i_layer)
            {
                if(!Layers[i].activeInHierarchy)
                    Layers[i].SetActive(true);
            }
            else
            {
                Layers[i].SetActive(false);
            }            
        }
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
        CanvasHUD.SetActive(true);
        SecondCamera.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void LoadGame()
    {
        AnimatorClickToStart.SetBool("Start", true);
        StartCoroutine(EnlargeScreen());
    }
}
