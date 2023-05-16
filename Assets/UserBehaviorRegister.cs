using System.Collections;
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
        //StartCoroutine(registerPositionAndRotation(sampleTime));
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
            json.setPositionRotation(user.transform);
         
        }
    }

}
