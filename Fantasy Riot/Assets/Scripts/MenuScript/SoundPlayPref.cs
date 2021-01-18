using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

//this script manage the mixer at the start of every scene
public class SoundPlayPref : MonoBehaviour
{
    public Slider volume;
    public Slider fxVolume;
    public AudioMixer masterMixer;

    void Start()
    {
        masterMixer.SetFloat("effVol", fxVolume.value);
        masterMixer.SetFloat("musVol", volume.value);
        volume.value = PlayerPrefs.GetFloat("VolumeMusica");
        fxVolume.value = PlayerPrefs.GetFloat("VolumeEffetti");
    }
}

