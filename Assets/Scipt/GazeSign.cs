using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class GazeSign : MonoBehaviour, IGazeFocusable
{
    public bool isTiming = false;
    public float startTime = 0f;
    public float timer = 0f;
    public float stopLooktimer = 0f;

    public float threshold = 2f;

    public bool looking = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    void Update()
    {
        if (isTiming)
        {
            timer += Time.deltaTime;

            if (!looking) 
            {
                stopLooktimer += Time.deltaTime;
                if (stopLooktimer >= threshold)
                {
                    StopTiming(); 
                }
            }


        }
    }

    public void StartTiming()
    {
        isTiming = true;
        timer = 0f;
        startTime = Time.time;
    }

    public void StopTiming()
    {
        string id = this.GetComponent<ID>().id;
        Debug.Log("End looking " + this.gameObject.GetComponent<ID>().id + " duratopm  " + timer);

        LevelManager.jsonUtility.setTimer(id, startTime, timer - stopLooktimer);

        isTiming = false;
        timer = 0f;
        stopLooktimer = 0f;


    }


    public void GazeFocusChanged(bool hasFocus)
    {
        if (hasFocus && isTiming == false) {
            StartTiming();
            Debug.Log("Now looking " + this.gameObject.GetComponent<ID>().id);
        }
        looking = hasFocus;
    }
}
