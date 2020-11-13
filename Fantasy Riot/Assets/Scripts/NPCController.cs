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
                movementSpeed = 0.0f;
        }
        else
        {
            movementSpeed = 0.0f;
            movementDirection = Vector2.zero;
            npcRB.velocity = Vector2.zero;
        }
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
                        break;
                    case 1:
                        movementDirection.x -= 1.0f;
                        break;
                    case 2:
                        movementDirection.y += 1.0f;
                        break;
                    case 3:
                        movementDirection.y -= 1.0f;
                        break;
                    default:
                        movementDirection = Vector2.zero;
                        break;
                }
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
