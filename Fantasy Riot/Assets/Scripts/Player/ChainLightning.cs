using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public bool hit;

    private List<Transform> targets = new List<Transform>();
    private int targetIndex;

    private Rigidbody2D rb;
    public float speed = 3f;
    public Transform MyTarget { get; set; }
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MyTarget = FindObjectOfType<EnemyHealthManager>().transform;
    }

    void FixedUpdate()
    {
        Vector2 dir = MyTarget.position - transform.position;
        rb.velocity = dir * speed;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if(MyTarget == null)
        {
            Destroy(gameObject);
        }

        float distance = 0;

        if (MyTarget != null)
        {
            distance = Vector2.Distance(transform.position, MyTarget.position);
        }
        if(distance <= 20f)
        {
            if(hit && targetIndex < targets.Count)
            {
                PickTarget(MyTarget.GetComponent<Collider2D>());
            }
            else if(MyTarget != null)
            {
                
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
            } else
            {
                Destroy(gameObject);
            }
            Collider2D[] tmp = Physics2D.OverlapCircleAll(other.transform.position, 10);

            foreach (Collider2D others in tmp)
            {
                if(others.transform != MyTarget && others.transform != transform && others.tag == "Enemy")
                {
                    targets.Add(others.transform);
                }
            }

            speed *= 2;
            PickTarget(other);
            Destroy(gameObject, 2f);
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
