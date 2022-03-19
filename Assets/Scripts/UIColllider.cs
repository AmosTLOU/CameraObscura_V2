using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColllider : MonoBehaviour
{
    Transform m_parent;
    private void Start()
    {
        m_parent = transform.parent;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Bound") return;
        //Debug.Log(collision.gameObject.name);
        if(collision.transform.parent != m_parent)
        {
            collision.transform.parent.SetParent(m_parent);
            EvidenceBoard.Instance.SortPhoto(this.m_parent);
        }

    }
}
