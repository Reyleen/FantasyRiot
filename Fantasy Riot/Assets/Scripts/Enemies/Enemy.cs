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
    public Infinitewaves wa1;
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
        wa1 = FindObjectOfType<Infinitewaves>();
        if (wa != null)
        {
            strada = wa.road;
        } else
        {
            strada = wa1.road;
        }
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
        if (player != null)
        {
            if (enHea.CurrentHealth <= 0)
            {
                isAlive = false;
                return;
            }
            if (isAlive)
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.position);

                if (nTower.Tot >= 1)
                {
                    GameObject closestTower = FindClosestTower();

                    if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) < 3 && Vector2.Distance(transform.position, main.position) > 3 && Vector2.Distance(transform.position, player.position) > 3)
                    {
                        float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);
                        FollowTower(closestTower);
                    }
                    else if (nTower.Tot >= 1 && Vector2.Distance(transform.position, player.position) < 3)
                    {
                        closestTower = FindClosestTower();

                        if (closestTower != null)
                        {
                            float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);

                            if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) < 3 && Vector2.Distance(transform.position, main.position) > 3)
                            {
                                FollowTower(closestTower);
                            }

                            if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3 && Vector2.Distance(transform.position, main.position) > 3)
                            {
                                FollowPlayer();
                            }
                        }
                    }
                    else if (nTower.Tot >= 1 && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, main.position) < 3)
                    {
                        closestTower = FindClosestTower();

                        if (closestTower != null)
                        {
                            float distanceToTower = Vector2.Distance(transform.position, closestTower.transform.position);

                            if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) < 3 && Vector2.Distance(transform.position, main.position) < 3)
                            {
                                FollowMainTower();
                            }

                            if (closestTower != null && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                            {
                                FollowMainTower();
                            }

                            if (closestTower != null && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                            {
                                FollowMainTower();
                            }
                        }
                    }
                    
                    else if (nTower.Tot >= 1 && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                    {
                        closestTower = FindClosestTower();

                        if (closestTower != null && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, closestTower.transform.position) < 3 && Vector2.Distance(transform.position, main.position) < 3)
                        {
                            FollowTower(closestTower);
                        }
                        if (closestTower != null && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, closestTower.transform.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                        {
                            FollowMainTower();
                        }
                    }

                    else
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

                else if (nTower.Tot <= 0 && Vector2.Distance(transform.position, player.position) < 3 && Vector2.Distance(transform.position, main.position) > 3)
                {
                    FollowPlayer();
                }

                else if (nTower.Tot <= 0 && Vector2.Distance(transform.position, main.position) < 3 && Vector2.Distance(transform.position, player.position) < 3)
                {
                    FollowMainTower();
                }

                else if (nTower.Tot <= 0 && Vector2.Distance(transform.position, player.position) > 3 && Vector2.Distance(transform.position, main.position) < 3)
                {
                    FollowMainTower();
                }

                else
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

    void FollowTower(GameObject closestTower)
    {
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
        GameObject closest = null;
        GameObject[] twPos;
        twPos = GameObject.FindGameObjectsWithTag("Tower");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject towerPos in twPos)
        {
            Vector3 diff = towerPos.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = towerPos;
                distance = curDistance;
            }
        }
        return closest;
    }
}