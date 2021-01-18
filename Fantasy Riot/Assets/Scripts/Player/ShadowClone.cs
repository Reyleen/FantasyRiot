using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : MonoBehaviour
{
    private List<Transform> targets = new List<Transform>();
    private int targetIndex;

    private Rigidbody2D rb;
    public float speed;
    public Transform MyTarget { get; set; }
    public int damage;
    public bool hit = false;
    public float distance;
    public bool isAttacking;
    private Animator anim;

    public float range;
    public float lastAttackTime;
    public float attackDelay;
    public float nextHitAllowed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 15f);
    }

    void FixedUpdate()
    {
       
        //rb.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        MyTarget = FindObjectOfType<EnemyHealthManager>().transform;
        if (MyTarget == null)
        {
            Destroy(gameObject);
        }
        Vector2 dir = MyTarget.position - transform.position;



        if (MyTarget != null)
        {
            distance = Vector2.Distance(transform.position, MyTarget.position);
            transform.position = Vector2.MoveTowards(transform.position, MyTarget.position, speed * Time.deltaTime);
            //Vector2 dir = MyTarget.position - transform.position;

            anim.SetFloat("AngleX", dir.x);
            anim.SetFloat("AngleY", dir.y);
            if (distance < range)
            {
                rb.velocity = Vector2.zero;
                isAttacking = false;

                if (Time.time > lastAttackTime + attackDelay)
                {
                    EnemyHealthManager e = MyTarget.GetComponent<EnemyHealthManager>();
                    e.HurtEnemy(damage,true);
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
                transform.position = Vector2.MoveTowards(transform.position, MyTarget.position, speed * Time.deltaTime);
                isAttacking = false;
                anim.SetBool("isAttacking", isAttacking);
                anim.SetFloat("AngleX", dir.x);
                anim.SetFloat("AngleY", dir.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" && other.transform == MyTarget)
        {
            EnemyHealthManager e = other.GetComponent<EnemyHealthManager>();
            if (targetIndex <= 5)
            {
                if (Time.time > nextHitAllowed + attackDelay)
                {
                    nextHitAllowed = Time.time + attackDelay;
                    e.HurtEnemy(damage,true);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
