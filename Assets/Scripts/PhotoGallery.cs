using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Windows;
using System.IO;

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

    GameManager _gameManager;

    float _scaleX;
    float _scaleY;
    DuLNode _headNode;
    DuLNode _tailNode;
    DuLNode _curNode;
    int _cntPhoto;
    int _curIndex;
    string _pathPhotos;


    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();

        TextIndexOfPhoto.text = "";
        HintDetails.SetActive(false);

        // since the viewPos is normalized, we need the scale to put the red cirlce on the right position
        _scaleX = (Screen.width + ImageDisplay.GetComponent<RectTransform>().rect.width)/2;
        _scaleY = (Screen.height + ImageDisplay.GetComponent<RectTransform>().rect.height)/2;
        _headNode = null;
        _tailNode = null;
        _cntPhoto = 0;
        _curIndex = -1;

        // Clear the photos from the last time when game restarts
        _pathPhotos = Application.dataPath + "/SavedFiles/Photos/";
        if (Directory.Exists(_pathPhotos)) 
        {
            Debug.Log("Clearing Old Photos.");
            Directory.Delete(_pathPhotos); 
        }
        Debug.Log("Creating a new gallery.");
        Directory.CreateDirectory(_pathPhotos);
    }

    // Save the screenshot
    public void Capture()
    {
        //string strDateAndTime = System.DateTime.Now.ToString();
        ScreenCapture.CaptureScreenshot(_pathPhotos + _cntPhoto + ".png");
        if (_headNode == null)
        {
            _headNode = new DuLNode(_cntPhoto.ToString(), Phase.NullPhase);
            _tailNode = _headNode;
            _headNode.Next = _tailNode;
            _tailNode.Prev = _headNode;
        }
        else
        {
            _tailNode.Next = new DuLNode(_cntPhoto.ToString(), Phase.NullPhase);
            _tailNode.Next.Prev = _tailNode;
            _tailNode = _tailNode.Next;
            _tailNode.Next = _headNode;
            _headNode.Prev = _tailNode;
        }
        _cntPhoto++;
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
            bytes = System.IO.File.ReadAllBytes(_pathPhotos + node.StrName + ".png");
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
            TextIndexOfPhoto.text = (index + 1) + " / " + _cntPhoto;
            if (node.HasClue && node.PhaseBelongTo <= _gameManager.GetPhase())
            {
                TextHintMessage.gameObject.SetActive(true);
                ImageRedCircle.gameObject.SetActive(true);
                ImageRedCircle.rectTransform.anchoredPosition = new Vector2((2f * node.ViewPos.x - 1f) * _scaleX, (2f * node.ViewPos.y - 1f) * _scaleY);
            }            
        }
    }

    public void EnterGallery()
    {
        _curNode = _tailNode;
        _curIndex = _cntPhoto-1;
        Show(_curNode, _curIndex);
    }

    public void ShowNextPhoto()
    {
        if (_curNode == null)
            return;
        _curIndex++;
        _curIndex %= _cntPhoto;
        _curNode = _curNode.Next;
        Show(_curNode, _curIndex);
    }

    public void ShowPrevPhoto()
    {
        if (_curNode == null)
            return;
        _curIndex--;
        if (_curIndex < 0)
            _curIndex = _cntPhoto - 1;
        _curNode = _curNode.Prev;
        Show(_curNode, _curIndex);
    }

    public void AddPromptToPhoto(Vector3 viewPos, string clueName, Phase phaseBelongTo)
    {
        if(_tailNode == null)
        {
            Debug.Log("Clue is captured, but fail to take a photo.");
            return;
        }
        _tailNode.HasClue = true;
        _tailNode.ViewPos = viewPos;
        _tailNode.ClueName = clueName;
        _tailNode.PhaseBelongTo = phaseBelongTo;
    }

    public void OpenOrCloseDetails()
    {
        if (_curNode.HasClue)
        {
            if(_curNode.ClueName == "Clue_Poster")
                HintDetails.SetActive(!HintDetails.activeInHierarchy);
        }
    }
}

