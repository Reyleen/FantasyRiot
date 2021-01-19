using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHeart : MonoBehaviour
{
    public int value;
    public PlayerStatus theHM;
    private Transform target;
    public AudioSource heal;

    // Start is called before the first frame update
    void Awake()
    {
        theHM = FindObjectOfType<PlayerStatus>();
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
            heal.Play();
            Destroy(gameObject);
        }
    }
}
