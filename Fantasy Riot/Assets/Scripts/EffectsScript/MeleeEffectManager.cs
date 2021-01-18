using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEffectManager : MonoBehaviour
{
    public AudioSource step;
    public AudioSource attckhit;
    public AudioSource Death;
    public PlayerStatus ps;
    public PlayerMovmentMelee player;
    public MeleeAttack melee;
    public bool done;
    // Start is called before the first frame update
    void Start()
    {
        done = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isShooting)
        {
            if (melee.att && !attckhit.isPlaying)
            {
                attckhit.Play();
            }
        }
        if (player.playerMoving)
        {
            if (!step.isPlaying)
                step.Play();
        }
        else
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
