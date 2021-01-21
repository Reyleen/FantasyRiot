using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGoldTutorial : MonoBehaviour
{
    public int value;
    public GoldManager theGM;
    private Transform target;
    public float speed;
    public AudioSource mon;

    // Start is called before the first frame update
    void Start()
    {
        theGM = FindObjectOfType<GoldManager>();
        mon = GameObject.Find("Effects").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            theGM.AddMoney(value);
            mon.Play();
            Destroy(gameObject);
        }
    }
}
