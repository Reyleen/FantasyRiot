using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemy : MonoBehaviour
{
    public float initialSpeed;
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
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    bool way = false;

    public Infinitewaves wa1;
    private WaveSpawner wa;
    public bool strada;
    public Transform main;
    GameObject tw;
    private CountTower nTower;

    // Start is called before the first frame update
    private void Awake()
    {
        main = GameObject.FindGameObjectWithTag("MainTower").transform;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        wa = FindObjectOfType<WaveSpawner>();
        wa1 = FindObjectOfType<Infinitewaves>();
        if (wa != null)
        {
            strada = wa.road;
        }
        else
        {
            strada = wa1.road;
        }
        timeBtwShots = startTimeBtwShots;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        seeker = GetComponent<Seeker>();
        tw = GameObject.FindGameObjectWithTag("Tower");
        nTower = FindObjectOfType<CountTower>();
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
        if (enHea.CurrentHealth <= 0)
        {
            isAlive = false;
            return;
        }

        if (isAlive)
        {
            if(nTower.Tot > 0)
            {
                GameObject closestTower = FindClosestTower();

                //torre vicina, player, main lontani
                if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) <= 5 && Vector2.Distance(transform.position, main.position) >= 5 && Vector2.Distance(transform.position, player.position) >= 5)
                {
                    FollowTower(closestTower);
                }

                //torre, player vicini, main lontana
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) <= 5 && Vector2.Distance(transform.position, main.position) >= 5 && Vector2.Distance(transform.position, player.position) <= 5)
                {
                    FollowTower(closestTower);
                }

                //torre, main vicini, player lontano
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) <= 5 && Vector2.Distance(transform.position, main.position) <= 5 && Vector2.Distance(transform.position, player.position) >= 5)
                {
                    FollowMainTower();
                }

                //torre,player,main vicini
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) <= 5 && Vector2.Distance(transform.position, main.position) <= 5 && Vector2.Distance(transform.position, player.position) <= 5)
                {
                    FollowMainTower();
                }

                //Torre lontana, player, main vicini
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) >= 5 && Vector2.Distance(transform.position, main.position) <= 5 && Vector2.Distance(transform.position, player.position) <= 5)
                {
                    FollowMainTower();
                }

                //Torre,main lontani, player vicino
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) >= 5 && Vector2.Distance(transform.position, main.position) >= 5 && Vector2.Distance(transform.position, player.position) <= 5)
                {
                    FollowPlayer();
                }

                //Torre,player lontani, main vicino
                else if (closestTower != null && Vector2.Distance(transform.position, closestTower.transform.position) >= 5 && Vector2.Distance(transform.position, main.position) <= 5 && Vector2.Distance(transform.position, player.position) >= 5)
                {
                    FollowMainTower();
                }

                //tutti lontani
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

            else if (nTower.Tot == 0)
            {
                //no torri, player, main vicini
                if (nTower.Tot == 0 && Vector2.Distance(transform.position, player.position) <= 5 && Vector2.Distance(transform.position, main.position) <= 5)
                {
                    FollowMainTower();
                }

                //no torri, player vicino, main lontano
                else if (nTower.Tot == 0 && Vector2.Distance(transform.position, player.position) <= 5 && Vector2.Distance(transform.position, main.position) >= 5)
                {
                    FollowPlayer();
                }

                //no torri, player lontano, main vicino
                else if (nTower.Tot == 0 && Vector2.Distance(transform.position, player.position) >= 5 && Vector2.Distance(transform.position, main.position) <= 5)
                {
                    FollowMainTower();
                }

                //tutti lontani
                else if(nTower.Tot == 0 && Vector2.Distance(transform.position, player.position) >= 5 && Vector2.Distance(transform.position, main.position) >= 5)
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

            if (curDistance < distance)
            {
                closest = towerPos;
                distance = curDistance;
            }
        }

        return closest;
    }

    void FollowTower(GameObject closestTower)
    {
        Vector2 dir = closestTower.transform.position - transform.position;

        if (Vector2.Distance(transform.position, closestTower.transform.position) > 2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTower.transform.position, speed * Time.deltaTime);
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);

        }
        else if (Vector2.Distance(transform.position, closestTower.transform.position) < 2f && Vector2.Distance(transform.position, closestTower.transform.position) > 1f)
        {
            transform.position = this.transform.position;
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);

        }
        else if (Vector2.Distance(transform.position, closestTower.transform.position) < 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTower.transform.position, -speed * Time.deltaTime);
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);
        }

        if (timeBtwShots <= 0)
        {
            isAttacking = true;
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            EnemyBullet bull = bullet.GetComponent<EnemyBullet>();
            bull.GoToTower(closestTower);
            timeBtwShots = startTimeBtwShots;
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
    }

    void FollowPlayer()
    {
        Vector2 dir = player.position - transform.position;

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
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            EnemyBullet bull = bullet.GetComponent<EnemyBullet>();
            bull.GoToPlayer();
            timeBtwShots = startTimeBtwShots;
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
    }

    void FollowMainTower()
    {
        Vector2 dir = main.position - transform.position;

        if (Vector2.Distance(transform.position, main.position) > 4f)
        {
            transform.position = Vector2.MoveTowards(transform.position, main.position, speed * Time.deltaTime);
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);

        }
        else if (Vector2.Distance(transform.position, main.position) < 4f && Vector2.Distance(transform.position, main.position) > retreatDistance)
        {
            transform.position = this.transform.position;
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);

        }
        else if (Vector2.Distance(transform.position, main.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, main.position, -speed * Time.deltaTime);
            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);
        }

        if (timeBtwShots <= 0)
        {
            isAttacking = true;
            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
            EnemyBullet bull = bullet.GetComponent<EnemyBullet>();
            bull.GoToMainTower();
            timeBtwShots = startTimeBtwShots;
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
    }

    void GetNextWaypoint()
    {
        if (strada)
        {
            if (waypointIndex >= Waypoints.points.Length - 1)
            {
                return;
            }

            waypointIndex++;
            target = Waypoints.points[waypointIndex];
            way = false;
        } else
        {
            if (waypointIndex >= Waypoints1.points.Length - 1)
            {
                return;
            }

            waypointIndex++;
            target = Waypoints1.points[waypointIndex];
            way = false;
        }
    }
}
