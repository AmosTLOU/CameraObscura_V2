using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;
using System.IO;
using Core;
using EventSystem.Data;

// Use DulNode structure to view and scroll photos
//[HideInInspector]
//public class DuLNode
//{
//    public string StrName { get; set; }
//    public bool HasClue { get; set; }
//    public string ClueName { get; set; }
//    public Phase PhaseBelongTo { get; set; }
//    public Vector3 ViewPos { get; set; }
//    public DuLNode Prev { get; set; }
//    public DuLNode Next { get; set; }

//    public DuLNode(string i_strName, Phase i_phase){
//        StrName = i_strName;
//        HasClue = false;
//        ClueName = "";
//        PhaseBelongTo = i_phase;
//        ViewPos = Vector3.zero;
//        Prev = null;
//        Next = null;
//    }
//}

[HideInInspector]
public class Photo
{
    public string FileName { get; set; }
    public bool HasClue { get; set; }
    public bool IsSuspect { get; set; }
    public string ClueName { get; set; }
    public Phase PhaseBelongTo { get; set; }
    public string SuspectName { get; set; }
    public Vector3 ViewPos { get; set; }
    public Photo(string i_FileName)
    {
        FileName = i_FileName;
        IsSuspect = false;
        HasClue = false;
        ClueName = "";
        SuspectName = "";
        PhaseBelongTo = Phase.NullPhase;
        ViewPos = Vector3.zero;
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
    List<Photo> m_photos;
    float m_scaleX;
    float m_scaleY;
    //DuLNode m_headNode;
    //DuLNode m_tailNode;
    //DuLNode m_curNode;
    int m_cntPhoto;
    int m_curIndex;
    string m_pathPhotos;


    private void Start()
    {
        m_gameManager = FindObjectOfType<GameManager>();
        m_photos = new List<Photo>();

        TextIndexOfPhoto.text = "";
        HintDetails.SetActive(false);

        // since the viewPos is normalized, we need the scale to put the red cirlce on the right position
        m_scaleX = (Screen.width + ImageDisplay.GetComponent<RectTransform>().rect.width)/2;
        m_scaleY = (Screen.height + ImageDisplay.GetComponent<RectTransform>().rect.height)/2;
        //m_headNode = null;
        //m_tailNode = null;
        m_cntPhoto = 0;
        m_curIndex = -1;

        // Clear the photos from the last time when game restarts
        m_pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(m_pathPhotos)) 
        {
            Debug.Log("Clearing Old Photos.");
            //Directory.Delete(m_pathPhotos, true); 
        }
        Debug.Log("Creating a new gallery.");
        Directory.CreateDirectory(m_pathPhotos);
    }

    // Save the screenshot
    public void Capture()
    {
        //string strDateAndTime = System.DateTime.Now.ToString();
        ScreenCapture.CaptureScreenshot(m_pathPhotos + m_cntPhoto + ".png");
        m_photos.Add(new Photo(m_cntPhoto.ToString()));
        //Debug.Log("Capture");
        m_cntPhoto++;
    }

    // Show a full picture
    public void Show(int index)
    {
        TextHintMessage.gameObject.SetActive(false);
        ImageRedCircle.gameObject.SetActive(false);
        HintDetails.SetActive(false);
        if (0 < m_cntPhoto && 0 <= index)
        {
            byte[] bytes;
            bytes = System.IO.File.ReadAllBytes(m_pathPhotos + m_photos[index].FileName + ".png");
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
            if (m_photos[index].HasClue && m_photos[index].PhaseBelongTo <= m_gameManager.GetPhase())
            {
                TextHintMessage.gameObject.SetActive(true);
                ImageRedCircle.gameObject.SetActive(true);
                ImageRedCircle.rectTransform.anchoredPosition = new Vector2((2f * m_photos[index].ViewPos.x - 1f) * m_scaleX, (2f * m_photos[index].ViewPos.y - 1f) * m_scaleY);
            }            
        }
    }

    public void EnterGallery()
    {
        //m_curNode = m_tailNode;

        if (m_cntPhoto <= 0)
            return;
        m_curIndex = m_cntPhoto-1;
        Show(m_curIndex);
    }

    public void ShowNextPhoto()
    {
        //if (m_curNode == null)
        //    return;

        if (m_cntPhoto <= 0)
            return;
        m_curIndex++;
        m_curIndex %= m_cntPhoto;
        Show(m_curIndex);
    }

    public void ShowPrevPhoto()
    {
        //if (m_curNode == null)
        //    return;

        if (m_cntPhoto <= 0)
            return;
        m_curIndex--;
        if (m_curIndex < 0)
            m_curIndex = m_cntPhoto - 1;
        Show(m_curIndex);
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
        //if(m_tailNode == null)
        //{
        //    Debug.Log("Clue is captured, but fail to take a photo.");
        //    return;
        //}

        if (m_cntPhoto <= 0)
        {
            Debug.Log("Clue is captured, but fail to take a photo.");
            return;
        }
        int lastIndex = m_cntPhoto - 1;
        m_photos[lastIndex].HasClue = true;
        m_photos[lastIndex].ViewPos = viewPos;
        m_photos[lastIndex].ClueName = clueName;
        m_photos[lastIndex].PhaseBelongTo = phaseBelongTo;
    }

    public void OpenOrCloseDetails()
    {
        if (m_photos[m_curIndex].HasClue)
        {
            if(m_photos[m_curIndex].ClueName == "Clue_Poster")
                HintDetails.SetActive(!HintDetails.activeInHierarchy);
        }
    }
}

