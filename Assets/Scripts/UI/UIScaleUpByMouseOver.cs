using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScaleUpByMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Sprite SpriteMouseAway;
    public Sprite SpriteMouseOver;

    Vector2 m_sizeMouseOver;
    Vector2 m_sizeMouseAway;
    bool mouseOver;

    // Start is called before the first frame update
    void Start()
    {
        m_sizeMouseAway = new Vector2(GetComponent<Image>().rectTransform.rect.width,
            GetComponent<Image>().rectTransform.rect.height);
        m_sizeMouseOver = new Vector2(m_sizeMouseAway.x * 1.2f, m_sizeMouseAway.y * 1.2f);
        mouseOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(mouseOver)
        //{
        //    //Debug.Log(button.image.rectTransform.rect.width + " " + button.image.rectTransform.rect.height);
        //    GetComponent<Image>().sprite = SpriteMouseOver;
        //    //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseOver;
        //}
        //else
        //{
        //    GetComponent<Image>().sprite = SpriteMouseAway;
        //    //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseAway;
        //}
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        GetComponent<Image>().sprite = SpriteMouseOver;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        GetComponent<Image>().sprite = SpriteMouseAway;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(name != "Button_Start" && name != "Button_Sound")
        {
            mouseOver = false;
            GetComponent<Image>().sprite = SpriteMouseAway;
        }        
    }
}
