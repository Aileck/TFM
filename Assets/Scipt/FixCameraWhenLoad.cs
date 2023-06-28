using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixCameraWhenLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<Camera>().enabled = false;
        StartCoroutine(StartAfterSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();
        this.GetComponent<Camera>().enabled = true;

    }
}
