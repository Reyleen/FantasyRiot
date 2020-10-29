using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {   // Find as a target the gameObject with the tag "Player" on it
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {   // If the distance from the target is more than 2 reach him until you are in that distance
        if(Vector2.Distance(transform.position, target.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
