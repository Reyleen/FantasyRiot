using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object

    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    private static bool cameraExists;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
        DontDestroyOnLoad(transform.gameObject);

        if(!cameraExists)
        {
            cameraExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }   else
        {
            Destroy(gameObject);
        }
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        transform.position = player.transform.position + offset;
    }
}