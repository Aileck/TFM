using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Toggle avatar;
    public Toggle training;

    public InputField id;

    public Text groupText;

    public Button start;

    private string groupTextstring;
    // Start is called before the first frame update
    void Start()
    {
        start.onClick.AddListener(LoadTutorial);
    }

    // Update is called once per frame
    void Update()
    {
        if (!avatar.isOn && !training.isOn) {
            groupTextstring = "A";
        }

        if (avatar.isOn && !training.isOn)
        {
            groupTextstring = "B";
        }

        if (!avatar.isOn && training.isOn)
        {
            groupTextstring = "C";
        }

        if (avatar.isOn && training.isOn)
        {
            groupTextstring = "D";
        }

        groupText.text = groupTextstring + "     ";


    }

    void LoadTutorial() {

        GameObject parameters = new GameObject();
        parameters.AddComponent<ExperimentParameter>();

        parameters.GetComponent<ExperimentParameter>().id = ((id.text != null) ? id.text :  "UNDEFINED");
        parameters.GetComponent<ExperimentParameter>().group = groupTextstring;
        parameters.GetComponent<ExperimentParameter>().avatar = avatar.isOn;
        parameters.GetComponent<ExperimentParameter>().training = training.isOn;
        parameters.tag = "Parameters";

        Instantiate(parameters);
        Object.DontDestroyOnLoad(parameters);

        StartCoroutine(LoadScene());
        UnityEngine.SceneManagement.SceneManager.LoadScene("Office");
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Tutorial");
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            groupText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                groupText.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space))
                    //Activate the Scene
                    asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
