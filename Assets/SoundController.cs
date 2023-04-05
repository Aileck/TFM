using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] walkStepsHeels;
    public AudioClip[] walkFastStepsHeels;

    public NPCBehaviour2 npc;
    public GenericModel.MODEL myModel;

    public AudioSource audioS;

    void Start()
    {
        audioS = this.gameObject.GetComponent<AudioSource>();

        npc = this.gameObject.GetComponent<NPCBehaviour2>();
        myModel = npc.GetModel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FootSteps() {

        if (myModel == GenericModel.MODEL.FEMALE_1 || myModel == GenericModel.MODEL.FEMALE_2 || myModel == GenericModel.MODEL.FEMALE_3 ||
            myModel == GenericModel.MODEL.FEMALE_4) {
            int r = Random.Range(0, walkStepsHeels.Length - 1);

            audioS.PlayOneShot(walkStepsHeels[r]);
        
        }
    }
}
