using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class NPCBehaviour : MonoBehaviour
{
    public enum State {
        SITTING,
        STANDING,
        ESCAPING
    }
    // Start is called before the first frame update
    Animator anim;
    AIDestinationSetter pathfinding;
    State myState = State.SITTING;
    int mySitStyle = 0;
    int myRunStyle = 0;



    void Awake()
    {
        pathfinding = this.GetComponent<AIDestinationSetter>();
        pathfinding.enabled = false;
    }
    void Start()
    {
        pathfinding = this.GetComponent<AIDestinationSetter>();
        pathfinding.enabled = false;
        myRunStyle = Random.Range(0,3);
        mySitStyle = Random.Range(0, 7);
        //mySitStyle = 0;

        anim = this.GetComponent<Animator>();
        anim.SetInteger("RunStyle", myRunStyle);
        anim.SetInteger("SitStyle", mySitStyle);


    }

    // Update is called once per frame
    void Update()
    {

        if (LevelManager.fire && myState == State.STANDING) {
            anim.SetBool("Running",true);

            pathfinding.enabled = true;

        }

    }

    public void SetToStand() {
        this.myState = State.STANDING;
        anim.SetBool("Standing", true);
    }
}
