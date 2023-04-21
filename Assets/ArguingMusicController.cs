using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArguingMusicController : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.fire)
        {
            audio.Pause();
        }
    }
}
