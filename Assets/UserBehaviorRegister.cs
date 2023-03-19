using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class UserBehaviorRegister : MonoBehaviour
{
    // Start is called before the first frame update
    public VRTK_SDKManager manager;
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
        VRTK_SDKSetup setup = manager.loadedSetup;
        if (setup.systemSDKInfo != null) {

            VRTK_SDKInfo info = setup.systemSDKInfo;
            string infoSring = info.ToString();

            json.setSDK(infoSring);
            json.setSampleRate(sampleTime);

            initiated = true;

        }

        if (initiated == true) {
            StartCoroutine(registerPositionAndRotation(sampleTime));

            if (LevelManager.end)
            {
                json.GenerateJSON();
            }
        }

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
