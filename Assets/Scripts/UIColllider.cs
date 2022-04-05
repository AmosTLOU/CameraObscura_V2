using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColllider : MonoBehaviour
{
    Transform suspect;
    public GameObject lines;
    public GameObject linePrefab;
    private void Start()
    {
        suspect = transform.parent;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (!collision.gameObject.name.Contains("Photo")) return;
        
        if(collision.transform.parent != suspect)
        {
            collision.transform.parent.SetParent(suspect);

            // Draw Line
            // ---------
            lines.transform.localPosition = new Vector3(0, 0, 0);
            collision.transform.parent.GetComponent<UI.Draggable>().AddLine();

            EvidenceBoard.Instance.SortPhoto(this.suspect);
        }


    }

    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("Exit");

        collision.transform.parent.SetParent(suspect.parent);
        collision.transform.parent.GetComponent<UI.Draggable>().DeleteLine();
    }
}
