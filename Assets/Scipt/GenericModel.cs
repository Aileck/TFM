using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GenericModel : MonoBehaviour
{
    // Start is called before the first frame update

    public enum MODEL
    {
        FEMALE_1,
        FEMALE_2,
        FEMALE_3,
        FEMALE_4,
        FEMALE_5,
        FEMALE_6,
        FEMALE_7,

        MALE_1,
        MALE_2,
        MALE_3,
        MALE_4,
        MALE_5,
        MALE_6,
        MALE_7,
        MALE_8,

        SECURE_1,
        SECURE_2,

        TECH1,
        RANDOM

    }

    public enum POSITION {
        STAND,
        SIT,
        MOVEMENT
    }

    public enum ROL
    {
        //Stand
        ANGRY,
        EXITED,
        TYPE,
        LEAN,
        PRESENT,
        LISTEN,
        KNOCK,
        COFEE,
        ARGUING,
        YELLING,

        //Sit
        LAUGHT,
        TALK,
        NO_TALK,
        TALK_LESS,

        //Movment
        PATROL,
        REPAIR,
        FAX,
        TELEPHONE
        
    }

    public enum REACTION_TIME_TO_RUN
    {
        //Unit = mili second
        SLOWEST = 2500,  
        Slow = 2000,      
        Moderate = 1500,  
        Fast = 1000,      
        Fastest = 200    
    }

    private ROL[] standRols = { ROL.ANGRY, ROL.EXITED, ROL.TYPE, ROL.LEAN, ROL.PRESENT, ROL.LISTEN, ROL.KNOCK, ROL.COFEE, ROL.ARGUING, ROL.YELLING };
    private ROL[] sitRols = { ROL.LAUGHT, ROL.TALK, ROL.NO_TALK, ROL.TALK_LESS };
    private ROL[] movementRols = { ROL.PATROL };

    public enum DESTINATION
    {
        DOOR1,
        DOOR2
    }
    [SerializeField]
    ModelCollector mc;
    [SerializeField]
    GenericModel.MODEL itsModel;
    [SerializeField]
    GenericModel.ROL itsRol;    
    [SerializeField]
    GenericModel.REACTION_TIME_TO_RUN itsReactionTime;
    [SerializeField]
    GenericModel.POSITION itsPosition;
    [SerializeField]
    GenericModel.DESTINATION itsDestination;
    [SerializeField]
    GameObject[] itsBeforeFireDestination;
    string[] doorTags = { "Door1", "Door2" };

    //[SerializeField]
    //GenericModel.ROL itsRol2;


    void Start()
    {
        int model = 0;
        if (itsModel != GenericModel.MODEL.RANDOM)
        {
            model = (int)itsModel;
        }
        else
        {
            model = Random.Range(0, 18);
        }

        GameObject thismodel = mc.GetModel(model);

        GameObject thisNPC = Instantiate(thismodel,this.transform.position, this.transform.rotation, this.transform);
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);

        thisNPC.GetComponent<NPCBehaviour2>().SetModel(itsModel);
        thisNPC.GetComponent<NPCBehaviour2>().SetRol(itsRol);
        thisNPC.GetComponent<NPCBehaviour2>().SetPosition(itsPosition);
        thisNPC.GetComponent<NPCBehaviour2>().SetDestination(doorTags[(int)itsDestination]);
        thisNPC.GetComponent<NPCBehaviour2>().SetBeforeDestination(itsBeforeFireDestination);
        thisNPC.GetComponent<NPCBehaviour2>().setReactionTimeToRun((float)((int)itsReactionTime)/1000);


        //thisNPC.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetModel(MODEL model) {
        this.itsModel = model;
    }

    public void SetRol(ROL rol, POSITION pos = POSITION.STAND) {
        this.itsRol = rol;
        itsPosition = pos;
    }

    public void SetBeforeDestination(GameObject[] destinations)
    {
        itsBeforeFireDestination = destinations;
    }

    public void SetReactionTime(REACTION_TIME_TO_RUN reaction) {
        itsReactionTime = reaction;
    }

    public void SetDestination(DESTINATION destin) {
        itsDestination = destin;
    }

}
