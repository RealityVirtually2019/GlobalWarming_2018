using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour {

    public GameObject[] iceCaps;
    public float time = 20f;
    public float offset;
    public GameObject earth;
    public GameObject handLeft;
    public GameObject[] UIs;
    public Slider doomsDaySlider;
    public TextMesh info;
    public Transform water;

    private float interval;
    private float currTime = 0f;
    private float currTime2 = 0f;
    private int index = 0;
    public bool hasEarth = true;

    private static GameManager _Instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = (GameManager)FindObjectOfType(typeof(GameManager));
            }
            return _Instance;
        }
    }

    // Use this for initialization
    void Start () {
        interval = time / iceCaps.Length;
	}

    // Update is called once per frame
    void Update()
    {
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
        currTime2 += Time.deltaTime;
        if (currTime2 >= 1 && hasEarth)
        {
            currTime = 0;
            doomsDaySlider.value += 1;
            info.text = "World ends in " + (doomsDaySlider.maxValue - doomsDaySlider.value) + " years";
            water.localScale = Vector3.one + new Vector3(1, 1, 1) * ((doomsDaySlider.value - doomsDaySlider.minValue) * 0.20f / (doomsDaySlider.maxValue - doomsDaySlider.minValue));
        }


        if (OVRInput.GetDown(OVRInput.Button.Three) && hasEarth)
        {
            earth.transform.position = handLeft.transform.position + handLeft.transform.up * offset;
            earth.SetActive(!earth.activeSelf);
            earth.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public void pickedEarth(bool picked)
    {
        hasEarth = picked;
        doomsDaySlider.gameObject.SetActive(true);
        info.text += doomsDaySlider.maxValue - doomsDaySlider.minValue + " years";
    }

    public void startUI(bool picked)
    {
        foreach(GameObject go in UIs)
        {
            go.SetActive(true);
        }
    }
}
