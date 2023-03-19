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
        public float posX;
        public float posY;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
    }

    public string FileName;
    public GameObject camera;
    MyData data;
    // Start is called before the first frame update
    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName);
        string jsonString = File.ReadAllText(filePath);

        MyData data = JsonUtility.FromJson<MyData>(jsonString);

        this.data = data;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(replay(data.sampleRate));
    }

    IEnumerator replay(float time)
    {
        Debug.Log(data.prs.Capacity);
        for(int i = 0; i < data.prs.Capacity; i++)
        {
            yield return new WaitForSeconds(time);
            float posX = data.prs[i].posX;
            float posY = data.prs[i].posY;
            float posZ = data.prs[i].posZ;

            float rotX = data.prs[i].rotX;
            float rotY = data.prs[i].rotY;
            float rotZ = data.prs[i].rotZ;
            camera.transform.position = new Vector3(posX,posY,posZ);
            camera.transform.rotation = Quaternion.Euler(rotX,rotY,rotZ);

        }
    }
}
