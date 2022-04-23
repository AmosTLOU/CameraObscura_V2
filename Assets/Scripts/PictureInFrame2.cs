using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PictureInFrame2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public GameObject HighlightedImage;
    public GameObject DetailedImage;

    private void Start()
    {
        HighlightedImage.SetActive(false);
        DetailedImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MenuInputManager2.Instance.State = MenuState2.Credit_Stop;
        HighlightedImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MenuInputManager2.Instance.State = MenuState2.Credit_Move;
        HighlightedImage.SetActive(false);
        DetailedImage.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        DetailedImage.SetActive(!DetailedImage.activeInHierarchy);
    }
}
