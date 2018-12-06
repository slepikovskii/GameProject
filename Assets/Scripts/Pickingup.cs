using System.Collections.Generic;
using UnityEngine;

public class Pickingup : MonoBehaviour
{

    public Transform Desti;

    void OnMouseDown()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        this.transform.position = Desti.position;
        this.transform.rotation = Desti.rotation;       
        this.transform.parent = GameObject.Find("Destination").transform;

    }

    void OnMouseUp()
    {
        GetComponent<BoxCollider>().enabled = true;
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().isKinematic = false;
    }



}
