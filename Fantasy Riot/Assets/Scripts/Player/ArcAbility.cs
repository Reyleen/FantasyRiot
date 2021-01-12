using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcAbility : MonoBehaviour
{
    public float timer;
    public float time;
    public bool ability;
    public GameObject circle;

    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
        Remove(targets);

        timer += Time.deltaTime;
        if (ability)
        {
            if(timer <= time)
            {
                foreach (Debuffs enemy in targets)
                {
                    StartCoroutine(enemy.KnockUp(0.03f, -0.1f, enemy.transform.position));
                }
            } else
            {
                ability = false;
                circle.SetActive(false);
            }
        }
    }

    public void Ability()
    {
        circle.SetActive(true);
        ability = true;
        timer = 0;
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
