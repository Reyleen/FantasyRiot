using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PROVAPlayerController : MonoBehaviour
{
    [Header("General:")]
    [Space]
    public bool npcTackled = false;
    public Collision2D tackledNPC;


    [Header("Movement Settings:")]
    [Space]
    public float movementBaseSpeed = 1.0f;
    public Vector2 movementDirection = Vector2.zero;
    public float movementSpeed = 0.0f;
    public bool canMove = true;

    [Header("References:")]
    [Space]
    public Rigidbody2D playerRB;


    void Update()
    {
        if (canMove)
        {
            ProcessMovementInputs();
            Move();
        }
    }
    #region Movement Handling
    void ProcessMovementInputs()
    {
        //reset that we are moving
        movementSpeed = 0.0f;

        //get the absolut inpuit from arrow keys to decide in which direction to move the player
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");

        //if the movement direction is not equal to the zero vector we will define the movmentspeed and declare that the player is actually moving
        if (movementDirection != Vector2.zero)
        {
            //clamp the movementdirections magnitude between 0 and 1, so nobody cheat with special input devices (xbox controllers), and assign it as the movementspeed
            movementSpeed = Mathf.Clamp(movementDirection.magnitude, 0.0f, 1.0f);
            //normalize the movement direction, so we are not unrealisticly moving double as fast when using diagonal movement direction
            movementDirection.Normalize();
        }
    }

    void Move()
    {
        //only move the palyer into the direction when he currently not in contact with an NPC
        if (!npcTackled)
        {
            playerRB.velocity = movementDirection * movementSpeed * movementBaseSpeed;
        }
        else
        {
            //get the relative position of the NPC to the player
            Vector2 positionRelative = transform.InverseTransformPoint(tackledNPC.transform.position);
            //if we are stucking at the NPC we need to trick around, so we can leave the NPC's colliding shape again
            //we do this by checking movementDirection (where the player would go to) and get the distance between the NPC's relative position and the movementDirection
            float moveRelative = Vector2.Distance(positionRelative, movementDirection);
            //as if the player is moving away from the NPC the moveRelative will get > 1, so we can assign the normal movement flow
            //if the player would go into the NPC with his movementDirection again, then the moveRelative would be < 1, so we assign vector2.zero velocity to his RB
            if (moveRelative > 1.0f)
            {
                playerRB.velocity = movementDirection * movementSpeed * movementBaseSpeed;
            }
            else
                playerRB.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //only care for collision with NPC
        //other collisions will be treated by the collider components (static structures, that cant get pushed)
        if (collision.transform.tag == "NPC")
        {
            npcTackled = true;
            //save the currently tackled NPC for later uses, e.g. relative position and talking with the NPC
            tackledNPC = collision;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (npcTackled)
        {
            npcTackled = false;
            tackledNPC = null;
        }
    }
    #endregion


}
