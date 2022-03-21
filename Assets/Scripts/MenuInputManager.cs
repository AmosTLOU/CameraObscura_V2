using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager : MonoBehaviour
{
    //public Texture2D TextureCursor;
    public Image ImageCursor;

    // Start is called before the first frame update
    void Start()
    {
        //Vector2 centerOfCursor = new Vector2(TextureCursor.width * 0.5f, TextureCursor.height * 0.5f);
        //Cursor.SetCursor(TextureCursor, centerOfCursor, CursorMode.Auto);

        //Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x + " " + mousePos.y);        
        ImageCursor.rectTransform.anchoredPosition = new Vector2(mousePos.x, mousePos.y);
    }
}
