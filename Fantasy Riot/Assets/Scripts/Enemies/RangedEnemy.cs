using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Rigidbody2D rb;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        //target = Waypoints.points[0];
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
    }

}
