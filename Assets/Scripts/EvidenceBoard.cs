using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using EventSystem.Data;
using CharacterInfo = Characters.Data.CharacterInfo;
using Utility;

public class EvidenceBoard : Core.SingletonBehaviour<EvidenceBoard>
{

    List<Photo> m_clues = new List<Photo>();
    List<CharacterInfo> m_characters;

    [SerializeField] GameObject chef;
    [SerializeField] GameObject dancer;
    [SerializeField] GameObject killer;
    [SerializeField] GameObject others;

    //[SerializeField] RectTransform chefBound;

    [SerializeField] GameObject photoPrefab;

    [SerializeField] float picsDistance;
    [SerializeField] Vector2 pictureSize = new Vector2(50, 50);

    public void OnEventClueFound(IGameEventData eData) {
        if (!Utils.TryConvertVal(eData, out ClueFoundEventData clueData)){
            Log.Err("Error converting value");
            return;
        }
        // Todo: @Anna Adding Clue to the list of clues
        Log.Info($"Clue Found!! File = {clueData.clue.Photo.FileName} ; CharacterId = {clueData.CharacterId}", Constants.TagTimeline);
        StartCoroutine(AddClue(clueData.clue.Photo, clueData.CharacterId));
    }


    IEnumerator AddClue(Photo photo, string characterId){

        yield return new WaitForSeconds(2);
        switch (characterId){
            case "chef":
                AddPhotoToVictim(chef, photo);
                break;
            case "dancer":
                AddPhotoToVictim(dancer, photo);
                break;
        }
    }
    
    public void AddClues(Photo i_photo)
    {
        if (i_photo.IsSuspect)
        {
            switch (i_photo.SuspectName)
            {
                case "Chef":
                    chef.GetComponent<RawImage>().texture = LoadTexture(i_photo.FileName);
                    break;
                case "Dancer":
                    dancer.GetComponent<RawImage>().texture = LoadTexture(i_photo.FileName);
                    break;
                default:
                    break;

            }
        }
        else
        {
            switch (i_photo.SuspectName)
            {
                case "Chef":
                    AddPhotoToVictim(chef, i_photo);
                    break;
                case "Dancer":
                    AddPhotoToVictim(dancer, i_photo);
                    break;
                case "Killer":
                    AddPhotoToRow(killer, i_photo);
                    break;
                default:
                    AddPhotoToRow(others, i_photo);
                    //evidences not related to any victims
                    //RandomPlace(i_photo);
                    break;

            }
        }
    }

    //public void ImageDrop(RectTransform rectTransform)
    //{
    //    //Log.I("enter image drop");
    //    if (rectTransform.Overlaps(chefBound)) {
    //        Log.I($"Incoming:: {printRT(rectTransform)}");
    //        Log.I($"ChefBounds:: {printRT(chefBound)}");
    //        Log.I("overlap!".Color("green"));
    //    }
        
    //}
    private string printRT(RectTransform rt)
    {
        return $"Center={rt.rect.center} position={rt.position} worldPos= Height = {rt.sizeDelta.x} Width = {rt.sizeDelta.y}";
    }

    void AddPhotoToVictim(GameObject i_victim, Photo i_photo)
    {
        GameObject NewObj = Instantiate(photoPrefab, i_victim.transform, false); //Create the GameObject
        //RawImage NewImage = NewObj.AddComponent<RawImage>(); //Add the Image Component script
        NewObj.GetComponent<RawImage>().texture = LoadTexture(i_photo.FileName);
        NewObj.GetComponent<UI.Draggable>().AddLine();
        //NewObj.AddComponent<UI.Draggable>();
        //NewObj.transform.SetParent(i_victim.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        //NewObj.SetActive(true); //Activate the GameObject

        SortPhoto(i_victim.transform);
    }

    void AddPhotoToRow(GameObject i_obj, Photo i_photo)
    {
        GameObject NewObj = Instantiate(photoPrefab, i_obj.transform, false);
        NewObj.GetComponent<RawImage>().texture = LoadTexture(i_photo.FileName);

        RectTransform rect = NewObj.GetComponent<RectTransform>();
        float width = pictureSize.x / 2;
        int childNum = i_obj.transform.childCount;
        float x = -i_obj.GetComponent<RectTransform>().sizeDelta.x/2 + width + (childNum - 1) * (pictureSize.x + 5);
        Debug.Log(childNum + " / " + x);
        //float height = this.GetComponent<RectTransform>().sizeDelta.y / 2;

        rect.sizeDelta = pictureSize;
        rect.localRotation = new Quaternion(0, 0, 0, 0);
        rect.localScale = new Vector3(1, 1, 1);
        rect.localPosition = new Vector3(x, 0, 0);
    }

    public void SortPhoto(Transform i_victim)
    {
        int childcount = i_victim.GetComponent<RectTransform>().childCount - 1;

        float degree = Mathf.PI * 2 / childcount;

        for (int i = 0; i <= childcount; i++)
        {
            RectTransform child = i_victim.GetComponent<RectTransform>().GetChild(i) as RectTransform;
            
            if (!child.name.Contains("Photo")) continue;
            child.localPosition = new Vector3(picsDistance * Mathf.Cos(degree * i), picsDistance * Mathf.Sin(degree * i), 0);//Vector3(Mathf.Cos(degree * i), Mathf.Sin(degree * i), 0);
            child.localRotation = new Quaternion(0, 0, 0, 0);
            child.localScale = new Vector3(1, 1, 1);
            child.sizeDelta = pictureSize;
            child.GetComponent<UI.Draggable>().UpdateLine();
        }
    }

    Texture2D LoadTexture(string i_fileName)
    {
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(i_fileName);
        Texture2D textureLoad = new Texture2D(1, 1);
        textureLoad.LoadImage(bytes);
        if (textureLoad)
        {
            return textureLoad;
        }
        else
        {
            Log.Err("failed to load picture");
            return null;
        }
    }

    void RandomPlace(Photo i_photo)
    {
        GameObject NewObj = Instantiate(photoPrefab, this.transform, false); ; //Create the GameObject
        //RawImage NewImage = NewObj.AddComponent<RawImage>(); //Add the Image Component script
        NewObj.GetComponent<RawImage>().texture = LoadTexture(i_photo.FileName);

        RectTransform rect = NewObj.GetComponent<RectTransform>();
        float width = this.GetComponent<RectTransform>().sizeDelta.x / 2;
        float height = this.GetComponent<RectTransform>().sizeDelta.y / 2;

        rect.sizeDelta = pictureSize;
        rect.localRotation = new Quaternion(0, 0, 0, 0);
        rect.localScale = new Vector3(1, 1, 1);
        rect.localPosition = new Vector3(Random.Range(- width + pictureSize.x, width - pictureSize.x), Random.Range(-height + pictureSize.y, height - pictureSize.y), 0);
    }

    public void OnClickKiller()
    {
        EndScreenNew.Instance.ShowWinScreen();
        FindObjectOfType<MenuInputManager>().CanvasHUD.SetActive(false);
    }

    public void OnClickNonKiller()
    {
        EndScreenNew.Instance.ShowLoseScreen();
        FindObjectOfType<MenuInputManager>().CanvasHUD.SetActive(false);
    }
}
