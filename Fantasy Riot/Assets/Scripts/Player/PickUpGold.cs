using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGold : MonoBehaviour
{
    public int value;
    public GoldManager theGM;
    private Transform target;
    public float speed;
    public AudioSource mon;

    // Start is called before the first frame update
    void Start()
    {
        mon = GameObject.Find("Effects").GetComponent<AudioSource>();
        theGM = FindObjectOfType<GoldManager>();
        Destroy(gameObject, 30f);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (Vector2.Distance(transform.position, target.position) < 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            theGM.AddMoney(value);
            mon.Play();
            Destroy(gameObject);
        }
    }
}
