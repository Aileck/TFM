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
    public GenericModel.POSITON myPos;
    public string myDestination;


    Animator anim;
    AIDestinationSetter pathfinding;
    RichAI ai;
    public State myState = State.BEFORE;

    int myRunStyle = 0;

    //Face Expression Variable
    float mouthBlendShape = 0;
    bool openmouth = true;

    void Start()
    {
        pathfinding = this.GetComponent<AIDestinationSetter>();
        anim = this.GetComponent<Animator>();
        ai = this.GetComponent<RichAI>();

        myRunStyle = Random.Range(0, 4);
        //anim.SetInteger("run_style", -1);
        anim.SetInteger("run_style", myRunStyle);

        if (myPos == GenericModel.POSITON.SIT) {
            anim.SetTrigger("sit");
        }
        switch (myRol)
        {
            case GenericModel.ROL.ANGRY:
                anim.SetBool("angry", true);
                break;
            case GenericModel.ROL.EXITED:
                anim.SetBool("exited", true);
                break;
            case GenericModel.ROL.TYPE:
                anim.SetBool("type", true);
                break;
            case GenericModel.ROL.LAUGHT:
                anim.SetBool("laught", true);
                break;
            case GenericModel.ROL.TALK:
                anim.SetBool("talk", true);
                break;
            case GenericModel.ROL.NO_TALK:
                anim.SetBool("no_talk", true);
                break;
            case GenericModel.ROL.TALK_LESS:
                anim.SetBool("talk_less", true);
                break;
            case GenericModel.ROL.LEAN:
                anim.SetBool("lean", true);
                break;
            case GenericModel.ROL.PRESENT:
                anim.SetBool("present", true);
                break;
            case GenericModel.ROL.LISTEN:
                anim.SetBool("listen", true);
                break;
            case GenericModel.ROL.KNOCK:
                anim.SetBool("knock", true);
                break;
            case GenericModel.ROL.COFEE:
                anim.SetBool("coffee", true);
                break;
            case GenericModel.ROL.ARGUING:
                anim.SetBool("arguing", true);
                break;
            case GenericModel.ROL.YELLING:
                anim.SetBool("yelling", true);
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!LevelManager.fire && myState == State.BEFORE)
        {
            FaceExpressionControl();

        }
        if (LevelManager.fire && myState == State.BEFORE)
        {
            anim.SetTrigger("fire");
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            myState = State.PREPARING;

            //Face expression
            FaceExpressionControl();

        }

        if (LevelManager.fire && myState == State.PREPARING)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Shock sit") && stateInfo.normalizedTime >= 0.25f){

                //Access to top parent (chair) that is ready
                Transform topParent = transform;
                while (!topParent.GetComponent<NPCGenerator>())
                {
                    topParent = topParent.parent;
                }
                if(topParent.GetComponent<NPCGenerator>() != null)
                    topParent.gameObject.GetComponent<NPCGenerator>().SetReady();


            }
            if ((stateInfo.IsName("Shock stand") || stateInfo.IsName("Shock sit")) && stateInfo.normalizedTime >= 0.80f)
            {
                //Debug.Log("Pre run animation has finished.");
                //anim.SetInteger("run_style", myRunStyle);

                pathfinding.target = GameObject.FindGameObjectWithTag(myDestination).transform;

                this.myState = State.ESCAPING;
            }

        }

        if (LevelManager.fire && myState == State.ESCAPING) {
            //Debug.Log("My model " + (int)myModel  + " " + ai.velocity.magnitude);
            anim.SetFloat("Speed", ai.velocity.magnitude);
            anim.applyRootMotion = true;
            //SmoothMovement();
        }
    }

    public void SetRol(GenericModel.ROL rol) {
        myRol = rol;
    }
    public void SetPosition(GenericModel.POSITON pos)
    {
        myPos = pos;
    }


    public void SetDestination(string destination) {
        myDestination = destination;
    }


    void FaceExpressionControl()
    {
        //Talk
        if(myRol == GenericModel.ROL.EXITED ||
            myRol == GenericModel.ROL.ARGUING ||
            myRol == GenericModel.ROL.YELLING ||
            myRol == GenericModel.ROL.TALK ||
            myRol == GenericModel.ROL.PRESENT)
        {
            mouthBlendShape = Mathf.Lerp(mouthBlendShape, 100, Time.deltaTime * 0.5F);

            SkinnedMeshRenderer skinnedMeshRenderer = transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
            Mesh mesh = skinnedMeshRenderer.sharedMesh;
            int mouthO = mesh.GetBlendShapeIndex("AA_VL_13_O");
            skinnedMeshRenderer.SetBlendShapeWeight(13, mouthBlendShape);
            if(myRol == GenericModel.ROL.EXITED) {
                skinnedMeshRenderer.SetBlendShapeWeight(25, mouthBlendShape);

            }





            if (openmouth)
            {
                mouthBlendShape = Mathf.Lerp(mouthBlendShape, 100, Time.deltaTime * 5F);

                if(mouthBlendShape >= 90)
                {
                    openmouth = false;
                }
            }

            else if (!openmouth)
            {
                mouthBlendShape = Mathf.Lerp(mouthBlendShape, 0, Time.deltaTime * 5F);
                if (mouthBlendShape <= 10)
                {
                    openmouth = true;
                }
            }
        }
    }

}
