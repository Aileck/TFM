using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class NPCBeh2 : MonoBehaviour
{
    // Start is called before the first frame update

    public enum State
    {
        BEFORE,
        PREPARING,
        ESCAPING
    }
    public GenericModel.MODEL myModel;
    public GenericModel.ROL myRol;


    Animator anim;
    AIDestinationSetter pathfinding;
    State myState = State.BEFORE;



    void Start()
    {
        anim = this.GetComponent<Animator>();
        switch (myRol)
        {
            case GenericModel.ROL.ANGRY:
                anim.SetBool("angry", true);
                break;
            case GenericModel.ROL.EXITED:
                anim.SetBool("exited", true);
                break;
        }
        //this.transform.GetChild(0).gameObject.SetActive(false);
        //int model = 0;
        //if (myModel != GenericModel.MODEL.RANDOM)
        //{
        //    model = (int)myModel;
        //}
        //else {
        //    model = Random.Range(0, 10);
        //}

        //SkinnedMeshRenderer thismodel = mc.GetModel(model);
        //GameObject skin = Instantiate(thismodel);
        //skin.transform.SetParent(this.transform);
        //this.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRol(GenericModel.ROL rol) {
        myRol = rol;

    }
}
