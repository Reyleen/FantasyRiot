using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("Movement Settings:")]
    [Space]
    public bool freeMoving = true;
    public float movementFrequenceThreshold = 1.0f;
    public float movementFrequence = 0.1f;
    public float movementBaseSpeed = 1.0f;
    public float movementDuration = 1.0f;
    public Vector2 movementDirection = new Vector2(0.0f, 0.0f);

    public float movementSpeed;
    public float movementFrequenceCounter = 0.0f;
    public float movementDurationCounter = 0.0f;
    public bool shouldMove = false;
    public bool tackled = false;

    public Animator anim;
    private Vector2 lastMove;
    public bool isMoving = false;

    [Header("References:")]
    [Space]
    public Rigidbody2D npcRB;

    void Update()
    {
        if (!tackled)
        {
            if (freeMoving)
            {
                ProcessAutoMovement();
                Move();
            }
            else
            {
                movementSpeed = 0.0f;
                isMoving = false;
            }
        }
        else
        {
            movementSpeed = 0.0f;
            movementDirection = Vector2.zero;
            npcRB.velocity = Vector2.zero;
            isMoving = false;
        }
        anim.SetBool("IsMoving", isMoving);
    }

    void ProcessAutoMovement()
    {
        if (movementFrequenceCounter > movementFrequenceThreshold)
        {
            movementFrequenceCounter = 0.0f;
            shouldMove = true;

            for (int i = 0; i < 2; i++)
            {
                int randomizer = UnityEngine.Random.Range(0, 4);
                switch (randomizer)
                {
                    case 0:
                        movementDirection.x += 1.0f;
                        lastMove = new Vector2(movementDirection.x, 0f);
                        isMoving = true;
                        break;
                    case 1:
                        movementDirection.x -= 1.0f;
                        lastMove = new Vector2(movementDirection.x, 0f);
                        isMoving = true;
                        break;
                    case 2:
                        movementDirection.y += 1.0f;
                        lastMove = new Vector2(0f, movementDirection.y);
                        isMoving = true;
                        break;
                    case 3:
                        movementDirection.y -= 1.0f;
                        lastMove = new Vector2(0f, movementDirection.y);
                        isMoving = true;
                        break;
                    default:
                        movementDirection = Vector2.zero;
                        isMoving = false;
                        break;
                }
                anim.SetFloat("MoveX", movementDirection.x);
                anim.SetFloat("MoveY", movementDirection.y);
                anim.SetFloat("LastmoveX", lastMove.x);
                anim.SetFloat("LastmoveY", lastMove.y);
            }

            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            movementDirection.Normalize();
        }
        else
            movementFrequenceCounter += movementFrequence;

    }

    void Move()
    {
        if (shouldMove)
        {
            if (movementDurationCounter < movementDuration)
            {
                npcRB.velocity = movementDirection * movementSpeed * movementBaseSpeed;
                movementDurationCounter += Time.deltaTime;
            }
            else
            {
                movementDurationCounter = 0.0f;
                shouldMove = false;
                npcRB.velocity = Vector2.zero;
                movementSpeed = 0.0f;
            }
        }
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        tackled = true;
        if (collision.transform.tag == "Player")
        {
            Vector2 positionRelative = transform.InverseTransformPoint(collision.transform.position);
            movementDirection = positionRelative;
            isMoving = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!(collision.transform.tag == "Player"))
            tackled = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        tackled = false;
    }
}
