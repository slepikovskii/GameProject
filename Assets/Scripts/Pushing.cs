using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour {

    public AudioSource StuckSource;

    // Use this for initialization
    void Start () {
        StuckSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.GetComponent<Rigidbody>().AddForce(Vector3.forward*2f);
            StuckSource.Play();
        }
    }
}
