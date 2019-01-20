using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class onButtonSelected : MonoBehaviour {

    public bool isButtonOn = true;
    public Color onColor = Color.white;
    public Color offColor = Color.green;
    public GameObject toHide;
    public GameObject toShow;
    public TextMeshPro textInfo; 
    protected GameManager MyGameManager;
    private Text text;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        MyGameManager = GameManager.Instance;
        audioSource = GetComponent<AudioSource>();
        /*if (toShow != null)
        {
            toShow.SetActive(true);
            UseIDtoReadCorrectFile();
        }*/
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hands")
        {
            isButtonOn = !isButtonOn;
            audioSource.Play();
            if (isButtonOn)
            {
                GetComponent<Renderer>().material.color = onColor;
                MyGameManager.addYears(-100);
                if (toHide != null)
                    toHide.SetActive(true);
                if (toShow != null)
                    toShow.SetActive(false);
                
            }
            else
            {
                GetComponent<Renderer>().material.color = offColor;
                MyGameManager.addYears(100);
                if (toHide != null)
                    toHide.SetActive(false);
                if (toShow != null)
                {
                    toShow.SetActive(true);
                    UseIDtoReadCorrectFile();
                }
            }
        }
    }

    void UseIDtoReadCorrectFile()
    {
        string fn = gameObject.tag + ".txt";
        string contents = ReadStringFromFile(fn);
        Debug.Log("********contents: " + contents);
        //DisplayString(ReadStringFromFile(fn));
        //DisplayString("This is text to display on screen");
        textInfo.text = contents;

    }

    static string ReadStringFromFile(string filename)
    {
        string thePath = "Assets/Text/" + filename;
        Debug.Log("********in ReadString*****");
        StreamReader reader = new StreamReader(thePath);
        ////////////Debug.Log(reader.ReadToEnd());
        string filecontents = reader.ReadToEnd();
        Debug.Log(reader.ReadToEnd());
        reader.Close();
        Debug.Log("*********ReadInCorrectly: " + filecontents);
        return filecontents;
    }


    void DisplayString(string s)
    {
        Debug.Log("s:" + s);
        // Load the Arial font from the Unity Resources folder.
        Font arial;
        arial = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");


        // Create Canvas GameObject.
        GameObject canvasGO = new GameObject();
        canvasGO.name = "Canvas";
        canvasGO.AddComponent<Canvas>();
        canvasGO.AddComponent<CanvasScaler>();
        canvasGO.AddComponent<GraphicRaycaster>();


        // Get canvas from the GameObject.
        Canvas canvas;
        canvas = canvasGO.GetComponent<Canvas>();
        //canvas.renderMode = RenderMode.ScreenSpaceOverlay;//This shows it in front
        canvas.renderMode = RenderMode.WorldSpace;  //NOT YET WORKING **********************
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;//This shows it in front

        // mk Create rectTransform for the canvas
        RectTransform canvasrectTransform;
        canvasrectTransform = canvas.GetComponent<RectTransform>();
        Vector3 vectPositionCanvas;
        vectPositionCanvas.x = 5;
        vectPositionCanvas.y = 5;
        vectPositionCanvas.z = 5;
        Vector3 vectScaleCanvas;
        vectScaleCanvas.x = 0.5f;
        vectScaleCanvas.y = 0.5f;
        vectScaleCanvas.z = 0.5f;
        vectPositionCanvas.Scale(vectScaleCanvas);
        Quaternion quatRotCanvas;
        quatRotCanvas.x = 0;
        quatRotCanvas.y = 0;
        quatRotCanvas.z = 0;
        quatRotCanvas.w = 0;
        canvasrectTransform.SetPositionAndRotation(vectPositionCanvas, quatRotCanvas);

        // Create the Text GameObject.
        GameObject textGO = new GameObject();
        textGO.transform.parent = canvasGO.transform;
        textGO.AddComponent<Text>();

        // Set Text component properties.
        text = textGO.GetComponent<Text>();
        text.font = arial;
        text.text = s; //use contents from the file
        text.fontSize = 18;
        //text.alignment = TextAnchor.MiddleCenter;
        text.alignment = TextAnchor.UpperRight;

        // Provide Text position and size using RectTransform.
        RectTransform rectTransform;
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        //rectTransform.localPosition = new Vector3(this., this.y, this.z);
        rectTransform.sizeDelta = new Vector2(400, 100);  //600,200

        Debug.Log("******at end of DisplayString()*******:" + text.text);

    }

}
