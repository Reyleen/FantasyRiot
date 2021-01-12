 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
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
    GameObject tw;
    private CountTower nTower;

    public WaveSpawner wa;
    public bool strada;

    private Transform main;
    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        wa = FindObjectOfType<WaveSpawner>();
        strada = wa.road;
        tw = GameObject.FindGameObjectWithTag("Tower");
        nTower = FindObjectOfType<CountTower>();
        main = GameObject.FindGameObjectWithTag("MainTower").GetComponent<Transform>();

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
                Debug.Log(nTower.Tot);
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (nTower.Tot >= 1)
                {
                    Debug.Log("Tower!!!");
                    GameObject closestTower = FindClosestTower();
                    if (closestTower != null)
                    {
                        float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);

                        if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) < 3)
                        {
                            FollowTower();
                        }
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
                        Vector2 force = Vector2.zero;//direction * speed * Time.deltaTime;
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
                
                if (nTower.Tot >= 1 && Vector2.Distance(transform.position, player.position) < 3)
                {
                    GameObject closestTower = FindClosestTower();
                    Debug.Log("Tower!!");
                    
                    if (closestTower != null)
                    {
                        float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);

                        if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) < 3)
                        {
                            FollowTower();
                        }

                        if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3)
                        {
                            FollowPlayer();
                        }
                    }
                }

                if (nTower.Tot >= 1 && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, main.position) < 3)
                {
                    GameObject closestTower = FindClosestTower();
                    Debug.Log("Tower!!");

                    if (closestTower != null)
                    {
                        float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);

                        if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) < 3 && Vector2.Distance(transform.position, main.position) < 3)
                        {
                            FollowTower();
                        }

                        if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                        {
                            FollowMainTower();
                        }
                    }
                }

                if (nTower.Tot <= 0 && Vector2.Distance(transform.position, player.position) < 3)
                {
                    FollowPlayer();
                }

                if (nTower.Tot <= 0 && Vector2.Distance(transform.position, main.position) < 3 && Vector2.Distance(transform.position, player.position) < 3)
                {
                    FollowMainTower();
                }

                if(nTower.Tot <= 0 && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                {
                    FollowMainTower();
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

    void FollowTower()
    {
        Debug.Log("Goint to the tower");
        GameObject closestTower = FindClosestTower();
        float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);
        if (distanceToTower < attackRange)
        {
            isAttacking = false;
            Vector2 dir = closestTower.transform.position - transform.position;

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
            transform.position = Vector2.MoveTowards(transform.position, closestTower.transform.position, speed * Time.deltaTime);
            Vector2 dir = closestTower.transform.position - transform.position;

            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);
        }
    }

    void FollowMainTower()
    {
        Debug.Log("Going to nexus");
        float distanceToMain = Vector2.Distance(transform.position, main.position);
        if (distanceToMain < attackRange)
        {
            isAttacking = false;
            Vector2 dir = main.position - transform.position;

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
            transform.position = Vector2.MoveTowards(transform.position, main.position, speed * Time.deltaTime);
            Vector2 dir = main.position - transform.position;

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

    GameObject FindClosestTower()
    {
        GameObject[] twPos;
        twPos = GameObject.FindGameObjectsWithTag("Tower");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        
        foreach (GameObject towerPos in twPos)
        {
            Vector3 diff = towerPos.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            
            if (curDistance < distance && curDistance < 3)
            {
                closest = towerPos;
                distance = curDistance;
            }
        }

        Vector3 pos = closest.transform.position;

        return closest;
    }
}