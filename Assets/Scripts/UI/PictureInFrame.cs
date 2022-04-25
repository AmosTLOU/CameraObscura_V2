using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PictureInFrame : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite SpriteMouseAway;
    public Sprite SpriteMouseOver;

    Vector2 m_sizeMouseOver;
    Vector2 m_sizeMouseAway;

    // Start is called before the first frame update
    void Start()
    {
        m_sizeMouseAway = new Vector2(GetComponent<Image>().rectTransform.rect.width,
            GetComponent<Image>().rectTransform.rect.height);
        m_sizeMouseOver = new Vector2(m_sizeMouseAway.x * 1.2f, m_sizeMouseAway.y * 1.2f);        
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MenuInputManager.Instance.State = MenuState.Credit_Stop;
        //Debug.Log(button.image.rectTransform.rect.width + " " + button.image.rectTransform.rect.height);
        GetComponent<Image>().sprite = SpriteMouseOver;
        //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseOver;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MenuInputManager.Instance.State = MenuState.Credit_Move;
        GetComponent<Image>().sprite = SpriteMouseAway;
        //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseAway;
    }
}