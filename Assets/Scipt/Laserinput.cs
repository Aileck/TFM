using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;

public class Laserinput : MonoBehaviour
{

    public static GameObject currentObject;
    int currentID;

    //public SteamVR_Input_Source input;
    public SteamVR_Action_Boolean input;

    // Start is called before the first frame update
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;

        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);

        for (int i = 0; i < hits.Length; i++) {
            ProcessLastHit();
            RaycastHit hit = hits[i];
            int id = hit.collider.gameObject.GetInstanceID();

            if (currentID != id) {
                currentID = id;
                currentObject = hit.collider.gameObject;
                string name = currentObject.name;

                ProcessHit(currentObject);
            }
        }
    }

    void ProcessHit(GameObject o) {
        if (o.GetComponent<Button>() != null) {
            Button b = o.GetComponent<Button>();
            b.interactable = false;

            if (input.GetStateDown(SteamVR_Input_Sources.Any)) {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
            }
        }
    }

    void ProcessLastHit() {
        if(currentObject != null)
            if (currentObject.GetComponent<Button>() != null)
            {
                Button b = currentObject.GetComponent<Button>();
                b.interactable = true;
            }
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Office");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {

                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

