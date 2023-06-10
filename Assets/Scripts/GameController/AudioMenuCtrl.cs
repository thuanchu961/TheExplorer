using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioMenuCtrl : MonoBehaviour
{
    [SerializeField] GameObject musicGameplay;
    [SerializeField] Slider masterVolume, musicVolume, sfxVolume;
    float numMusicVolume, numSFXVolume;
    Scene sceneTMT;
    [SerializeField]
    List<AudioSource> listSFXVolume = new List<AudioSource>();
    AudioSource[] temp;
    float oldListCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        sceneTMT = SceneManager.GetActiveScene();
        // masterVolume.onValueChanged.AddListener((volume) =>
        // {
        //     PlayerPrefs.SetFloat("masterVolume", volume);
        //     musicGameplay.gameObject.GetComponent<AudioSource>().volume = volume;
        // });

        if (PlayerPrefs.HasKey("musicVolume"))
        {
            numMusicVolume = PlayerPrefs.GetFloat("musicVolume");
            musicVolume.value = numMusicVolume;
        }

        musicVolume.onValueChanged.AddListener((volume) =>
        {
            PlayerPrefs.SetFloat("musicVolume", volume);
            musicGameplay.gameObject.GetComponent<AudioSource>().volume = volume;
        });

        sfxVolume.onValueChanged.AddListener((volume) =>
        {
            PlayerPrefs.SetFloat("sfxVolume", volume);
            if (listSFXVolume.Count > 0)
                foreach (var i in listSFXVolume)
                {
                    i.volume = volume;
                }
        });

        TMT_FindAllAudioSource();
        AddNumForVolume();

        oldListCount = listSFXVolume.Count;
    }

    // Update is called once per frame
    void Update()
    {
        temp = GameObject.FindObjectsOfType<AudioSource>();

        if (temp.Length != listSFXVolume.Count + 1)
        {
            TMT_FindAllAudioSource();
            AddNumForVolume();
        }
    }

    public void TMT_RestartLvl()
    {
        SceneManager.LoadScene(sceneTMT.buildIndex);
    }

    public void TMT_FindAllAudioSource()
    {
        List<AudioSource> temp2 = new List<AudioSource>();
        temp = GameObject.FindObjectsOfType<AudioSource>();

        foreach (var i in temp)
        {
            if (!i.CompareTag("MasterMusic"))
            {
                temp2.Add(i);
            }
        }

        listSFXVolume = temp2;
    }

    void AddNumForVolume()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            numSFXVolume = PlayerPrefs.GetFloat("sfxVolume");
            sfxVolume.value = numSFXVolume;
            if (listSFXVolume.Count > 0)
                foreach (var i in listSFXVolume)
                {
                    i.volume = numSFXVolume;
                }
        }
    }
}
