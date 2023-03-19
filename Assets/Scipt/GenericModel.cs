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

    public enum ROL
    {
        ANGRY,
        EXITED
    }

    public ModelCollector mc;
    public GenericModel.MODEL itsModel;
    public GenericModel.ROL itsRol;

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

        //thisNPC.transform.SetParent(this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
