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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
