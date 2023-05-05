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


    void Start()
    {
        //if (setup = )
        //user = GameObject.FindGameObjectWithTag("MainCamera");
    }



    // Update is called once per frame
    void FixedUpdate()
    {


    }

    IEnumerator registerPositionAndRotation(float time)
    {
        while (true && LevelManager.end != false)
        {
            yield return new WaitForSeconds(time);
            json.setPositionRotation(user.transform);
         
        }
    }

}
