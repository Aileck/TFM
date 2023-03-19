using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCollector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] models;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetModel(int index) {
        return models[index];
    }
}
