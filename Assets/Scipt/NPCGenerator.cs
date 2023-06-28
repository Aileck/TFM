using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GenericModel.MODEL itsModel;
    public GenericModel.ROL itsRol;
    public GenericModel.POSITION itsPosition = GenericModel.POSITION.SIT;
    public GenericModel.MOVE_VELOCITY itsVelocity;
    public GenericModel.REACTION_TIME_TO_RUN itsReactionTime;
    public GameObject genericModel;
    public GenericModel.DESTINATION itsDestination;
    public GameObject[] itsBeforeFireDestination;
    GameObject myNPC;

    bool pushed;
    bool ready;

    void Start()
    {
        GameObject thisNPC = Instantiate(genericModel, this.transform.position, this.transform.rotation, this.transform);
        thisNPC.GetComponent<GenericModel>().SetModel(itsModel);
        thisNPC.GetComponent<GenericModel>().SetRol(itsRol, itsPosition);
        thisNPC.GetComponent<GenericModel>().SetBeforeDestination(itsBeforeFireDestination);
        thisNPC.GetComponent<GenericModel>().SetDestination(itsDestination);
        thisNPC.GetComponent<GenericModel>().SetReactionTime(itsReactionTime);
        thisNPC.GetComponent<GenericModel>().SetVelocity(itsVelocity);

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

        //Push chair when sounds alar
        if (itsPosition != GenericModel.POSITION.MOVEMENT && (LevelManager.fire && pushed == false && ready && this.GetComponent<Rigidbody>() != null))
        {

            Vector3 backwardForce = new Vector3(0, 0, -1);

            this.GetComponent<Rigidbody>().AddForce(backwardForce, ForceMode.Impulse);
            pushed = true;

        }

    }

    public void SetReady() {
        ready = true;
    }


}
