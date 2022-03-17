using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core;
using CharacterInfo = Characters.Data.CharacterInfo;

public class EvidenceBoard : Core.SingletonBehaviour<EvidenceBoard>
{

    List<Photo> m_clues;
    List<CharacterInfo> m_characters;

    [SerializeField] GameObject chef;
    [SerializeField] GameObject dancer;
    [SerializeField] GameObject none;

    [SerializeField] float picsDistance;
    [SerializeField] Vector2 pictureSize = new Vector2(50, 50);

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
                default:
                    //evidences not related to any victims
                    RandomPlace(i_photo);
                    break;

            }
        }
    }

    void AddPhotoToVictim(GameObject i_victim, Photo i_photo)
    {
        GameObject NewObj = new GameObject(); //Create the GameObject
        RawImage NewImage = NewObj.AddComponent<RawImage>(); //Add the Image Component script
        NewImage.texture = LoadTexture(i_photo.FileName);
        NewObj.transform.SetParent(i_victim.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject

        int childcount = i_victim.GetComponent<RectTransform>().childCount;

        float degree = Mathf.PI * 2 / childcount;

        for(int i = 0; i < childcount; i++)
        {
            RectTransform child = i_victim.GetComponent<RectTransform>().GetChild(i) as RectTransform;
            child.localPosition = new Vector3(picsDistance * Mathf.Cos(degree * i), picsDistance * Mathf.Sin(degree * i), 0);//Vector3(Mathf.Cos(degree * i), Mathf.Sin(degree * i), 0);
            child.localRotation = new Quaternion(0, 0, 0, 0);
            child.localScale = new Vector3(1, 1, 1);
            child.sizeDelta = pictureSize;
        }
    }

    Texture2D LoadTexture(string i_fileName)
    {
        byte[] bytes;
        bytes = System.IO.File.ReadAllBytes(i_fileName + ".png");
        Texture2D textureLoad = new Texture2D(1, 1);
        textureLoad.LoadImage(bytes);
        if (textureLoad)
        {
            return textureLoad;
        }
        else
        {
            Log.E("failed to load picture");
            return null;
        }
    }

    void RandomPlace(Photo i_photo)
    {
        GameObject NewObj = new GameObject(); //Create the GameObject
        RawImage NewImage = NewObj.AddComponent<RawImage>(); //Add the Image Component script
        NewImage.texture = LoadTexture(i_photo.FileName);
        NewObj.transform.SetParent(this.transform); //Assign the newly created Image GameObject as a Child of the Parent Panel.
        NewObj.SetActive(true); //Activate the GameObject

        RectTransform rect = NewObj.GetComponent<RectTransform>();
        float width = this.GetComponent<RectTransform>().sizeDelta.x / 2;
        float height = this.GetComponent<RectTransform>().sizeDelta.y / 2;

        rect.sizeDelta = pictureSize;
        rect.localRotation = new Quaternion(0, 0, 0, 0);
        rect.localScale = new Vector3(1, 1, 1);
        rect.localPosition = new Vector3(Random.Range(- width + pictureSize.x, width - pictureSize.x), Random.Range(-height + pictureSize.y, height - pictureSize.y), 0);
    }
}
