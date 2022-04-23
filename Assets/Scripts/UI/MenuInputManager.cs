using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuInputManager : SingletonBehaviour<MenuInputManager>
{
    public MenuState State;
    public GameObject Credits;
    public GameObject[] FramesInCredits;
    public Animator AnimatorClickToStart;
    public RawImage RawImageCamera;
    public RenderTexture renderTextureCamera;

    //public Texture2D TextureCursor;
    public Image[] ImageCursor;
    public GameObject[] Layers;   

    private int m_layer;

    void Start()
    {
        State = MenuState.Idle;
        Credits.SetActive(false);
        RawImageCamera.gameObject.SetActive(false);

        ShowLayer(0);

        //Vector2 centerOfCursor = new Vector2(TextureCursor.width * 0.5f, TextureCursor.height * 0.5f);
        //Cursor.SetCursor(TextureCursor, centerOfCursor, CursorMode.Auto);

        //Cursor.lockState = CursorLockMode.Confined;
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x + " " + mousePos.y);
        for (int i = 0; i < ImageCursor.Length; i++)
        {
            ImageCursor[i].rectTransform.anchoredPosition = new Vector2(mousePos.x, mousePos.y);
        }
        if(State == MenuState.LoadGame)
        {
            ImageCursor[0].gameObject.SetActive(false);
            ImageCursor[1].gameObject.SetActive(false);
        }
        else if (State == MenuState.Idle)
        {
            ImageCursor[0].gameObject.SetActive(true);
            ImageCursor[1].gameObject.SetActive(false);
        }
        else
        {
            ImageCursor[0].gameObject.SetActive(false);
            ImageCursor[1].gameObject.SetActive(true);
        }
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
        if (m_layer == 0)
            State = MenuState.Idle;
        else if(m_layer == 1)
            State = MenuState.Idle;
        else
            State = MenuState.Color;
    }

    IEnumerator SetStateToStartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        State = MenuState.StartGame;
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
        yield return StartCoroutine(SetStateToStartWithDelay(1f));

        gameObject.SetActive(false);
    }
    public void LoadGame()
    {
        State = MenuState.LoadGame;
        AnimatorClickToStart.SetBool("Start", true);
        StartCoroutine(EnlargeScreen());
        SceneManager.LoadScene(1);
    }    

    public void InOrOutCredits(bool enter)
    {
        Credits.SetActive(enter);
        if (enter)
        {
            State = MenuState.Credit_Move;
        }
        else
        {
            State = MenuState.Idle;
        }
        
    }
}

public enum MenuState
{
    LoadGame,
    StartGame,
    Credit_Move,
    Credit_Stop,
    Color,
    Idle
}
