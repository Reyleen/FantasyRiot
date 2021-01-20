using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;
    public int damageAttack;
    private bool attacking;
    private bool justAttacked;
    private WaveSpawner s;
    private SpawnATurret arcade;
    private Infinitewaves s1;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    GameObject canvas;

    public bool Attacking 
    { 
        get { return attacking; }
    }

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        arcade = FindObjectOfType<SpawnATurret>();
        if (arcade.arcade == true)
        {
            s1 = FindObjectOfType<Infinitewaves>();
        }
        else
        {

            s = FindObjectOfType<WaveSpawner>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (arcade.arcade == true)
        {
            if (s1.spawningEnemies == true)
            {
                Dissapear();
            }

            else if (s1.spawningEnemies == false)
            {
                Appear();
            }
        }

        else
        {
            if (s.spawningEnemies == true)
            {
                Dissapear();
            }

            else if (s.spawningEnemies == false)
            {
                Appear();
            }
        }
        FindTarget();
        if (targets.Count > 0)
        {
            Attack();
        }
    }

    public void FindTarget()
    {
        if (targets.Count > 0)
        {
            foreach (Debuffs enemy in targets)
            {
                target = enemy;
            }
        }
    }

    public void Select()
    {
        mySpriteRenderer.enabled = false;
    }
    public void Show()
    {
        mySpriteRenderer.enabled = true;
    }

    private void Attack()
    {
        if (!canAttack)
        {
            if (justAttacked && timer > attackDelay+0.2f)
            {
                foreach (Debuffs enemy in targets)
                {
                    StartCoroutine(enemy.KnockUp(0.05f, +5, enemy.transform.position));
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

                if (timer >= 0.05f)
                {
                    attacking = false;
                }

                if(timer >= attackDelay)
                {
                    foreach (Debuffs enemy in targets)
                    {
                        enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageAttack, false);
                        StartCoroutine(enemy.KnockUp(0.1f, -5, enemy.transform.position));
                        canAttack = false;
                        justAttacked = true;
                        Remove(targets);
                    }
                }
            }
        }
    }
    public void Remove(List<Debuffs> targets)
    {
        foreach (Debuffs enemy in targets)
        {
            if (enemy == null)
            {
                targets.Remove(enemy);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Add(other.GetComponent<Debuffs>());
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<Debuffs>());
            target = null;
        }
    }
    private void Dissapear()
    {
        mySpriteRenderer.enabled = false;
        canvas.SetActive(false);
    }

    private void Appear()
    {
        canvas.SetActive(true);
    }
}
