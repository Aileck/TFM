﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserBehaviorRegister : MonoBehaviour
{
    // Start is called before the first frame update
    public float sampleTime = 0.5f;
    public GameObject user;

    private bool initiated;

    public JsonWriter json;

    public bool saved = false;


    void Start()
    {
        //if (setup = )
        //user = GameObject.FindGameObjectWithTag("MainCamera");

        json.setSampleRate(sampleTime);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(registerPositionAndRotation(sampleTime));
        if (LevelManager.end && !saved)
        {
            Debug.Log("Emd");
            json.GenerateJSON();
            saved = true;
            //Destroy(this);
        }

    }

    IEnumerator registerPositionAndRotation(float time)
    {

        Debug.Log("Go ahead " + LevelManager.end);
        while (!LevelManager.end)
        {
            yield return new WaitForSeconds(time);

            float posX = user.transform.position.x;
            float posY = user.transform.position.y;
            float posZ = user.transform.position.z;                                                 
            float rotX = user.transform.rotation.x;
            float rotY = user.transform.rotation.y;
            float rotZ = user.transform.rotation.z;

            json.setPositionRotation2(posX, posY, posZ, rotX, rotY, rotZ);

        }
    }

}
