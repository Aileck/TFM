using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip[] stepsBusinessFemale;

    public AudioClip[] stepsBusinessMale;

    public AudioClip[] stepsSecurity;

    public AudioClip[] stepsTechnical;

    NPCBehaviour2 npc;
    GenericModel.MODEL myModel;

    AudioSource audioS;

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
            myModel == GenericModel.MODEL.FEMALE_4 || myModel == GenericModel.MODEL.FEMALE_5 || myModel == GenericModel.MODEL.FEMALE_6
            || myModel == GenericModel.MODEL.FEMALE_7 ) {
            int r = Random.Range(0, stepsBusinessFemale.Length - 1);
            audioS.PlayOneShot(stepsBusinessFemale[r]);
        
        }

        if (myModel == GenericModel.MODEL.MALE_1 || myModel == GenericModel.MODEL.MALE_2 || myModel == GenericModel.MODEL.MALE_3 ||
            myModel == GenericModel.MODEL.MALE_4 || myModel == GenericModel.MODEL.MALE_5 || myModel == GenericModel.MODEL.MALE_6
    ||      myModel == GenericModel.MODEL.MALE_7 || myModel == GenericModel.MODEL.MALE_8)
        {
            int r = Random.Range(0, stepsBusinessMale.Length - 1);

            audioS.PlayOneShot(stepsBusinessMale[r]);

        }

        if (myModel == GenericModel.MODEL.SECURE_1 || myModel == GenericModel.MODEL.SECURE_2) {
            int r = Random.Range(0, stepsSecurity.Length - 1);

            audioS.PlayOneShot(stepsSecurity[r]);
        }

        if (myModel == GenericModel.MODEL.TECH1)
        {
            int r = Random.Range(0, stepsTechnical.Length - 1);

            audioS.PlayOneShot(stepsTechnical[r]);
        }

        //npc.Footstep();
    }
}
