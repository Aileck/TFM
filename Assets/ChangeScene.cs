using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject officePart1;
    public GameObject officePart2;

    public GameObject doorsToClose;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchScene() {
        officePart1.SetActive(false);
        officePart2.SetActive(true);
    }
}
