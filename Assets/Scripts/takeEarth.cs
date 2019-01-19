﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeEarth : MonoBehaviour {
    public GameObject earthUI;
    protected GameManager MyGameManager = null;
    private bool hasEarth = false;
    // Use this for initialization
    void Start () {
        MyGameManager = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hands" && !hasEarth)
        {
            gameObject.SetActive(false);
            MyGameManager.pickedEarth(true);
            MyGameManager.startUI(true);
            hasEarth = true;
            earthUI.SetActive(false);

        }

        if (other.tag == "hands" && hasEarth)
        {
            gameObject.transform.RotateAround(Vector3.up, 20f);
        }

    }
}
