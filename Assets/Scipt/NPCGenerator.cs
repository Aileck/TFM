using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GenericModel.MODEL itsModel;
    public GenericModel.ROL itsRol;
    public GenericModel.POSITION itsPosition = GenericModel.POSITION.SIT;
    public GenericModel.REACTION_TIME_TO_RUN itsReactopmTime;
    public GameObject genericModel;
    public GameObject[] itsBeforeFireDestination;

    public GameObject myNPC;

    public float m_Thrust;
    bool pushed;
    bool ready;

    void Start()
    {
        GameObject thisNPC = Instantiate(genericModel, this.transform.position, this.transform.rotation, this.transform);
        thisNPC.GetComponent<GenericModel>().SetModel(itsModel);
        thisNPC.GetComponent<GenericModel>().SetRol(itsRol, itsPosition);
        thisNPC.GetComponent<GenericModel>().SetBeforeDestination(itsBeforeFireDestination);
        thisNPC.GetComponent<GenericModel>().SetReactionTime(itsReactopmTime) ;

        myNPC = thisNPC;
        pushed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(LevelManager.fire)
        {
            this.gameObject.layer = 0;
            transform.GetChild(0).gameObject.layer = 0;
            transform.GetChild(1).gameObject.layer = 0;
        }
        if (itsPosition != GenericModel.POSITION.MOVEMENT && (LevelManager.fire && pushed == false && ready && this.GetComponent<Rigidbody>() != null))
        {
            //this.transform.position-= Vector3.left * Time.deltaTime;
            //this.transform.GetChild(0).transform.position -= Vector3.left * Time.deltaTime;
            //this.transform.GetChild(1).transform.position -= Vector3.left * Time.deltaTime;


            //transform.Translate(Vector3.back);


            Vector3 backwardForce = new Vector3(0, 0, -1);

            this.GetComponent<Rigidbody>().AddForce(backwardForce, ForceMode.Impulse);
            pushed = true;



        }

        //this.transform.position -= new Vector3(-0.5f,0,0);
        //amountGoBack -= Time.deltaTime;


    }

    public void SetReady() {
        ready = true;
    }


}
