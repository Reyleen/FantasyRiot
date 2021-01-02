using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemy : MonoBehaviour
{
    public float speed;
    public float stopDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    private Transform player;

    public EnemyHealthManager enHea;
    public bool isAlive = true;
    public bool isAttacking;
    private Transform target;
    private int waypointIndex = 0;
    private Rigidbody2D rb;
    private Animator anim;

    private Transform enemyPosition;
    public Point GridPosition { get; set; }

    public bool slowed = false;
    public float SlowInput;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    bool way = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        target = Waypoints.points[0];
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
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
    void FixedUpdate()
    {
        Vector2 dir = player.position - transform.position;

        if (enHea.CurrentHealth <= 0)
        {
            isAlive = false;
            return;
        }

        if (isAlive)
        {
            if (Vector2.Distance(transform.position, player.position) < 8)
            {
                if (Vector2.Distance(transform.position, player.position) > stopDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    anim.SetFloat("AngleX", dir.x);
                    anim.SetFloat("AngleY", dir.y);

                }
                else if (Vector2.Distance(transform.position, player.position) < stopDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                    anim.SetFloat("AngleX", dir.x);
                    anim.SetFloat("AngleY", dir.y);

                }
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                    anim.SetFloat("AngleX", dir.x);
                    anim.SetFloat("AngleY", dir.y);
                }

                if (timeBtwShots <= 0)
                {
                    isAttacking = true;
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                    Destroy(projectile, 2.0f);
                    anim.SetFloat("AttX", dir.x);
                    anim.SetFloat("AttY", dir.y);
                    anim.SetBool("isAttacking", isAttacking);
                }
                else
                {
                    isAttacking = false;
                    timeBtwShots -= Time.deltaTime;
                    anim.SetBool("isAttacking", isAttacking);
                }
            } else
            {
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
        
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            return;
        }

        waypointIndex++;
        target = Waypoints.points[waypointIndex];
        way = false;
    }

    public void DebuffSlow()
    {
        if (slowed == false)
        {
            speed = speed - SlowInput;
            slowed = true;
            Debug.Log("Getting slowed");
        }
    }

    public void RemoveBuff()
    {
        speed = speed + SlowInput;
        slowed = false;
    }
}
