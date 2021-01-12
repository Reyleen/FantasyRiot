using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Queue<Debuffs> monsters = new Queue<Debuffs>();
    private bool canAttack = true;
    private float timer;
    private Projectile proj;
    private Debuffs target;

    public Debuffs Target
    {
        get { return target; }
    }

    [SerializeField]
    private GameObject ballPre;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private float projectileSpeed;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Attack();
        Debug.Log(target);
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            timer += Time.deltaTime;

            if (timer >= cooldown)
            {
                canAttack = true;
                timer = 0;
            }
        }

        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }

        if (target != null)
        {
            if (canAttack)
            {
                DamageAttack();
                canAttack = false;
            }
        }
    }

    private void DamageAttack()
    {
        GameObject fireBall = Instantiate(ballPre, new Vector3(transform.position.x, transform.position.y + 0.8f), Quaternion.identity);
        Projectile proj = fireBall.GetComponent<Projectile>();
        proj.Initialize(this);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            monsters.Enqueue(other.GetComponent<Debuffs>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
            Debug.Log("No Enemy In Range");
        }
    }
}
