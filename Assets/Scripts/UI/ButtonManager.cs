using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    float origin_width, origin_height;
    float offset = 10;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        origin_width = rect.sizeDelta.x;
        origin_height = rect.sizeDelta.y;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rect.sizeDelta = new Vector2(origin_width + offset, origin_height + offset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rect.sizeDelta = new Vector2(origin_width, origin_height);
    }
}
