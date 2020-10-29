using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    private PlayerMovement thePlayer;
    private PlayerFollow theCamera;

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
}
