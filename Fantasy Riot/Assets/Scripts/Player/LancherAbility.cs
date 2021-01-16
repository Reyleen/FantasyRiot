using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LancherAbility : MonoBehaviour
{
    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;
    public float time;
    public float timer;
    public int damage;
    public bool ability;
    public bool ability1;
    public CircleCollider2D coll;
    public float timer1;
    public Animator topAnim;
    public Animator botAnim;

    public float delay;

    public float cooldown;
    public Image fill;
    // Start is called before the first frame update
    void Start()
    {
        fill = GameObject.Find("Canvas").transform.Find("UHD").transform.Find("AbilityButton").transform.Find("Cd").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer1 += Time.deltaTime;
        timer += Time.deltaTime;
        FindTarget();
        Remove(targets);
        Attack();
        if (ability1)
        {
            fill.fillAmount += 1 / cooldown * Time.deltaTime;

            if (fill.fillAmount >= 1)
            {
                fill.fillAmount = 0;
                ability1 = false;
            }
        }

    }
           
    public void Attack()
    {
        if (ability)
        {
            topAnim.SetBool("Ability", ability);
            botAnim.SetBool("Ability", ability);

            if (timer <= time)
            {
                if (target != null && timer > delay)
                {
                    delay = timer + delay + 0.5f;
                    foreach (Debuffs enemy in targets)
                    {
                        enemy.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                        StartCoroutine(enemy.KnockUp(0.1f, -0.1f, enemy.transform.position));
                    }
                }
            }
            else
            {
                delay = 1;
                ability = false;
                coll.enabled = false;
                topAnim.SetBool("Ability", ability);
                botAnim.SetBool("Ability", ability);
            }
        }
    }

    public void Ability()
    {
        if (timer1 > cooldown)
        {
            timer1 = 0;
            timer = 0;
            ability = true;
            ability1 = true;
            coll.enabled = true;
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
