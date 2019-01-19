using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onButtonSelected : MonoBehaviour {

    public bool isButtonOn = true;
    public Color onColor = Color.white;
    public Color offColor = Color.green;
    public GameObject toHide;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hands")
        {
            isButtonOn = !isButtonOn;
            if (isButtonOn)
            {
                GetComponent<Renderer>().material.color = onColor;
                if (toHide != null)
                    toHide.SetActive(true);
            }
            else
            {
                GetComponent<Renderer>().material.color = offColor;
                if (toHide != null)
                    toHide.SetActive(false);
            }
        }
    }
}
