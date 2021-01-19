using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : MonoBehaviour
{
    public bool slowed = false;
    public float SlowInput;
    private bool stunned = false;
    public bool ranged;
    public bool assassin;
    public Rigidbody2D rb;
    public Enemy enemySpeed;
    public RangedEnemy rangedSpeed;
    public EnemyAssassin assassinSpeed;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void DebuffSlow()
    {
        if(ranged == true)
        {
            if (slowed == false)
            {
                rangedSpeed.speed = rangedSpeed.speed - SlowInput;
                slowed = true;
            }
        }

        else if(assassin == true)
        {
            if (slowed == false)
            {
                assassinSpeed.speed = assassinSpeed.speed - SlowInput;
                slowed = true;
            }
        }

        else
        {
            if (slowed == false)
            {
                enemySpeed.speed = enemySpeed.speed - SlowInput;
                slowed = true;
            }
        }
    }

    public void RemoveBuff()
    {
        if (ranged == true)
        {
            if (slowed == true)
            {
                rangedSpeed.speed = rangedSpeed.initialSpeed;
                slowed = false;
            }
        }

        else if (assassin == true)
        {
            if (slowed == true)
            {
                assassinSpeed.speed = assassinSpeed.initialSpeed;
                slowed = false;
            }
        }

        else
        {
            if (slowed == true)
            {
                enemySpeed.speed = enemySpeed.initialSpeed;
                slowed = false;
            }
        }
        
    }

    public IEnumerator KnockUp(float knockDur, float knockPow, Vector3 knockDir)
    {
        float timerKnock = 0;

            while (knockDur > timerKnock)
            {
                timerKnock += Time.deltaTime;
                rb.AddForce(new Vector3(knockDir.x, knockDir.y * knockPow, transform.position.z));
            }
            
        yield return 0;
    }

    public IEnumerator KnockBack(float knockDur, float knockPow, Vector3 knockDir)
    {
        float timerKnock = 0;

        while (knockDur > timerKnock)
        {
            timerKnock += Time.deltaTime;
            rb.AddForce(new Vector3(-knockDir.x * knockPow, -knockDir.y * knockPow, transform.position.z));
        }

        yield return 0;
    }

    public void Stun()
    {
        if (ranged)
        {
            if (stunned == false)
            {
                rangedSpeed.speed = 0;
            }
        }

        else if (assassin)
        {
            if (stunned == false)
            {
                assassinSpeed.speed = 0;
            }
        }
        
        else 
        {
            if (stunned == false)
            {
                enemySpeed.speed = 0;
            }
        }

    }

    public void NotStun()
    {
        if (ranged)
        {
            rangedSpeed.speed = rangedSpeed.initialSpeed;
            stunned = false;
        }

        else if (assassin)
        {
            assassinSpeed.speed = assassinSpeed.initialSpeed;
            stunned = false;
        }

        else if(!ranged)
        {
            enemySpeed.speed = enemySpeed.initialSpeed;
            stunned = false;
        }
    }
}
