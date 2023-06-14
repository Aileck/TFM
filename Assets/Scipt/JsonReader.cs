using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonReader : MonoBehaviour
{
    [System.Serializable]
    public class MyData
    {
        public string sdk;

        public DateTime time;
        public int day;
        public int month;
        public int hour;
        public int minute;
        public int second;


        public float sampleRate;

        public List<PositionRotation> prs;
    }
    [System.Serializable]
    public class PositionRotation
    {
        public float timeStamp;
        public float posX;
        public float posY;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
    }

    //Var
    private float replayStartTime = 0f;
    public string FileName;
    public GameObject camera;
    MyData data;
    int counter = 0;

    Vector3 lastPosition = new Vector3();
    Quaternion lastRotation = new Quaternion();


    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName+".json");
        string jsonString = File.ReadAllText(filePath);

        Debug.Log(jsonString);

        MyData data = JsonUtility.FromJson<MyData>(jsonString);

        this.data = data;
        this.replayStartTime = Time.time;
    }

    // Update is called once per frame
    //void Update()
    //{
    //    StartCoroutine(replay(data.sampleRate));
    //}

    void FixedUpdate()
    {
        if (counter < data.prs.Capacity)
        {
            Debug.Log(counter);
            float replayTime = Time.time - replayStartTime;

            float time = data.prs[counter].timeStamp;
            Debug.Log(replayTime + " vs " + time);

            float posX = data.prs[counter].posX;
            float posY = data.prs[counter].posY;
            float posZ = data.prs[counter].posZ;

            float rotX = data.prs[counter].rotX;
            float rotY = data.prs[counter].rotY;
            float rotZ = data.prs[counter].rotZ;

            Vector3 nextPosition = new Vector3(posX, posY, posZ);
            Quaternion nextRotation = Quaternion.LookRotation(new Vector3(rotX, rotY, rotZ));

            if (replayTime >= time)
            {

                camera.transform.position = nextPosition;
                camera.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

                lastPosition = nextPosition;
                lastRotation = nextRotation;

                counter++;
            }
            else if(counter != 0)
            {
                float t = (replayTime - data.prs[counter - 1].timeStamp) /
                          (data.prs[counter].timeStamp - data.prs[counter-1].timeStamp);

                transform.position = Vector3.Lerp(lastPosition, nextPosition, t);
                transform.rotation = Quaternion.Slerp(lastRotation, nextRotation, t);
            }
        }
    }


    IEnumerator replay(float time)
    {
        for (int i = 0; i < data.prs.Capacity; i++)
        {
            yield return new WaitForSeconds(time);
            float posX = data.prs[i].posX;
            float posY = data.prs[i].posY;
            float posZ = data.prs[i].posZ;

            float rotX = data.prs[i].rotX;
            float rotY = data.prs[i].rotY;
            float rotZ = data.prs[i].rotZ;

            Debug.Log(i);
            camera.transform.position = new Vector3(posX, posY, posZ);
            camera.transform.rotation = Quaternion.Euler(rotX, rotY, rotZ);

        }
    }
}
