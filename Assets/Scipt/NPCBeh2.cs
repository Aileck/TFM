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

    //Smooth animation

    public float moveSpeed = 1.0f;      
    public float animSpeed = 1.0f;      

    private Vector3 lastPosition;       
    private float distance;             

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
        if (LevelManager.fire && myState == State.BEFORE)
        {
            anim.SetTrigger("fire");
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            myState = State.PREPARING;

        }

        if (LevelManager.fire && myState == State.PREPARING)
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

            if ((stateInfo.IsName("Shock stand") || stateInfo.IsName("Shock sit")) && stateInfo.normalizedTime >= 0.80f)
            {
                Debug.Log("Pre run animation has finished.");
                //anim.SetInteger("run_style", myRunStyle);

                pathfinding.target = GameObject.FindGameObjectWithTag(myDestination).transform;

                this.myState = State.ESCAPING;

                lastPosition = transform.position;
            }

        }

        if (LevelManager.fire && myState == State.ESCAPING) {
            Debug.Log("My model " + (int)myModel  + " " + ai.velocity.magnitude);
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

    void SmoothMovement() {
        // 获取目标位置
        Vector3 targetPosition = pathfinding.target.transform.position;

        // 计算当前位置和目标位置之间的距离
        distance = Vector3.Distance(transform.position, targetPosition);

        // 根据距离调整移动速度和动画播放速度
        float t = Mathf.Clamp01(distance / 2.0f);
        float moveSpeedAdjusted = Mathf.Lerp(ai.maxSpeed, 0.0f, t);
        float animSpeedAdjusted = Mathf.Lerp(animSpeed, 0.0f, t);

        // 计算当前位置和目标位置之间的方向
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 移动角色
        transform.position += direction * moveSpeedAdjusted * Time.deltaTime;

        // 播放动画
        anim.speed = animSpeedAdjusted;

        // 更新上一帧的位置
        lastPosition = transform.position;
    }

}
