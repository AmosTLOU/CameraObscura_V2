using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

public class MenuInputManager : SingletonBehaviour<MenuInputManager>
{
    //public Texture2D TextureCursor;
    public Image ImageCursor;
    public GameObject[] Layers;

    private int m_layer;

    public bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        ShowLayer(0);

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

    public void ShowLayer(int i_layer)
    {
        m_layer = i_layer;
        for (int i = 0; i < Layers.Length; i++)
        {
            if (i == i_layer)
            {
                if(!Layers[i].activeInHierarchy)
                    Layers[i].SetActive(true);
            }
            else
            {
                Layers[i].SetActive(false);
            }            
        }
    }
}
