using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour {

    public string tagName;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagName)
        {
            Destroy(other.gameObject);
        }
        else
        {
            other.GetComponent<Rigidbody>().AddForce(Vector3.up + Vector3.right, ForceMode.Impulse);
        }
    }
}
