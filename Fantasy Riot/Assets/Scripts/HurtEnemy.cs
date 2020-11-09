using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageToGive;
    public Rigidbody2D rb;
    private bool hasHit;

    // Start is called before the first frame update
    void Start()
    {
        hasHit = false;
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
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            this.transform.parent = other.transform;
        } 
        
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Coin")
        {

        } else
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }  


    }

}

