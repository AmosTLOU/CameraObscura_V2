using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;
using System.IO;
using Core;
using EventSystem.Data;

// Use DulNode structure to view and scroll photos
[HideInInspector]
public class DuLNode
{
    public string StrName { get; set; }
    public bool HasClue { get; set; }
    public string ClueName { get; set; }
    public Phase PhaseBelongTo { get; set; }
    public Vector3 ViewPos { get; set; }
    public DuLNode Prev { get; set; }
    public DuLNode Next { get; set; }

    public DuLNode(string i_strName, Phase i_phase){
        StrName = i_strName;
        HasClue = false;
        ClueName = "";
        PhaseBelongTo = i_phase;
        ViewPos = Vector3.zero;
        Prev = null;
        Next = null;
    }
}

public class PhotoGallery : MonoBehaviour
{
    public RawImage ImageDisplay;
    public Text TextIndexOfPhoto;
    public Text TextHintMessage;
    public Image ImageRedCircle;
    public GameObject HintDetails;

    GameManager m_gameManager;

    float m_scaleX;
    float m_scaleY;
    DuLNode m_headNode;
    DuLNode m_tailNode;
    DuLNode m_curNode;
    int m_cntPhoto;
    int m_curIndex;
    string m_pathPhotos;


    private void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();

        TextIndexOfPhoto.text = "";
        HintDetails.SetActive(false);

        // since the viewPos is normalized, we need the scale to put the red cirlce on the right position
        m_scaleX = (Screen.width + ImageDisplay.GetComponent<RectTransform>().rect.width)/2;
        m_scaleY = (Screen.height + ImageDisplay.GetComponent<RectTransform>().rect.height)/2;
        m_headNode = null;
        m_tailNode = null;
        m_cntPhoto = 0;
        m_curIndex = -1;

        // Clear the photos from the last time when game restarts
        m_pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(m_pathPhotos)) 
        {
            Debug.Log("Clearing Old Photos.");
            Directory.Delete(m_pathPhotos); 
        }
        Debug.Log("Creating a new gallery.");
        Directory.CreateDirectory(m_pathPhotos);
    }

    // Save the screenshot
    public void Capture()
    {
        //string strDateAndTime = System.DateTime.Now.ToString();
        ScreenCapture.CaptureScreenshot(m_pathPhotos + m_cntPhoto + ".png");
        if (m_headNode == null)
        {
            m_headNode = new DuLNode(m_cntPhoto.ToString(), Phase.NullPhase);
            m_tailNode = m_headNode;
            m_headNode.Next = m_tailNode;
            m_tailNode.Prev = m_headNode;
        }
        else
        {
            m_tailNode.Next = new DuLNode(m_cntPhoto.ToString(), Phase.NullPhase);
            m_tailNode.Next.Prev = m_tailNode;
            m_tailNode = m_tailNode.Next;
            m_tailNode.Next = m_headNode;
            m_headNode.Prev = m_tailNode;
        }
        m_cntPhoto++;
    }

    // Show a full picture
    public void Show(DuLNode node, int index)
    {
        TextHintMessage.gameObject.SetActive(false);
        ImageRedCircle.gameObject.SetActive(false);
        HintDetails.SetActive(false);
        if (node != null && 0 <= index)
        {
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(m_pathPhotos + node.StrName + ".png");
            Texture2D textureLoad = new Texture2D(1, 1);
            textureLoad.LoadImage(bytes);
            if (textureLoad)
            {
                //Debug.Log("Load Picture Success");
                ImageDisplay.texture = textureLoad;
            }
            else
            {
                //Debug.Log("Load Picture Failure");
                return;
            }
            TextIndexOfPhoto.text = (index + 1) + " / " + m_cntPhoto;
            if (node.HasClue && node.PhaseBelongTo <= m_gameManager.GetPhase())
            {
                TextHintMessage.gameObject.SetActive(true);
                ImageRedCircle.gameObject.SetActive(true);
                ImageRedCircle.rectTransform.anchoredPosition = new Vector2((2f * node.ViewPos.x - 1f) * m_scaleX, (2f * node.ViewPos.y - 1f) * m_scaleY);
            }            
        }
    }

    public void EnterGallery()
    {
        m_curNode = m_tailNode;
        m_curIndex = m_cntPhoto-1;
        Show(m_curNode, m_curIndex);
    }

    public void ShowNextPhoto()
    {
        if (m_curNode == null)
            return;
        m_curIndex++;
        m_curIndex %= m_cntPhoto;
        m_curNode = m_curNode.Next;
        Show(m_curNode, m_curIndex);
    }

    public void ShowPrevPhoto()
    {
        if (m_curNode == null)
            return;
        m_curIndex--;
        if (m_curIndex < 0)
            m_curIndex = m_cntPhoto - 1;
        m_curNode = m_curNode.Prev;
        Show(m_curNode, m_curIndex);
    }

    public void OnSwipePhoto(IGameEventData data){
        if (!Utils.TryConvertVal(data, out SwipePhotoEventData result)){
            return;
        }

        if (result.Left) ShowPrevPhoto();
        else ShowNextPhoto();
    }

    public void AddPromptToPhoto(Vector3 viewPos, string clueName, Phase phaseBelongTo)
    {
        if(m_tailNode == null)
        {
            Debug.Log("Clue is captured, but fail to take a photo.");
            return;
        }
        m_tailNode.HasClue = true;
        m_tailNode.ViewPos = viewPos;
        m_tailNode.ClueName = clueName;
        m_tailNode.PhaseBelongTo = phaseBelongTo;
    }

    public void OpenOrCloseDetails()
    {
        if (m_curNode.HasClue)
        {
            if(m_curNode.ClueName == "Clue_Poster")
                HintDetails.SetActive(!HintDetails.activeInHierarchy);
        }
    }
}

