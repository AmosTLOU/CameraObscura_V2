using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager2 : SingletonBehaviour<MenuInputManager2>
{
    public MenuState2 State;
    public GameObject[] FramesInCredits;


    public Image[] ImageCursor;


    void Start()
    {
        State = MenuState2.Idle;
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Vector3 mousePos = Input.mousePosition;

        for (int i = 0; i < ImageCursor.Length; i++)
        {
            ImageCursor[i].rectTransform.anchoredPosition = new Vector2(mousePos.x, mousePos.y);
        }
        ImageCursor[0].gameObject.SetActive(false);
        ImageCursor[1].gameObject.SetActive(true);

    }


    IEnumerator SetStateToStartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        State = MenuState2.StartGame;
    }

}

public enum MenuState2
{
    LoadGame,
    StartGame,
    Credit_Move,
    Credit_Stop,
    Color,
    Idle
}
