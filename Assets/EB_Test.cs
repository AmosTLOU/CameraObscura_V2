using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_Test : MonoBehaviour
{

    public bool banana;
    public bool knife;
    public bool shoes;
    public bool chef;
    public bool book;

    //public GameObject p1;
    //public GameObject p2;

    Photo p_banana;
    Photo p_knife;
    Photo p_shoes;
    Photo p_chefImage;
    Photo p_book;
    // Start is called before the first frame update
    void Start()
    {
        p_banana = new Photo(Application.dataPath + "/SavedFiles/Photos/banana.png");
        p_banana.SuspectName = "Chef";

        p_knife = new Photo(Application.dataPath + "/SavedFiles/Photos/knife");
        p_knife.SuspectName = "Killer";

        p_shoes = new Photo(Application.dataPath + "/SavedFiles/Photos/shoes");
        p_shoes.SuspectName = "Dancer";

        p_book = new Photo(Application.dataPath + "/SavedFiles/Photos/book");
        p_book.SuspectName = "Others";

        p_chefImage = new Photo(Application.dataPath + "/SavedFiles/Photos/chef");
        p_chefImage.SuspectName = "Chef";
        p_chefImage.IsSuspect = true;

        //Debug.Log(p1.transform.localPosition);

        //Debug.Log(p2.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (banana)
        {
            banana = false;
            FindObjectOfType<EvidenceBoard>().AddClues(p_banana);
        }

        if (knife)
        {
            knife = false;
            FindObjectOfType<EvidenceBoard>().AddClues(p_knife);
        }

        if (shoes)
        {
            shoes = false;
            FindObjectOfType<EvidenceBoard>().AddClues(p_shoes);
        }

        if (chef)
        {
            chef = false;
            FindObjectOfType<EvidenceBoard>().AddClues(p_chefImage);
        }

        if (book)
        {
            book = false;
            FindObjectOfType<EvidenceBoard>().AddClues(p_book);
        }


    }
}
