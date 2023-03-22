using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GenericModel.MODEL itsModel;
    public GenericModel.ROL itsRol;
    public GenericModel.POSITON itsPosition = GenericModel.POSITON.SIT;
    public GameObject genericModel;

    public float m_Thrust;
    bool pushed = false;
    float amountGoBack = 9;

    void Start()
    {
        GameObject thisNPC = Instantiate(genericModel, this.transform.position, this.transform.rotation, this.transform);
        thisNPC.GetComponent<GenericModel>().SetModel(itsModel);
        thisNPC.GetComponent<GenericModel>().SetRol(itsRol, itsPosition);

        thisNPC.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.fire && pushed == false)
        {
            //this.transform.position-= Vector3.left * Time.deltaTime;
            //this.transform.GetChild(0).transform.position -= Vector3.left * Time.deltaTime;
            //this.transform.GetChild(1).transform.position -= Vector3.left * Time.deltaTime;

            transform.Translate(Vector3.back * 50);
            pushed = true;

        }

        //this.transform.position -= new Vector3(-0.5f,0,0);
        //amountGoBack -= Time.deltaTime;




    }
}
