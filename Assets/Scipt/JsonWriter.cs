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



        //Other
        public List<AvatarInfo> avatars;
        public List<Sign> signs;
        public List<Exit> exits;

        //User info
        public UserInfo user;

    }

    [System.Serializable]
    public class UserInfo
    {
        //Experiment sumarry
        public string exit;
        public float totalduration;

        //Route
        public float height;
        public List<PositionRotation> prs;

        public UserInfo() {
            prs = new List<PositionRotation>();
        }
    }

    [System.Serializable]
    public class AvatarInfo
    {
        public string id;
        public List<PositionRotation> prs;

        public AvatarInfo(string id)
        {
            this.id = id;
            this.prs = new List<PositionRotation>();
        }
    }

    [System.Serializable]
    public class Sign
    {
        public string id;
        public StaticPosition pos;

        public Sign(string id, float posX, float posZ) {
            this.id = id;
            this.pos = new StaticPosition(posX, posZ);
        }
    }

    public class Exits
    {
        public List<Exit> exits;

        public Exits()
        {
            exits = new List<Exit>();
        }
    }

    [System.Serializable]
    public class Exit
    {
        public string id;
        public StaticPosition pos;

        public Exit(string id, float posX, float posZ)
        {
            this.id = id;
            this.pos = new StaticPosition(posX, posZ);
        }
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

        public StaticPosition(float posX, float posZ) {
            this.posX = posX;
            this.posZ = posZ;
        }
    }

    private MyData data;
    private string filename;

    private bool generated;

    private void Start()
    {

        data = new MyData();
        data.time = DateTime.Now;
        data.day = DateTime.Now.Day;
        data.month = DateTime.Now.Month;
        data.year = DateTime.Now.Year;
        data.hour = DateTime.Now.Hour;
        data.minute = DateTime.Now.Minute;
        data.second = DateTime.Now.Second;

        data.user = new UserInfo();

        data.avatars = new List<AvatarInfo>();

        data.signs = new List<Sign>();
        data.exits = new List<Exit>();

    }

    public void setID(string id) {
        data.id = id;
    }

    public void setCondition(bool training, bool avatar) {
        //Parameter
        data.training = training;
        data.avatar = avatar;
 
        //public float sampleRate;
    }

    public void setGroup(string group) {
        data.group = group;
    }

    public void setTimeBeforeTimber(int time) {
        data.timeBeforeTimber = time;
    }

    public void setSampleRate(float sample) {
        data.sampleRate = sample;
    }

    public void createAvatar(string id) {
        data.avatars.Add(new AvatarInfo(id));
    }

    public void createSign(string id, float posX, float posZ)
    {
        data.signs.Add(new Sign(id, posX, posZ));
    }

    public void createExit(string id, float posX, float posZ)
    {
        data.exits.Add(new Exit(id, posX, posZ));
    }


    public void setPositionRotation2(float px, float py, float pz, float rx, float ry, float rz, float ts)
    {
        PositionRotation register = new PositionRotation
        {
            timeStamp = ts,
            posX = ((Mathf.Round(px) * 10) / 10),
            posZ = ((Mathf.Round(pz) * 10) / 10),
                                     
            rotX = ((Mathf.Round(rx) * 10) / 10),
            rotY = ((Mathf.Round(ry) * 10) / 10),
            rotZ = ((Mathf.Round(rz) * 10) / 10),

        };

        data.user.prs.Add(register);
    }

    public void GenerateJSON() {
        if (!generated) {
            string jsonString = JsonUtility.ToJson(data);

            string filePath = Path.Combine(Application.persistentDataPath,
                data.day.ToString() + data.month.ToString() +
                data.hour.ToString() + data.minute.ToString() + data.second.ToString() +
                ".json");
            File.WriteAllText(filePath, jsonString);

            Debug.Log("JSON file written to: " + filePath);

            TimeSpan timeDiff = DateTime.Now - data.time;
            Debug.Log("Total recording time: " + (int)timeDiff.TotalSeconds);

            generated = true;

        }
    }
}
