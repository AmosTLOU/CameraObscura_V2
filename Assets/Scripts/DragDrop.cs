using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        //Output the name of the GameObject that is being clicked
        Debug.Log(name + "Game Object Click in Progress");
    }
    //Image i;
    //public Camera gridCamera; //Camera that renders to the texture
    //private RectTransform textureRectTransform; //RawImage RectTransform that shows the RenderTexture on the UI
    //bool isPress = false;

    //private void Start()
    //{
    //    textureRectTransform = this.transform.parent.GetComponent<RectTransform>();
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        Ray ray = gridCamera.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            if (hit.collider == this.GetComponent<Collider>())
    //            {
    //                isPress = true;
    //            }
    //        }
    //    }
    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        isPress = false;
    //    }

    //    if (isPress)
    //    {
    //        Ray ray = gridCamera.ScreenPointToRay(Input.mousePosition);

    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //            textureRectTransform.position = hit.point;

    //    }
    //}
}


