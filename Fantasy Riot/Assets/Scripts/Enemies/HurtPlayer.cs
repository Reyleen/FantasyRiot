﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    public float hitDelay;
    private float nextHitAllowed;
    public Collider2D thisenemy;

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Time.time > nextHitAllowed + hitDelay)
            {
                nextHitAllowed = Time.time + hitDelay;
                other.gameObject.GetComponent<PlayerStatus>().HurtPlayer(damageToGive);

            }
        }

        if(other.gameObject.tag == "Tower")
        {
            if (Time.time > nextHitAllowed + hitDelay)
            {
                nextHitAllowed = Time.time + hitDelay;
                other.gameObject.GetComponent<TowerHealth>().HurtTower(damageToGive);
            }
        }

        if (other.gameObject.tag == "MainTower")
        {
            if (Time.time > nextHitAllowed + hitDelay)
            {
                nextHitAllowed = Time.time + hitDelay;
                other.gameObject.GetComponent<MainTowerHp>().HurtMainTower(damageToGive);
            }
        }
    }

}
