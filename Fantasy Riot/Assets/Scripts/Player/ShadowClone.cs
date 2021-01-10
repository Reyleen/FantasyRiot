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
    public bool hit;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MyTarget = FindObjectOfType<EnemyHealthManager>().transform;
        Destroy(gameObject, 15f);
    }

    void FixedUpdate()
    {
        Vector2 dir = MyTarget.position - transform.position;
        //rb.velocity = dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (MyTarget == null)
        {
            Destroy(gameObject);
        }

        if (MyTarget != null)
        {
            distance = Vector2.Distance(transform.position, MyTarget.position);
            transform.position = Vector2.MoveTowards(transform.position, MyTarget.position, speed * Time.deltaTime);
            Vector2 dir = MyTarget.position - transform.position;

            //anim.SetFloat("AngleX", dir.x);
            //anim.SetFloat("AngleY", dir.y);
            if (distance <= 2f)
            {
                if (hit && targetIndex < targets.Count)
                {
                    PickTarget(MyTarget.GetComponent<Collider2D>());
                }
                else
                {

                }
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hit && other.gameObject.tag == "Enemy" && other.transform == MyTarget)
        {
            //hit = true;
            if (targetIndex <= 5)
            {
                EnemyHealthManager e = other.GetComponent<EnemyHealthManager>();
                e.HurtEnemy(damage);
            }
            else
            {
                Destroy(gameObject);
            }
            Collider2D[] tmp = Physics2D.OverlapCircleAll(other.transform.position, 10);

            foreach (Collider2D others in tmp)
            {
                if (others.transform != MyTarget && others.transform != transform && others.tag == "Enemy")
                {
                    targets.Add(others.transform);
                }
            }
            PickTarget(other);
            
        }
    }

    private void PickTarget(Collider2D other)
    {
        EnemyHealthManager e = other.GetComponent<EnemyHealthManager>();
        e.HurtEnemy(damage);
        MyTarget = targets[targetIndex];
        targetIndex++;
    }
}
