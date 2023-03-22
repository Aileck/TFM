using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericModel : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MODEL
    {
        FEMALE_1,
        FEMALE_2,
        FEMALE_3,
        FEMALE_4,

        MALE_1,
        MALE_2,
        MALE_3,
        MALE_4,
        MALE_5,
        MALE_6,
        MALE_7,
        RANDOM

    }

    public enum POSITON {
        STAND,
        SIT,
        MOVMENT
    }

    public enum ROL
    {
        ANGRY,
        EXITED,
        TYPE,
        LAUGHT,
        TALK,
        NO_TALK,
        TALK_LESS,
        LEAN,
        PRESENT,
        LISTEN,
        KNOCK,
        COFEE,
        ARGUING,
        YELLING
    }

    public enum DESTINATION
    {
        DOOR1,
        DOOR2
    }

    public ModelCollector mc;
    public GenericModel.MODEL itsModel;
    public GenericModel.ROL itsRol;
    public GenericModel.POSITON itsPosition;
    public GenericModel.DESTINATION itsDestination;
    string[] doorTags = { "Door1", "Door2" };

    void Start()
    {
        int model = 0;
        if (itsModel != GenericModel.MODEL.RANDOM)
        {
            model = (int)itsModel;
        }
        else
        {
            model = Random.Range(0, 10);
        }

        GameObject thismodel = mc.GetModel(model);

        GameObject thisNPC = Instantiate(thismodel,this.transform.position, this.transform.rotation, this.transform);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);

        thisNPC.GetComponent<NPCBeh2>().SetRol(itsRol);
        thisNPC.GetComponent<NPCBeh2>().SetPosition(itsPosition);
        thisNPC.GetComponent<NPCBeh2>().SetDestination(doorTags[(int)itsDestination]);

        //thisNPC.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetModel(MODEL model) {
        this.itsModel = model;
    }

    public void SetRol(ROL rol, POSITON pos = POSITON.STAND) {
        this.itsRol = rol;
        itsPosition = pos;
    }
}
