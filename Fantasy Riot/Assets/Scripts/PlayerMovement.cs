using System.Collections;
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

    private Animator anim;
    private bool playerMoving;

    private Vector2 lastMove;
    Vector2 move;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
    }

    void FixedUpdate()
    {
        playerMoving = false;
        if (move.x > 0.5f || move.x < -0.5f)
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

        anim.SetFloat("MoveX", move.x);
        anim.SetFloat("MoveY", move.y);
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
}
