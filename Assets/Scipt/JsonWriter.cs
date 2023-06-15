using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JsonWriter : MonoBehaviour
{
    [System.Serializable]
    public class MyData
    {
        public string id;

        public DateTime time;

        //Time info
        public int day;
        public int month;
        public int year;
        public int hour;
        public int minute;
        public int second;

        //Parameter
        public bool training;
        public bool avatar;
        public string group;
        public float timeBeforeTimber;
        public float sampleRate;

        //User info
        public UserInfo user;

        //Avatar
        public List<AvatarInfo> avatars;

    }

    [System.Serializable]
    public class UserInfo
    {
        //Experiment sumarry
        public string exit;
        public float totaldutarion;

        //Route
        public float height;
        public List<PositionRotation> prs;
    }

    public class AvatarInfo
    {
        public string id;
        public List<PositionRotation> prs;
    }

    public class Sign
    {
        public string id;
        public StaticPosition pos;
    }

    public class Exit
    {
        public string id;
        public StaticPosition pos;
    }

    [System.Serializable]
    public class PositionRotation
    {
        public float timeStamp;

        public float posX;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
    }

    [System.Serializable]
    public class StaticPosition
    {
        public float posX;
        public float posZ;
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

        data.user = new UserInfo();
        data.prs = new List<PositionRotation>();

    }

    public void setID(string id) {
        data.id = id;
    }

    public void setSampleRate(float sample) {
        data.sampleRate = sample;
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
