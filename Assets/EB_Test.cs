using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EB_Test : MonoBehaviour
{

    public bool banana;
    public bool knife;
    public bool shoes;

    Photo p_banana;
    Photo p_knife;
    Photo p_shoes;
    // Start is called before the first frame update
    void Start()
    {
        p_banana = new Photo(Application.dataPath + "/SavedFiles/Photos/banana");
        p_banana.SuspectName = "Chef";

        p_knife = new Photo(Application.dataPath + "/SavedFiles/Photos/knife");
        p_knife.SuspectName = "none";

        p_shoes = new Photo(Application.dataPath + "/SavedFiles/Photos/shoes");
        p_shoes.SuspectName = "Dancer";
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
    }
}
