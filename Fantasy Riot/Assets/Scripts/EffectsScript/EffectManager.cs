using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public AudioSource step;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(player.playerMoving)
        {
            if(!step.isPlaying)
            step.Play();
        } else
        {
            step.Stop();
        }
    }
}
