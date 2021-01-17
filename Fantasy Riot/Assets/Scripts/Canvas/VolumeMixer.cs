using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeMixer : MonoBehaviour
{
    public AudioMixer masterMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEffect(float effVol)
    {
        
        masterMixer.SetFloat("effVol", effVol);
        PlayerPrefs.SetFloat("VolumeEffetti", effVol);
    }

    public void SetMusic(float musVol)
    {
        masterMixer.SetFloat("musVol", musVol);
        PlayerPrefs.SetFloat("VolumeMusica", musVol);
    }
}
