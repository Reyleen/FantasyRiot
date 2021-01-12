using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancherAbility : MonoBehaviour
{
    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;
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
                    foreach (Debuffs enemy in targets)
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
            foreach (Debuffs enemy in targets)
            {
                target = enemy;
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

    public void Remove(List<Debuffs> targets)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            EnemyHealthManager e = targets[i].GetComponent<EnemyHealthManager>();
            if (e.CurrentHealth == 0)
            {
                targets.Remove(e.GetComponent<Debuffs>());
            }
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
}
