using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreenNew : SingletonBehaviour<EndScreenNew>
{
    public GameObject AllElements;
    public Image Panel, TitleLose, TitleWin;
    public GameObject Buttons;
    public AnimationCurve PanelAlphaCurve, TitleAlphaCurve, TitleScaleCurve;
    public float Duration1, Duration2;

    public Image ImageCursor;

    private float m_finalY_Buttons;
    private bool m_gameEnd;

    private void Start()
    {
        m_gameEnd = false;

        Vector2 initialV2 = Buttons.GetComponent<RectTransform>().anchoredPosition;
        m_finalY_Buttons = initialV2.y;
        Buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(initialV2.x, 0);

        AllElements.gameObject.SetActive(false);
    }

    IEnumerator LoadScreen(float i_delay, bool i_lose)
    {
        
        yield return new WaitForSeconds(i_delay);

        m_gameEnd = true;
        AllElements.gameObject.SetActive(true);
        Buttons.gameObject.SetActive(true);
        Panel.gameObject.SetActive(true);
        TitleLose.gameObject.SetActive(i_lose);
        TitleWin.gameObject.SetActive(!i_lose);        

        float startTime = Time.time;
        float currentTime, panelAlpha, titleAlpha, titleScale;

        while (Time.time - startTime < Duration1)
        {
            currentTime = Time.time - startTime;
            panelAlpha = PanelAlphaCurve.Evaluate(Mathf.Min(currentTime / Duration1, 1));
            titleAlpha = TitleAlphaCurve.Evaluate(Mathf.Min(currentTime / Duration1, 1));
            titleScale = TitleScaleCurve.Evaluate(Mathf.Min(currentTime / Duration1, 1));

            Panel.color = new Color(0, 0, 0, panelAlpha);
            if (i_lose)
            {
                TitleLose.color = new Color(1, 1, 1, titleAlpha);
                TitleLose.transform.localScale = new Vector3(titleScale, titleScale, titleScale);
            }
            else
            {
                TitleWin.color = new Color(1, 1, 1, titleAlpha);
                TitleWin.transform.localScale = new Vector3(titleScale, titleScale, titleScale);
            }
            yield return null;
        }

        if(i_lose)
        {
            startTime = Time.time;
            while (Time.time - startTime < Duration2)
            {
                currentTime = Time.time - startTime;
                Vector2 curV2 = Buttons.GetComponent<RectTransform>().anchoredPosition;
                Buttons.GetComponent<RectTransform>().anchoredPosition = new Vector2(curV2.x, m_finalY_Buttons * currentTime / Duration2);
                yield return null;
            }
        }
    }

    public void ShowLoseScreen(float delay = 0)
    {
        StartCoroutine(LoadScreen(delay, true));
    }

    public void ShowWinScreen(float delay = 0)
    {
        StartCoroutine(LoadScreen(delay, false));
    }


    public void RestartGame()
    {
        m_gameEnd = false;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //    ShowLoseScreen();
        //else if (Input.GetKeyDown(KeyCode.P))
        //    ShowWinScreen();

        if (m_gameEnd)
        {
            ImageCursor.gameObject.SetActive(true);
            Cursor.visible = false;
            Vector3 mousePos = Input.mousePosition;
            ImageCursor.rectTransform.anchoredPosition = new Vector2(mousePos.x, mousePos.y);
        }        
        else
        {
            ImageCursor.gameObject.SetActive(false);
        }
    }
}
