﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public AudioSource step;
    public AudioSource Death;
    public PlayerStatus ps;
    public PlayerMovement player;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        done = true;
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

        if (ps.currentHp <= 0)
        {
            if (done)
            {
                Death.Play();
                done = false;
            }
        }
    }
}
