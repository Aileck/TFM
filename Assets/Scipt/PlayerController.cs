﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isInOffice1 = true;
    public GameObject endNotice;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Office1")
        {
            Debug.Log("Scene 1");
        }
        else {
            Debug.Log("Scene2");
        }

        if (other.tag == "EndFloor") {
            LevelManager.end = true;

            endNotice.SetActive(true);
        }
    }
}