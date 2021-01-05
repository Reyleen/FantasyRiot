using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Enemy> targets = new List<Enemy>();
    private Enemy target;
    public int InitialDamage;
    private bool attacking;
    private bool justAttacked;
    private bool stunned;
    public float StunDur;
    private float MoreStun;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private TowerHealth hp;

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
            if((timer > StunDur + MoreStun) && stunned)
            {
                foreach (Enemy enemy in targets)
                {
                    enemy.gameObject.GetComponent<Enemy>().NotStun();
                }

                stunned = false;
                MoreStun = 0;
            }

            if (justAttacked && timer > attackDelay + 0.2f)
            {
                justAttacked = false;
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

                if (timer >= attackDelay)
                {
                    if(hp.CurrentTowerHp == 50)
                    {
                        foreach (Enemy enemy in targets)
                        {
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage);
                            Debug.Log("Damaging");
                            enemy.gameObject.GetComponent<Enemy>().Stun();
                            Debug.Log("Stunned");
                            stunned = true;
                            canAttack = false;
                            justAttacked = true;
                        }
                    }

                    if(hp.CurrentTowerHp <= 35 && hp.CurrentTowerHp >= 25)
                    {
                        foreach (Enemy enemy in targets)
                        {
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 4);
                            Debug.Log("MORE Damage");
                            MoreStun = 1f;
                            enemy.gameObject.GetComponent<Enemy>().Stun();
                            Debug.Log("Stunned");
                            stunned = true;
                            canAttack = false;
                            justAttacked = true;
                        }
                    }
                    
                    if(hp.CurrentTowerHp < 25 && hp.CurrentTowerHp >= 15)
                    {
                        foreach (Enemy enemy in targets)
                        {
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 6);
                            Debug.Log("MORE MORE Damage");
                            MoreStun = 2f;
                            enemy.gameObject.GetComponent<Enemy>().Stun();
                            Debug.Log("Stunned");
                            stunned = true;
                            canAttack = false;
                            justAttacked = true;
                        }
                    }

                    if (hp.CurrentTowerHp < 15 && hp.CurrentTowerHp >= 0)
                    {
                        foreach (Enemy enemy in targets)
                        {
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 8);
                            Debug.Log("MORE MORE MORE Damage");
                            MoreStun = 3f;
                            enemy.gameObject.GetComponent<Enemy>().Stun();
                            Debug.Log("Stunned");
                            stunned = true;
                            canAttack = false;
                            justAttacked = true;
                        }
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
