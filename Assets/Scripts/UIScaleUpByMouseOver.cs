using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIScaleUpByMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button ButtonSelf;
    public Sprite SpriteMouseAway;
    public Sprite SpriteMouseOver;    

    Vector2 m_sizeMouseOver;
    Vector2 m_sizeMouseAway;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream:Assets/Scripts/UIScaleUpByMouseOver.cs
        ButtonSelf.image.sprite = SpriteMouseAway;
        m_sizeMouseAway = new Vector2(ButtonSelf.image.rectTransform.rect.width,
            ButtonSelf.image.rectTransform.rect.height);
=======
        m_sizeMouseAway = new Vector2(GetComponent<Image>().rectTransform.rect.width,
            GetComponent<Image>().rectTransform.rect.height);
>>>>>>> Stashed changes:Assets/Scripts/UI/PictureInFrame.cs
        m_sizeMouseOver = new Vector2(m_sizeMouseAway.x * 1.2f, m_sizeMouseAway.y * 1.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log(button.image.rectTransform.rect.width + " " + button.image.rectTransform.rect.height);
        ButtonSelf.image.sprite = SpriteMouseOver;
        //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseOver;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ButtonSelf.image.sprite = SpriteMouseAway;
        //ButtonSelf.image.rectTransform.sizeDelta = m_sizeMouseAway;
    }
}