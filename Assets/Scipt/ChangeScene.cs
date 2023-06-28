using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public GameObject officePart1Decoration;
    public GameObject officePart2Decoration;

    public bool ChangeOffice1;
    public bool ChangeOffice2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchScene() {
        if (ChangeOffice2 && LevelManager.inoffice1)
        {
            LevelManager.inoffice1 = false;
            SwitchOffice2();
        }

        if (ChangeOffice1 && !LevelManager.inoffice1)
        {
            LevelManager.inoffice1 = true;
            SwitchOffice1();
        }
    }

    private void SwitchOffice2() {
        officePart1Decoration.SetActive(false);
        officePart2Decoration.SetActive(true);
    }

    private void SwitchOffice1()
    {
        officePart1Decoration.SetActive(true);
        officePart2Decoration.SetActive(false);
    }
}
