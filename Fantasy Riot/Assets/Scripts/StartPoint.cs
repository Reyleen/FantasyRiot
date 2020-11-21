using System.Collections;
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
    private PlayerHealthManager plaHea;

    // Start is called before the first frame update
    void Start()
    {
         wave = FindObjectOfType<WaveSpawner>();
         waveCounter = GameObject.Find("WaveCounter").GetComponent<Text>();

         thePlayer = FindObjectOfType<PlayerMovement>();
         thePlayer.transform.position = transform.position;

         theCamera = FindObjectOfType<PlayerFollow>();
         theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z); 

    }

    // Update is called once per frame
    void Update()
    {
        plaHea = FindObjectOfType<PlayerHealthManager>();
        waveCounter.text = "Wave: " + (wave.nextWave + 1);

        if(plaHea.playerCurrentHealth <= 0)
        {
            waveCounter.enabled = false;
        } else
        {
            waveCounter.enabled = true;
        }
    }
}
