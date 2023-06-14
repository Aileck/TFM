using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonWriter : MonoBehaviour
{
    // 创建一个包含要写入的数据的类
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

    private MyData data;
    private string filename; 

    private void Start()
    {

        data = new MyData();
        data.time = DateTime.Now;
        data.day = DateTime.Now.Day;
        data.month = DateTime.Now.Month;
        data.hour = DateTime.Now.Hour;
        data.minute = DateTime.Now.Minute;
        data.second = DateTime.Now.Second;

        data.prs = new List<PositionRotation>();

    }

    public void setSDK(string newsdk) {
        data.sdk = newsdk;
    }

    public void setSampleRate(float sample) {
        data.sampleRate = sample;
    }

    public void setPositionRotation(Transform transform) {
        //Debug.Log(Mathf.Round(transform.position.x * 10) / 10);
        PositionRotation register = new PositionRotation
        {
            //posX = Mathf.Round(transform.position.x * 10) / 10,
            //posY = Mathf.Round(transform.position.y * 10) / 10,
            //posZ = Mathf.Round(transform.position.z * 10) / 10,

            //rotX = Mathf.Round(transform.rotation.x * 10) / 10,
            //rotY = Mathf.Round(transform.rotation.y * 10) / 10,
            //rotZ = Mathf.Round(transform.rotation.z * 10) / 10,

        };
        
        data.prs.Add(register);
    }

    public void setPositionRotation2(float px, float py, float pz, float rx, float ry, float rz, float ts)
    {
        PositionRotation register = new PositionRotation
        {
            timeStamp = ts,
            posX = ((Mathf.Round(px) * 10) / 10),
            posY = ((Mathf.Round(py) * 10) / 10),
            posZ = ((Mathf.Round(pz) * 10) / 10),
                                     
            rotX = ((Mathf.Round(rx) * 10) / 10),
            rotY = ((Mathf.Round(ry) * 10) / 10),
            rotZ = ((Mathf.Round(rz) * 10) / 10),

        };

        data.prs.Add(register);
    }

    public void GenerateJSON() {

        string jsonString = JsonUtility.ToJson(data);

        string filePath = Path.Combine(Application.persistentDataPath, 
            data.day.ToString()  + data.month.ToString()  +
            data.hour.ToString()  + data.minute.ToString() + data.second.ToString() +
            ".json");
        File.WriteAllText(filePath, jsonString);

        Debug.Log("JSON file written to: " + filePath);

        TimeSpan timeDiff = DateTime.Now - data.time;
        Debug.Log("Total recording time: " + (int)timeDiff.TotalSeconds);
        Debug.Log("Points registered: " + data.prs.Capacity);
    }
}
