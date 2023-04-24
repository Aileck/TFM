using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class NPCBehaviour2 : MonoBehaviour
{
    // Global state

    public enum State
    {
        BEFORE,
        PREPARING,
        ESCAPING
    }

    State myState = State.BEFORE;

    //Apparence and Behehaviour
    GenericModel.MODEL myModel;
    GenericModel.ROL myRol;
    GenericModel.POSITION myPos;

    //Face Expression Variable
    float mouthBlendShape = 0;
    bool openmouth = true;

    //Fath finding
    string myFireDestination;
    GameObject[] myBeforeFireDestination;
    int currentDestination = 0;
    public float distanceThreshold = 2f; // Distance threshold, used to judge whether to reach the target point

    AIDestinationSetter pathfinding;
    RichAI ai;
    //AIPath ai;

    //Animation controller
    float reactionTimeToRun = 0;
    int myRunStyle = 0;
    Animator anim;
    bool footstepping = false;
    public float footstepCounter = 0.02f;
    float currentFootstepCounter = 0f;




    void Start()
    {
        pathfinding = this.GetComponent<AIDestinationSetter>();
        anim = this.GetComponent<Animator>();
        ai = this.GetComponent<RichAI>();
        ai.maxSpeed = 0.8f;

        myRunStyle = Random.Range(0, 4);
        anim.SetInteger("run_style", myRunStyle);

        anim.SetFloat("reaction_time_to_run", reactionTimeToRun);

        if (myPos == GenericModel.POSITION.SIT) {
            anim.SetTrigger("sit");
        }
        if (myPos == GenericModel.POSITION.MOVEMENT)
        {
            anim.SetTrigger("movement");
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
            case GenericModel.ROL.TELEPHONE:
                anim.SetBool("telephone", true);
                break;
            case GenericModel.ROL.KNOCK:
                ai.canMove = false;
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
            case GenericModel.ROL.PATROL:
                anim.SetBool("patrol", true);
                ai.maxSpeed = 0.8f;
                break;

            case GenericModel.ROL.REPAIR:
                ai.canMove = false;
                anim.SetBool("repair", true);
                break;

            case GenericModel.ROL.FAX:
                ai.canMove = false;
                anim.SetBool("fax", true);
                break;
        }



        


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //1.Befotr fire, do its routine
        if (!LevelManager.fire && myState == State.BEFORE)
        {
            FaceExpressionControl();
            if (myPos == GenericModel.POSITION.MOVEMENT) {
                MovmentControl();
            }

            //Determine if foot step
            if (footstepping) {
                FootStepControl();
            }

        }

        //2. Prepare to be shocked
        if (LevelManager.fire && myState == State.BEFORE)
        {
            anim.SetTrigger("fire");
            myState = State.PREPARING;

            //Face expression
            FaceExpressionControl();
  

        }

        //3. Be shock
        if (LevelManager.fire && myState == State.PREPARING)
        {
            //Stop all movement
            ai.canMove = false;

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


            if ((   (stateInfo.IsName("Shock stand") && stateInfo.normalizedTime >= 0.80f) || 
                    stateInfo.IsName("Shock sit 2")) && stateInfo.normalizedTime >= 0.50f)
            {

                reactionTimeToRun -= Time.deltaTime;
                anim.SetFloat("reaction_time_to_run", reactionTimeToRun);

                if (reactionTimeToRun <= 0) {
                    pathfinding.target = GameObject.FindGameObjectWithTag(myFireDestination).transform;
                    this.myState = State.ESCAPING;
                }     
            }

        }


        //4. Start to run
        if (LevelManager.fire && myState == State.ESCAPING) {
            //Debug.Log("My model " + (int)myModel  + " " + ai.velocity.magnitude);
            ai.canMove = true;
            //anim.SetFloat("Speed", ai.velocity.magnitude);
            //anim.applyRootMotion = true;
            //SmoothMovement();
        }
    }
    public void SetModel(GenericModel.MODEL model)
    {
        myModel = model;
    }
    public void SetRol(GenericModel.ROL rol) {
        myRol = rol;
    }
    public void SetPosition(GenericModel.POSITION pos)
    {
        myPos = pos;
    }

    public void SetDestination(string destination) {
        myFireDestination = destination;
    }

    public void SetBeforeDestination(GameObject[] destinations)
    {
        myBeforeFireDestination = destinations;
    }

    public GenericModel.MODEL GetModel() {
        return myModel;
    }

    public void setReactionTimeToRun(float time)
    {
        reactionTimeToRun = time;
    }

    void MovmentControl() {

        if (myBeforeFireDestination.Length > 0) {
            pathfinding.target = myBeforeFireDestination[currentDestination].transform;
            float distance = Vector3.Distance(transform.position, pathfinding.target.position);
            Debug.Log(distance);
            //On path complete
            if (distance < distanceThreshold)
            {
                //Special case
                if (myRol == GenericModel.ROL.REPAIR || myRol == GenericModel.ROL.FAX)
                {
                    Debug.Log(myModel + " Reached");
                    PauseMovement();
                }

                //Redo 
                currentDestination++;
                if (currentDestination >= myBeforeFireDestination.Length)
                {
                    currentDestination = 0;

                }
            }
        }


    }



    public void NextMovement()
    {
        ai.canMove = true;
        anim.SetBool("ready_for_next_animation", true);
    }

    public void PauseMovement()
    {
        ai.canMove = false;
        anim.SetBool("ready_for_next_animation", false);
    }

    public void Footstep()
    {
        footstepping = true;
        ai.canMove = false;
    }


    void FootStepControl() {
        currentFootstepCounter += Time.deltaTime;
        if (currentFootstepCounter >= footstepCounter) {
            currentFootstepCounter = 0;

            footstepping = false;
            ai.canMove = true;
        }
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
