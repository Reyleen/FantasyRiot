using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeMenuManager : MonoBehaviour
{
    public AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music.volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
