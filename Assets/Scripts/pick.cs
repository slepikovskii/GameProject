using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider colider)
    {
        if (gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
