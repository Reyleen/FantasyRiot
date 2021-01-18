using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class IntroMusic : MonoBehaviour
{
    public AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("VolumeMusica"))
        {
            masterMixer.SetFloat("effVol", PlayerPrefs.GetFloat("VolumeEffetti"));
            masterMixer.SetFloat("musVol", PlayerPrefs.GetFloat("VolumeMusica"));
        }
        else
        {
            masterMixer.SetFloat("effVol", PlayerPrefs.GetFloat("VolumeEffetti"));
            masterMixer.SetFloat("musVol", PlayerPrefs.GetFloat("VolumeMusica"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
