using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public Transform parent;
    public float speed = 40f;
	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(parent.position, Vector3.up, speed * Time.deltaTime);
    }

}
