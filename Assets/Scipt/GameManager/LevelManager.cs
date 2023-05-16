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

    public Text fireText;
    public Text countDownText;

    public bool playerInOffice1;




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
