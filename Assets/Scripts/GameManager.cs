using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] iceCaps;
    public float time = 20f;

    private float interval;
    private float currTime = 0f;
    private int index = 0;
	// Use this for initialization
	void Start () {
        interval = time / iceCaps.Length;
	}
	
	// Update is called once per frame
	void Update () {
        currTime += Time.deltaTime;
        if (currTime >= interval)
        {
            currTime = 0;
            if (index < iceCaps.Length)
            {
                iceCaps[index].SetActive(false);
                index++;
            }
        }
	}
}
