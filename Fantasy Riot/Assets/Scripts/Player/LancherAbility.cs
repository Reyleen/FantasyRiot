using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancherAbility : MonoBehaviour
{
    private List<Enemy> targets = new List<Enemy>();
    private Enemy target;
    public float time;
    public float timer;
    public int damage;
    public bool ability;
    public CircleCollider2D coll;

    public Animator topAnim;
    public Animator botAnim;

    public float delay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        Remove(targets);
        Attack();
    }
           
    public void Attack()
    {
        if (ability)
        {
            timer += Time.deltaTime;
            topAnim.SetBool("Ability", ability);
            botAnim.SetBool("Ability", ability);

            if (timer <= time)
            {
                if (target != null && timer > delay)
                {
                    foreach (Enemy enemy in targets)
                    {
                        enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                        StartCoroutine(enemy.KnockUp(0.1f, -10, enemy.transform.position));
                    }
                    delay = timer + delay - 0.5f;
                }
            }
            else
            {
                delay = 1;
                ability = false;
                //coll.enabled = false;
                topAnim.SetBool("Ability", ability);
                botAnim.SetBool("Ability", ability);
            }
        }
    }

    public void Ability()
    {
        timer = 0;
        ability = true;
        coll.enabled = true;
    }

    public void FindTarget()
    {
        if (targets.Count > 0)
        {
            foreach (Enemy enemy in targets)
            {
                target = enemy;
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

    public void Remove(List<Enemy> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            EnemyHealthManager e = targets[i].GetComponent<EnemyHealthManager>();
            if (e.CurrentHealth == 0)
            {
                targets.Remove(e.GetComponent<Enemy>());
            }
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
