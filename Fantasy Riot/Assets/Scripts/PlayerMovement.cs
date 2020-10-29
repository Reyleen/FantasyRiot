﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Joystick joystick;
    public Animator topAnim;
    public Animator botAnim;
    private bool playerMoving;
    private bool isShooting;
    private Vector2 lastMove;
    Vector2 move;
    private static bool playerExists;


    public GameObject arrowPrefab;
    public Joystick aimStick;
    Vector2 aim;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Start()
    {

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        aim.x = aimStick.Horizontal;
        aim.y = aimStick.Vertical;
    }
    
    void FixedUpdate() {
        playerMoving = false;
        isShooting = false;

        if (move.x > 0.5f || move.x < -0.5f )
        {
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
            playerMoving = true;
            lastMove = new Vector2(move.x, 0f);
        }

        if (move.y > 0.5f || move.y < -0.5f)
        {
            rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
            playerMoving = true;
            lastMove = new Vector2(0f, move.y);
        }

        if((aim.x > 0.5f || aim.x < -0.5f || aim.y > 0.5f || aim.y < -0.5f) && Time.time > nextFire)
        {
            isShooting = true;
            nextFire = Time.time + fireRate;
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            arrow.GetComponent<Rigidbody2D>().velocity = aim * 10;
            arrow.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg);
            Destroy(arrow, 2.0f);
        }

        botAnim.SetFloat("MoveX", move.x);
        botAnim.SetFloat("MoveY", move.y);
        botAnim.SetBool("PlayerMoving", playerMoving);
        botAnim.SetFloat("LastMoveX", lastMove.x);
        botAnim.SetFloat("LastMoveY", lastMove.y);
        botAnim.SetFloat("AimX", aim.x);
        botAnim.SetFloat("AimY", aim.y);
        botAnim.SetBool("IsShooting", isShooting);


        topAnim.SetFloat("MoveX", move.x);
        topAnim.SetFloat("MoveY", move.y);
        topAnim.SetBool("PlayerMoving", playerMoving);

        topAnim.SetFloat("AimX", aim.x);
        topAnim.SetFloat("AimY", aim.y);
        topAnim.SetBool("IsShooting", isShooting);
    }
}