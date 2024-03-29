﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class StartPoint : MonoBehaviour
{
    private PlayerMovement thePlayer;
    private PlayerFollow theCamera;
    private WaveSpawner wave;
    private Text waveCounter;
    private PlayerStatus plaHea;

    // Start is called before the first frame update
    void Start()
    {
        
      //  wave = FindObjectOfType<WaveSpawner>();
      //  waveCounter = GameObject.Find("WaveCounter").GetComponent<Text>();

        thePlayer = FindObjectOfType<PlayerMovement>();
        thePlayer.transform.position = transform.position;

        theCamera = FindObjectOfType<PlayerFollow>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);

        plaHea = FindObjectOfType<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        
       // waveCounter.text = "Wave: " + (wave.nextWave + 1);

        if(plaHea.currentHp <= 0)
        {
            waveCounter.enabled = false;
        } else
        {
            waveCounter.enabled = true;
        }
    }
}
