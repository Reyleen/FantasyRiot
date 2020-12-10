using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{
    // Reference to Audio Source component
    private AudioSource audioSrc;

    // Music volume variable that will be modified by dragging slider knob
    private float musicVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // Assign Audio Source component to control it
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = musicVolume;
    }

    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }
}
