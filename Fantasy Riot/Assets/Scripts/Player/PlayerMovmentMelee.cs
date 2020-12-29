using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Movment system for Melee Hero*/
public class PlayerMovmentMelee : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    private Joystick joystick;
    public Animator topAnim;
    public Animator botAnim;
    private bool playerMoving;
    private bool isShooting;
    private Vector2 lastMove;
    Vector2 move;
    private static bool playerExists;
    private MeleeAttack att;
    private Joystick aimStick;
    Vector2 aim;

    public float fireRate = 0.5F;
    private float nextFire = 0.0F;

    void Start()
    {
        att = this.GetComponent<MeleeAttack>();
        joystick = FindObjectOfType<FixedJoystick>();
        aimStick = FindObjectOfType<VariableJoystick>();
    }

    void Update()
    {
        

        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        aim.x = aimStick.Horizontal;
        aim.y = aimStick.Vertical;
    }


    void FixedUpdate()
    {
        playerMoving = false;
        isShooting = false;
        rb.velocity = Vector2.zero;
        //direction check
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
        //aiming check
        if ((aim.x > 0.5f || aim.x < -0.5f || aim.y > 0.5f || aim.y < -0.5f) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            att.Attack(aim.x,aim.y);
        }

        if (aim.x > 0.5f || aim.x < -0.5f || aim.y > 0.5f || aim.y < -0.5f)
        {
            isShooting = true;
        }
        //animation set
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
        topAnim.SetFloat("LastMoveX", lastMove.x);
        topAnim.SetFloat("LastMoveY", lastMove.y);

        topAnim.SetFloat("AimX", aim.x);
        topAnim.SetFloat("AimY", aim.y);
        topAnim.SetBool("IsShooting", isShooting);
    }
}
