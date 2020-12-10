using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public GameObject coin;
    public Animator anim;
    public Rigidbody2D rb;
    public float drop;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 1f);
            if (Time.time >= drop)
            {
                drop = Time.time + 2;
                Instantiate(coin, transform.position, Quaternion.identity);
            }
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        CurrentHealth -= damageToGive;
    }

    public void setMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
