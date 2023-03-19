using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class BusinessManGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] businessmanPrefabs;
    string[] doorTags = { "Door1", "Door2"};

    float amountGoBack = 1;

    GameObject globalBusinessMan;
    bool initiated = false;
    void Start()
    {
        int selectBusinessMan = Random.Range(0, businessmanPrefabs.Length);
        int selectTag = Random.Range(0, doorTags.Length);

        Vector3 initPos = new Vector3 (0, 0f, 0.5f);
        Quaternion initRot = Quaternion.identity;

        GameObject selected = businessmanPrefabs[selectBusinessMan];
        GameObject businessman = Instantiate(selected, initPos, initRot) as GameObject;
        businessman.GetComponent<CharacterController>().enabled = false;
        businessman.transform.SetParent(this.transform);

        businessman.transform.localPosition = initPos;
        businessman.transform.localRotation = initRot;

        string selectedTag = doorTags[selectTag];
        businessman.GetComponent<AIDestinationSetter>().target = GameObject.FindGameObjectWithTag(selectedTag).transform;

        globalBusinessMan = businessman.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (!initiated)
        {
            Vector3 initPos = new Vector3(0, 0f, 0.0f);
            Quaternion initRot = Quaternion.identity;

            globalBusinessMan.transform.localPosition = initPos;
            globalBusinessMan.transform.localRotation = initRot;

            initiated = true;
        }

        if (LevelManager.fire && amountGoBack >= 0)
        {
            transform.position -= Vector3.left * Time.deltaTime;
            amountGoBack -= Time.deltaTime;

            if (globalBusinessMan != null) {
                globalBusinessMan.GetComponent<NPCBehaviour>().SetToStand();
                globalBusinessMan.transform.parent = null;
                globalBusinessMan = null;

            }


        }
    }
}
