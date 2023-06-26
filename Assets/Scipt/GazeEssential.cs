using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.G2OM;

public class GazeEssential : MonoBehaviour,  IGazeFocusable
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GazeFocusChanged(bool hasFocus)
    {
        //If this object received focus, fade the object's color to highlight color
        if (hasFocus)
        {
            Debug.Log("Essential look");
        }

    }
}
