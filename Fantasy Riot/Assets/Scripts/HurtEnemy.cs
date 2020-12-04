using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            this.transform.parent = other.transform;
        }
        
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Coin" || other.gameObject.tag == "NPC")
        {

        } else
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }  


    }

}

