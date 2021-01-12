using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeart : MonoBehaviour
{
    public int value;
    public PlayerHealthManager theHM;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        theHM = FindObjectOfType<PlayerHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            theHM.HurtPlayer(value);
            Destroy(gameObject);
        }
    }
}
