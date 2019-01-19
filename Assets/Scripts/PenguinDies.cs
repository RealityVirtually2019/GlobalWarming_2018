using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinDies : MonoBehaviour {

    public string tagName;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagName)
        {
            Debug.Log("Penguin Died");
        }
    }
}
