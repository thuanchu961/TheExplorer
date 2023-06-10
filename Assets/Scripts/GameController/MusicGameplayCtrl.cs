using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicGameplayCtrl : MonoBehaviour
{
    float musicVolume;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            GetComponent<AudioSource>().volume = musicVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
