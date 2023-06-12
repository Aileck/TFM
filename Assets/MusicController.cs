using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicController : MonoBehaviour
{
    public bool IsBGM;
    public bool IsSFX;
    AudioSource audio;
    public string[] stringArray; 
    public AudioClip[] audioClipArray;
    public string nowPlaying;

    int initialAvatarNum;

    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();

    Scene currentScene;

    void Start()
    {
        
        audio = this.GetComponent<AudioSource>();
        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        for (int i = 0; i < stringArray.Length; i++)
        {
            string str = stringArray[i];
            AudioClip clip = audioClipArray[i];

            clips.Add(str, clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBGM) {
            PlayBGM();
        }

        if (IsSFX) {
            PlaySFX();
        }

    }

    AudioClip GetMusic(string name)
    {
        AudioClip clip;
        if (clips.TryGetValue(name, out clip))
        {
            nowPlaying = name;
            audio.Play(); 
        }
        else
        {
            Debug.LogWarning("Cammpt found：" + name);
        }
        return clip;

    }
    void PlayBGM()
    {
        if (currentScene.name.StartsWith("Office"))
        {
            if (!LevelManager.fire)
            {
                audio.clip = GetMusic("office_atmosphere");
                audio.loop = true;
                audio.Play();
            }
            else if (LevelManager.fire)
            {
                if (nowPlaying == "office_atmosphere")
                {

                    audio.clip = GetMusic("office_fire_alarm");
                    audio.loop = false;
                    audio.volume = 0.7f;
                    audio.Play();


                }
                if (nowPlaying == "office_fire_alarm")
                {
                    if (!audio.isPlaying)
                    {
                        audio.clip = GetMusic("office_shock");
                        audio.loop = false;
                        audio.volume = 1f;
                        audio.Play();


                    }

                }
                if (nowPlaying == "office_shock")
                {
                    if (!audio.isPlaying)
                    {
                        audio.clip = GetMusic("office_nervous"); ;
                        audio.loop = true;
                        audio.Play();

                        initialAvatarNum = GameObject.FindGameObjectsWithTag("Avatar").Length;
                    }

                }
                if (nowPlaying == "office_nervous") {
                    int avatarNum = GameObject.FindGameObjectsWithTag("Avatar").Length;
                    float newVolume = ((float)avatarNum / (float)initialAvatarNum);
                    audio.volume = newVolume;

                    Debug.Log("Initial: " + (initialAvatarNum));
                    Debug.Log("Now: " + (avatarNum));
                    Debug.Log("Now control volume: " + newVolume);
                }


            }
        }
        else
        {

            Debug.LogError("No music available for scene: " + currentScene.name);
        }

    }

    void PlaySFX()
    {
        if (LevelManager.fire)
        {
            if (!audio.isPlaying && nowPlaying != "office_shock")
            {
                audio.PlayOneShot(GetMusic("office_shock"));
            }


            if (nowPlaying == "office_shock")
            {
                if (!audio.isPlaying)
                {
                    audio.Pause();
                }

            }



        }
        else {

        }
    }
}

