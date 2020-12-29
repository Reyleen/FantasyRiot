using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Enemy> targets = new List<Enemy>();
    private Enemy target;
    public int damageAttack;
    private bool attacking;
    private bool justAttacked;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float cooldown;

    public bool Attacking 
    { 
        get { return attacking; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        Attack();
    }

    public void FindTarget()
    {
        if (targets.Count > 0)
        {
            foreach (Enemy enemy in targets)
            {
                target = enemy;
                Debug.Log(target);
            }
        }
    }

    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            if (justAttacked && timer > attackDelay+0.2f)
            {
                foreach (Enemy enemy in targets)
                {
                    StartCoroutine(enemy.KnockUp(0.1f, +10, enemy.transform.position));
                    Debug.Log("down");
                    justAttacked = false;
                }
            }

            if (timer >= cooldown)
            {
                canAttack = true;
                timer = 0;
            }
        }

        if (target != null)
        {
            timer += Time.deltaTime;

            if (canAttack)
            {
                attacking = true;

                if (timer >= 0.1f)
                {
                    attacking = false;
                    Debug.Log("Starting Animation");
                }

                if(timer >= attackDelay)
                {
                    foreach (Enemy enemy in targets)
                    {
                        enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageAttack);
                        Debug.Log("Damaging");
                        StartCoroutine(enemy.KnockUp(0.1f, -10, enemy.transform.position));
                        Debug.Log("knock");
                        canAttack = false;
                        justAttacked = true;
                    }
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Add(other.GetComponent<Enemy>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<Enemy>());
            target = null;
        }
    }
}
