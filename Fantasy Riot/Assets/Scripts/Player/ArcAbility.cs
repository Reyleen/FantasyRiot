using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArcAbility : MonoBehaviour
{
    public float timer;
    public float time;
    public bool ability;
    public bool ability1;
    public GameObject circle;

    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;

    public float timer1;
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
        FindTarget();
        targets.RemoveAll(targets => targets == null);
        timer1 += Time.deltaTime;
        timer += Time.deltaTime;
        if (ability1)
        {
            fill.fillAmount += 1 / cooldown * Time.deltaTime;

            if (fill.fillAmount >= 1)
            {
                fill.fillAmount = 0;
                ability1 = false;
            }
        }
        if (ability)
        {
            if(timer <= time)
            {
                foreach (Debuffs enemy in targets)
                {
                    enemy.DebuffSlow();
                }
            } else
            {
                ability = false;
                circle.SetActive(false);
                foreach(Debuffs enemy in targets)
                {
                    enemy.RemoveBuff();
                }
            }
        }
    }

    public void Ability()
    {
        if (timer > cooldown)
        {
            circle.SetActive(true);
            ability = true;
            ability1 = true;
            timer1 = 0;
            timer = 0;
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

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Remove(other.GetComponent<Debuffs>());
            target = null;
        }
    }
}
