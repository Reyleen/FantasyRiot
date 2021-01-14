using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAssassin : MonoBehaviour
{
    public float initialSpeed;
    public float speed = 2f;

    public Transform target;
    private int wavepointIndex = 0;
    public Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    private float lastAttackTime;
    public bool isAttacking;
    public float attackDelay;
    public float attackRange;

    public EnemyHealthManager enHea;
    public bool isAlive = true;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    bool way = false;

    public WaveSpawner wa;
    public bool strada;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        wa = FindObjectOfType<WaveSpawner>();
        strada = wa.road;

        if (strada)
        {
            target = Waypoints.points[0];
        }
        else
        {
            target = Waypoints1.points[0];
        }
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.zero;

        if (player != null)
        {
            if (enHea.CurrentHealth <= 0)
            {
                isAlive = false;
                return;
            }
            if (isAlive)
            {
                if (Vector2.Distance(transform.position, player.position) < 5)
                {
                    FollowPlayer();
                }
                else
                {
                    Debug.Log("Going to the waypoint");
                    if (path == null)
                        return;
                    if (currentWaypoint >= path.vectorPath.Count)
                    {
                        if (way)
                            GetNextWaypoint();
                        reachedEndOfPath = true;
                        return;
                    }
                    else
                    {
                        way = true;
                        reachedEndOfPath = false;
                    }
                    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                    Vector2 force = direction * speed * Time.deltaTime;
                    transform.Translate(force, Space.World);
                    Vector2 dir = target.position - transform.position;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    anim.SetFloat("AngleX", dir.x);
                    anim.SetFloat("AngleY", dir.y);
                    float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
                    if (distance < nextWaypointDistance)
                    {
                        currentWaypoint++;
                    }
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    void FollowPlayer()
    {
        Debug.Log("Going to the player");
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            isAttacking = false;
            Vector2 dir = player.position - transform.position;

            if (Time.time > lastAttackTime + attackDelay)
            {
                lastAttackTime = Time.time;
                isAttacking = true;
                rb.velocity = Vector2.zero;
            }
            anim.SetFloat("AttX", dir.x);
            anim.SetFloat("AttY", dir.y);
            anim.SetBool("isAttacking", isAttacking);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            Vector2 dir = player.position - transform.position;

            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);
        }
    }

    void GetNextWaypoint()
    {
        if (strada)
        {
            if (wavepointIndex >= Waypoints.points.Length - 1)
            {
                return;
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];
            way = false;
        }
        else
        {
            if (wavepointIndex >= Waypoints1.points.Length - 1)
            {
                return;
            }

            wavepointIndex++;
            target = Waypoints1.points[wavepointIndex];
            way = false;
        }
    }
}
