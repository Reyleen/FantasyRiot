﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
    public Rigidbody2D rb;
    public PlayerStatus plaSta;

    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        hit = false;
        damageToGive = plaSta.attack;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" && !hit)
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            hit = true;
            this.transform.parent = other.transform;
        }
        
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Coin" || other.gameObject.tag == "NPC" || other.gameObject.tag == "Tower" || other.gameObject.tag == "RangeTower" || other.gameObject.tag == "Circle")
        {

        } else
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }  


    }

}

