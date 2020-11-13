using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZProvaEnemy : MonoBehaviour
{
    public float speed = 2f;

    private Transform target;
    private int wavepointIndex = 0;

    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        target = Waypoints.points[0];
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
            Vector2 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector2.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }

    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length -1)
        {
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }
}
