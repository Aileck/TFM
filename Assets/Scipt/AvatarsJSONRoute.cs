using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class AvatarsJSONRoute : MonoBehaviour
{
    public string FileName;
    private MyData data;
    [System.Serializable]
    public class AvatarInfo
    {
        public string id;
        public List<PositionRotation> avatarPositions;

        public AvatarInfo(string id)
        {
            this.id = id;
            this.avatarPositions = new List<PositionRotation>();
        }
    }

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

        public List<PositionRotation> playerPositions;
        public List<AvatarInfo> avatars;

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

    void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FileName + ".json");
        string jsonString = File.ReadAllText(filePath);

        Debug.Log(jsonString);

        data = JsonUtility.FromJson<MyData>(jsonString);


        LineRenderer playerRoute = GetComponent<LineRenderer>();
           // Debug.Log(counter);
           // float replayTime = Time.time - replayStartTime;
        for (int counter=0; counter<data.playerPositions.Count; counter++)
        { 
            float time = data.playerPositions[counter].timeStamp;
          //  Debug.Log(replayTime + " vs " + time);

            float posX = data.playerPositions[counter].posX;
            float posY = data.playerPositions[counter].posY;
            float posZ = data.playerPositions[counter].posZ;

            float rotX = data.playerPositions[counter].rotX;
            float rotY = data.playerPositions[counter].rotY;
            float rotZ = data.playerPositions[counter].rotZ;

            Vector3 nextPosition = new Vector3(posX, posY, posZ);
            Quaternion nextRotation = Quaternion.Euler(rotX, rotY, rotZ);
            playerRoute.SetPosition(counter, new Vector3(posX, posY, posZ));

           /* if (replayTime >= time)
            {

                camera.transform.position = nextPosition;
                camera.transform.rotation = nextRotation;

                lastPosition = nextPosition;
                lastRotation = nextRotation;

                counter++;
            }
            else if (counter != 0)
            {
                float t = (replayTime - data.prs[counter - 1].timeStamp) /
                          (data.prs[counter].timeStamp - data.prs[counter - 1].timeStamp);

                transform.position = Vector3.Lerp(lastPosition, nextPosition, t);
                transform.rotation = Quaternion.Slerp(lastRotation, nextRotation, t);
            }*/
        }
    }
}
