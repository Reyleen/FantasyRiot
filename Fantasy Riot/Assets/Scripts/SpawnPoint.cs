using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private PlayerMovement thePlayer;
    private PlayerFollow theCamera;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerMovement>();
        thePlayer.transform.position = transform.position;

        theCamera = FindObjectOfType<PlayerFollow>();
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
