using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("IF ON FIRE")]
    [Tooltip("Actual situation")]
    [SerializeField] 
    bool onFire = false;

    [SerializeField]
    bool endgame = false;

    [SerializeField]
    int timeToFire = 5;

    [SerializeField]
    float samplerate = 0.5f;

    public Text fireText;
    public Text countDownText;

    public bool playerInOffice1;

    public JsonWriter json;




    // The script that manages all others
    public static LevelManager instance = null;
    void Start()
    {
        if (EditorApplication.isPlaying)
        {
            fireText.enabled = true;
            countDownText.enabled = true;
        }
        else {
            fireText.enabled = false;
            countDownText.enabled = false;
        }

        StartCoroutine(Countdown(timeToFire));
        StartCoroutine(StartAfterSceneLoad());

    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            endgame = true;

        }

        if (endgame) {
            json.GenerateJSON();
        }
    }

    public static bool inoffice1
    {
        get
        {
            return instance.playerInOffice1;
        }
        set
        {
            instance.playerInOffice1 = value;
        }
    }

    public static bool fire
    {
        get
        {
            return instance.onFire;
        }
        set
        {
            instance.onFire = value;
        }
    }

    public static bool end
    {
        get
        {
            return instance.endgame;
        }
        set
        {
            instance.endgame = value;
        }
    }

    public static JsonWriter jsonUtility
    {
        get
        {
            return instance.json;
        }

    }

    private IEnumerator StartAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();

        //Set up json
        //json.setID("");
        //json.setCondition("true, false");
        //json.setGroup("");
        json.setTimeBeforeTimber(timeToFire);
        json.setSampleRate(samplerate);

        //Avatar
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Avatar")) {
            Debug.Log("Le");
            json.createAvatar(g.name);
        }

        //Sign
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Sign"))
        {
            json.createSign(g.name, g.transform.position.x, g.transform.position.z);
        }

        //Exit
        GameObject e1 = GameObject.FindGameObjectWithTag("Door1");
        json.createExit("1", e1.transform.position.x, e1.transform.position.z);

        //Exit
        GameObject e2 = GameObject.FindGameObjectWithTag("Door2");
        json.createExit("2", e2.transform.position.x, e2.transform.position.z);

    }


        IEnumerator Countdown( int time)
    {
        while (time >= 0)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);
            string CountDownString = ts.ToString(@"mm\:ss");
            countDownText.text = CountDownString;
            yield return new WaitForSeconds(1);
            time--;
        }
        fire = true;
            fireText.text = "Fire";
            fireText.color = Color.red;
    }
    
}
