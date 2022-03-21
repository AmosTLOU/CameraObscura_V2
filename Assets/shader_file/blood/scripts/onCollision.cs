using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class onCollision : MonoBehaviour
{
    MeshRenderer bloodmat;
    Material bloodg;

    private void Start()
    {
        bloodmat = gameObject.GetComponent<MeshRenderer>();

    }


    public void OnParticleCollision(GameObject other)
    {
        Debug.Log("coliide"); //trial to detect collision
        //bloodmat = GetComponent<MeshRenderer>();

        Material[] materials = bloodmat.materials;
        materials[1] = null;
        bloodmat.materials = materials; 
        


        
    }
 
}
