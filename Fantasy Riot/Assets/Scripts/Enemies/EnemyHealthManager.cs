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
    public GameObject heart;
    public Animator anim;
    public Animator anim2;
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
                int a = Random.Range(0, 31);
                Debug.Log(a);
                if (a % 3 == 0)
                {
                    Instantiate(heart, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public void HurtEnemy(int damageToGive, bool id)
    {
        CurrentHealth -= damageToGive;
        if (id)
            StartCoroutine(PlayOneShot());
    }
    public IEnumerator PlayOneShot()
    {
        anim2.SetBool("ok", true);
        yield return null;
        anim2.SetBool("ok", false);
    }
    public void setMaxHealth()
    {
        CurrentHealth = MaxHealth;
    }
}
