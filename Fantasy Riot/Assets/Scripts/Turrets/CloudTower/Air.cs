using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Air : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;
    public int InitialDamage;
    private bool attacking;
    private bool justAttacked;
    private bool stunned;
    public float StunDur;
    private float MoreStun;
    public GameObject lightning;
    private WaveSpawner s;

    [SerializeField]
    private float attackDelay;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private TowerHealth hp;

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
        s = FindObjectOfType<WaveSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(s.spawningEnemies==true)
        {
            Dissapear();
        }

        if(s.spawningEnemies==false)
        {
            Appear();
        }

        FindTarget();
        Attack();
        Remove(targets);
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
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled; 
    }

    private void Attack()
    {
        if (!canAttack)
        {
            if((timer > StunDur + MoreStun) && stunned)
            {
                foreach (Debuffs enemy in targets)
                {
                    enemy.NotStun();
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
                }

                if (timer >= attackDelay)
                {
                    Instantiate(lightning, new Vector3(transform.position.x, transform.position.y - 0.2f), Quaternion.identity);
                    if (hp.CurrentTowerHp <= 35 && hp.CurrentTowerHp > 22)
                    {
                        foreach (Debuffs enemy in targets)
                        {
                            canAttack = false;
                            justAttacked = true;
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage, false);
                            enemy.Stun();
                            stunned = true;
                        }
                    }

                    if(hp.CurrentTowerHp <= 22 && hp.CurrentTowerHp >= 10)
                    {
                        foreach (Debuffs enemy in targets)
                        {
                            canAttack = false;
                            justAttacked = true;
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 4, false);
                            MoreStun = 1f;
                            enemy.Stun();
                            stunned = true;
                        }
                    }
                    
                    if(hp.CurrentTowerHp < 10 && hp.CurrentTowerHp >= 5)
                    {
                        foreach (Debuffs enemy in targets)
                        {
                            canAttack = false;
                            justAttacked = true;
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 6, false);
                            MoreStun = 2f;
                            enemy.gameObject.GetComponent<Debuffs>().Stun();
                            stunned = true;
                        }
                    }

                    if (hp.CurrentTowerHp < 5 && hp.CurrentTowerHp >= 0)
                    {
                        foreach (Debuffs enemy in targets)
                        {
                            canAttack = false;
                            justAttacked = true;
                            enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(InitialDamage + 8, false);
                            MoreStun = 3f;
                            enemy.gameObject.GetComponent<Debuffs>().Stun();
                            stunned = true;
                        }
                    }
                }
            }
        }
    }
    public void Remove(List<Debuffs> targets)
    {
        for (int i=0; i<=targets.Count; i++ )
        {
            EnemyHealthManager e = targets[i].GetComponent<EnemyHealthManager>();
            if (e.CurrentHealth <= 0)
            {
                targets.Remove(e.GetComponent<Debuffs>());
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

    public void Dying()
    {
        if (targets.Count > 0)
        {
            foreach (Debuffs enemy in targets)
            {
                enemy.gameObject.GetComponent<Debuffs>().NotStun();
            }
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
