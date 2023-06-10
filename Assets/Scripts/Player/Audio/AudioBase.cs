using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBase : MonoBehaviour
{
    [SerializeField] List<AudioSource> listClip = new List<AudioSource>();
    int ranClip;

    private void OnEnable()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TMT_RandomAudioClip()
    {
        ranClip = Random.Range(0, listClip.Count);
        listClip[ranClip].Play();
    }
}