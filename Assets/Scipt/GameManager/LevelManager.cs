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

    string exit;

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
        else
        {
            fireText.enabled = false;
            countDownText.enabled = false;
        }

        //Initialize JSON
        StartCoroutine(StartAfterSceneLoad());

        //Count down
        StartCoroutine(Countdown(timeToFire));

        //Register path user
        StartCoroutine(RegisterPathUser());

        //Register path avatar
        StartCoroutine(RegisterPathAvatar());


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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            endgame = true;

        }

        if (endgame)
        {
            ExitChoice(exit);
            json.GenerateJSON();
        }
    }

    public void ExitChoice(string i) {
        json.setPlayerExit(i);
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


    public static string exitChoice
    {
        get
        {
            return instance.exit;
        }
        set
        {
            instance.exit = value;
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

        GameObject parameters = GameObject.FindGameObjectWithTag("Parameters");
        if  (parameters != null){
            Debug.Log("Parameter Loades");
            json.setID(parameters.GetComponent<ExperimentParameter>().id);
            json.setCondition(parameters.GetComponent<ExperimentParameter>().avatar, parameters.GetComponent<ExperimentParameter>().training);
            json.setGroup(parameters.GetComponent<ExperimentParameter>().group);

            //Handle condition:avatar
            if (!parameters.GetComponent<ExperimentParameter>().avatar) {
                foreach (GameObject g in GameObject.FindGameObjectsWithTag("Avatar"))
                {
                    g.SetActive(false);
                }
            }
        }
        else
        {
            Debug.LogError("Critical error, no group detected");
        }
        //Set up json

        json.setTimeBeforeTimber(timeToFire);
        json.setSampleRate(samplerate);

        //User
        GameObject user = GameObject.FindGameObjectWithTag("Player");
        json.setUserHeight(user.transform.position.y);

        //Avatar
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Avatar"))
        {
            json.createAvatar(g.GetComponent<ID>().id);
        }

        //Sign
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Sign"))
        {
            json.createSign(g.GetComponent<ID>().id, g.transform.position.x, g.transform.position.z);
        }

        //Exit
        GameObject e1 = GameObject.FindGameObjectWithTag("Door1");
        json.createExit("1", e1.transform.position.x, e1.transform.position.z);

        //Exit
        GameObject e2 = GameObject.FindGameObjectWithTag("Door2");
        json.createExit("2", e2.transform.position.x, e2.transform.position.z);

    }


    IEnumerator Countdown(int time)
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

    IEnumerator RegisterPathUser()
    {
        while (!end)
        {
            yield return new WaitForSeconds(samplerate);

            GameObject user = GameObject.FindGameObjectWithTag("Player");

            float posX = user.transform.position.x;
            float posZ = user.transform.position.z;

            float rotX = user.transform.rotation.eulerAngles.x;
            float rotY = user.transform.rotation.eulerAngles.y;
            float rotZ = user.transform.rotation.eulerAngles.z;

            json.setPositionRotation2(posX, posZ, rotX, rotY, rotZ, Time.time);

        }

    }

    IEnumerator RegisterPathAvatar()
    {
        while (!end)
        {
            yield return new WaitForSeconds(samplerate);

            foreach (GameObject g in GameObject.FindGameObjectsWithTag("Avatar"))
            {
                float posX = g.transform.position.x;
                float posZ = g.transform.position.z;

                float rotX = g.transform.rotation.eulerAngles.x;
                float rotY = g.transform.rotation.eulerAngles.y;
                float rotZ = g.transform.rotation.eulerAngles.z;

                json.setPositionRotationAvatar(g.GetComponent<ID>().id, posX, posZ, 
                                                rotX, rotY, rotZ, 
                                                Time.time);
            }


        }

    }
}
