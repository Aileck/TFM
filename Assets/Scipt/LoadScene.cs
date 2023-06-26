using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public Text LoadText;
    public Button LoadButton;

    private bool prepated = false;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadNextScene());
        LoadButton.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Clicked() {
        prepated = true;
        LoadText.text = "Cool";
    }

    IEnumerator LoadNextScene()
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
            LoadText.text = "Loading progress: " + (asyncOperation.progress * 100) + "%";

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                LoadText.text = "Press the space bar to continue";
                //Wait to you press the space key to activate the Scene
                if (Input.GetKeyDown(KeyCode.Space) && prepated) {
                    asyncOperation.allowSceneActivation = true;
                }
                    //Activate the Scene
                    //
            }

            yield return null;
        }
    }
}
