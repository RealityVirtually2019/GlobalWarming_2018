using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour {
    public AudioClip touchEarth;
    public AudioClip[] adClips;
    public AudioClip endClip;
    public AudioSource adSource;
    public Transform flood;
    public GameObject[] iceCaps;
    public float time = 20f;
    public float offset;
    public GameObject earth;
    public GameObject handLeft;
    public GameObject[] UIs;
    public Slider doomsDaySlider;
    public TextMesh info;
    public Transform water;
    public AudioSource introAudio;
    public AudioClip[] nextAudios;
    public OVRScreenFade screenFade;
    private float interval;
    private float currTime = 0f;
    private float currTime2 = 0f;
    private int index = 0;
    public bool hasEarth = true;
    private bool audioStarted = false;
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

    public bool played = false;
    public bool done = false;
    // Update is called once per frame
    void Update()
    {
        if (doomsDaySlider.value == doomsDaySlider.maxValue && !adSource.isPlaying && !done) {
            adSource.clip = endClip;
            adSource.Play();
            done = true;
        }
        if (screenFade.isFadingDone && !played)
        {
            played = true;
            introAudio.clip = touchEarth;
            introAudio.Play();
        }
        currTime += Time.deltaTime;
        if (currTime >= interval && screenFade.isFadingDone)
        {
            currTime = 0;
            if (index < iceCaps.Length)
            {
                iceCaps[index].SetActive(false);
                index++;
            }
        }
        currTime2 += Time.deltaTime;
        if (currTime2 >= 0.20 && hasEarth)
        {
            currTime2 = 0;
            doomsDaySlider.value += 1;
            info.text = "No polar bear in " + (doomsDaySlider.maxValue - doomsDaySlider.value) + " years";
            water.localScale = Vector3.one + new Vector3(1, 1, 1) * ((doomsDaySlider.value - doomsDaySlider.minValue) * 0.20f / (doomsDaySlider.maxValue - doomsDaySlider.minValue));
            flood.localPosition += new Vector3(0,0.005f,0) * ((doomsDaySlider.value - doomsDaySlider.minValue) * 2.80f / (doomsDaySlider.maxValue - doomsDaySlider.minValue));
        }


        if ((OVRInput.GetDown(OVRInput.Button.Three)) && hasEarth)
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
        StartCoroutine(DelayTime());
    }

    IEnumerator DelayTime()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(playAudioSequentially());
        
    }

    IEnumerator playAudioSequentially()
    {
        yield return null;

        //1.Loop through each AudioClip
        for (int i = 0; i < adClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            adSource.clip = adClips[i];

            //3.Play Audio
            adSource.Play();

            //4.Wait for it to finish playing
            while (adSource.isPlaying)
            {
                yield return null;
            }

            //5. Go back to #2 and play the next audio in the adClips array
        }
    }

    public void startUI(bool picked)
    {
        foreach(GameObject go in UIs)
        {
            go.SetActive(true);
        }
    }

    public void addYears(int value)
    {
        if (doomsDaySlider.maxValue + value > doomsDaySlider.value)
            doomsDaySlider.maxValue += value;
        info.text = "No polar bear in " + (doomsDaySlider.maxValue - doomsDaySlider.value) + " years";
        water.localScale = Vector3.one + new Vector3(1, 1, 1) * ((doomsDaySlider.value - doomsDaySlider.minValue) * 0.20f / (doomsDaySlider.maxValue - doomsDaySlider.minValue));
    }

    public void startAudio()
    {
        audioStarted = true;
    }
}
