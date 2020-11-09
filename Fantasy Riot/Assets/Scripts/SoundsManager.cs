using System.Collections;
using System.Collections.Generic;
using System.Runtime.Versioning;
using UnityEngine;
using System.Diagnostics;

public class SoundsManager : MonoBehaviour
{
    public static AudioClip coinSound;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        coinSound = Resources.Load<AudioClip>("monei");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "coinSound":
                audioSrc.PlayOneShot(coinSound);
                break;
                
        }
    }
}
