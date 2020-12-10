﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;

    private Transform target;
    private int wavepointIndex = 0;
    public Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    private float lastAttackTime;
    public bool isAttacking;
    public float attackDelay;
    public float attackRange;

    public EnemyHealthManager enHea;
    public bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector2.zero;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if(enHea.CurrentHealth <= 0)
        {
            isAlive = false;
        }

        if (isAlive)
        {
            if (Vector2.Distance(transform.position, player.position) < 3)
            {
                if (distanceToPlayer < attackRange)
                {
                    isAttacking = false;
                    Vector2 dir = player.position - transform.position;

                    if (Time.time > lastAttackTime + attackDelay)
                    {
                        lastAttackTime = Time.time;
                        isAttacking = true;
                        rb.velocity = Vector2.zero;
                    }
                    anim.SetFloat("AttX", dir.x);
                    anim.SetFloat("AttY", dir.y);
                    anim.SetBool("isAttacking", isAttacking);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    Vector2 dir = player.position - transform.position;

                    anim.SetFloat("AngleX", dir.x);
                    anim.SetFloat("AngleY", dir.y);
                }
            }
            else
            {
                Vector2 dir = target.position - transform.position;
                transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                if (Vector2.Distance(transform.position, target.position) <= 0.4f)
                {
                    GetNextWaypoint();
                }

                anim.SetFloat("AngleX", dir.x);
                anim.SetFloat("AngleY", dir.y);
            }
        } else
        {
            rb.velocity = Vector2.zero;
        }


    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}