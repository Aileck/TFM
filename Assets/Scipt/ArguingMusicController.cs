using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArguingMusicController : MonoBehaviour
{
    AudioSource audio;
    // Start is called before the first frame update
    void Awake()
    {
        audio = this.GetComponent<AudioSource>();
        StartCoroutine(StartAfterSceneLoad());
        //if ((GameObject.FindGameObjectsWithTag("Avatar").Length != 0))
        //{

        //}
        //else {
        //    audio.Pause();
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.fire)
        {
            audio.Pause();
        }

    }

    void UpdateVolume()
    {
        int avatarNum = GameObject.FindGameObjectsWithTag("Avatar").Length;
        float newVolume = ((float)avatarNum / 18);
        audio.volume = newVolume;
    }

    private IEnumerator StartAfterSceneLoad()
    {
        yield return new WaitForEndOfFrame();
        GameObject parameters = GameObject.FindGameObjectWithTag("Parameters");
        if (parameters != null)
        {
            bool sound = parameters.GetComponent<ExperimentParameter>().avatar;
            if (sound)
            {
                audio.Play();
            }
            else
            {
                audio.Pause();
            }
        }
        else {
            audio.Play();
        }

    }
}