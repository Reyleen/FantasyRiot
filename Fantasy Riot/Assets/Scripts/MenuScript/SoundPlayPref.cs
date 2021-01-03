using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPlayPref : MonoBehaviour
{
    public AudioSource music;
    public AudioSource FX;
    public Slider volume;
    public Slider fxVolume;
    void Start()
    {
        volume.value = PlayerPrefs.GetFloat("MusicVolume");
        fxVolume.value = PlayerPrefs.GetFloat("FxVolume");
    }
    public void MusicVolumePrefs()
    {
        PlayerPrefs.SetFloat("MusicVolume",music.volume);

    }
    public void FXVolumePrefs()
    {
        PlayerPrefs.SetFloat("FxVolume", fxVolume.value);
    }
}

