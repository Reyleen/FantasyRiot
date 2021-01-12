using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debuffs : MonoBehaviour
{
    public bool slowed = false;
    public float SlowInput;
    private bool stunned = false;
    public bool ranged;
    public Rigidbody2D rb;
    public Enemy enemySpeed;
    public RangedEnemy rangedSpeed;
    
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
                Debug.Log("Getting slowed");
            }
        }
        
        else if (!ranged)
        {
            if (slowed == false)
            {
                enemySpeed.speed = enemySpeed.speed - SlowInput;
                slowed = true;
                Debug.Log("Getting slowed");
            }
        }
    }

    public void RemoveBuff()
    {
        if (ranged == true)
        {
            if (slowed == false)
            {
                rangedSpeed.speed = rangedSpeed.speed + SlowInput;
                slowed = false;
            }
        }

        else if (!ranged)
        {
            if (slowed == false)
            {
                enemySpeed.speed = enemySpeed.speed + SlowInput;
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

    public void Stun()
    {
        if (ranged)
        {
            if (stunned == false)
            {
                rangedSpeed.speed = 0;
            }
        }

        else if(!ranged)
        {
            if (stunned == false)
            {
                enemySpeed.speed = 0;
            }
        }

    }

    public void NotStun()
    {
        if(ranged)
        {
            rangedSpeed.speed = rangedSpeed.initialSpeed;
            stunned = false;
        }

        else if(!ranged)
        {
            enemySpeed.speed = enemySpeed.initialSpeed;
            stunned = false;
        }
    }
}
