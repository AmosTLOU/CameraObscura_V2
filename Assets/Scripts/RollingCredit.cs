using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredit : MonoBehaviour
{
    private RectTransform m_recTransform;
    private float speed_natural;
    private float speed_manual;
    private float m_width;

    void Start()
    {
        m_recTransform = GetComponent<RectTransform>();
        speed_natural = 3;
        speed_manual = speed_natural * 60;
        m_width = m_recTransform.lossyScale.x * m_recTransform.sizeDelta.x;
    }

    void Update()
    {
        if (MenuInputManager.Instance.State != MenuState.Credit_Stop && MenuInputManager.Instance.State != MenuState.Credit_Move)
            return;

        float offsetWheel = Input.GetAxis("Mouse ScrollWheel");
        if (0.001 < Mathf.Abs(offsetWheel))
        {
            m_recTransform.anchoredPosition += Vector2.left * Time.deltaTime * (offsetWheel < 0 ? -speed_manual : speed_manual);
        }            
        else
        {
            if(MenuInputManager.Instance.State == MenuState.Credit_Move)
                m_recTransform.anchoredPosition += Vector2.left * Time.deltaTime * speed_natural;
        }

        double wing = (m_width * MenuInputManager.Instance.FramesInCredits.Length - Screen.width) * 0.5;
        double leftBoundary = -wing - m_width*0.5;
        double rightBoundary = Screen.width + wing + m_width * 0.5;
        if (m_recTransform.anchoredPosition.x < leftBoundary)
        {
            m_recTransform.anchoredPosition = new Vector2(
                m_recTransform.anchoredPosition.x + m_width * MenuInputManager.Instance.FramesInCredits.Length,
                m_recTransform.anchoredPosition.y);
        }
        else if (rightBoundary < m_recTransform.anchoredPosition.x)
        {
            m_recTransform.anchoredPosition = new Vector2(
                m_recTransform.anchoredPosition.x - m_width * MenuInputManager.Instance.FramesInCredits.Length,
                m_recTransform.anchoredPosition.y);
        }
    }
}
