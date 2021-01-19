using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Debuffs> targets = new List<Debuffs>();
    private Debuffs target;
    public int damageField;
    private WaveSpawner s;
    private SpawnATurret arcade;
    private Infinitewaves s1;

    [SerializeField]
    private float cooldown;
    
    [SerializeField]
    private TowerHealth hp;
    
    [SerializeField]
    GameObject canvas;

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

    void Update()
    {
        if (arcade.arcade == true)
        {
            if (s.spawningEnemies == true)
            {
                Dissapear();
            }

            if (s.spawningEnemies == false)
            {
                Appear();
            }
        }

        else
        {
            if (s1.spawningEnemies == true)
            {
                Dissapear();
            }

            if (s1.spawningEnemies == false)
            {
                Appear();
            }
        }
        TargetFinder();
        Remove(targets);
    }

    public void TargetFinder()
    {
        if (targets.Count > 0)
        {
            foreach (Debuffs enemy in targets)
            {
                target = enemy;
                Attack();
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
                timer += Time.deltaTime;

                if (timer >= cooldown)
                {
                    canAttack = true;
                    timer = 0;
                }
            }

            if (target != null)
            {
                if (canAttack)
                {
                    target.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageField,false);
                    canAttack = false;
                }
            }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
           targets.Add(other.GetComponent<Debuffs>());
           other.gameObject.GetComponent<Debuffs>().DebuffSlow();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
           other.gameObject.GetComponent<Debuffs>().RemoveBuff();
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
                enemy.gameObject.GetComponent<Debuffs>().RemoveBuff();
            }
        }
    }
    public void Remove(List<Debuffs> targets)
    {
        for (int i = 0; i <= targets.Count; i++)
        {
            EnemyHealthManager e = targets[i].GetComponent<EnemyHealthManager>();
            if (e.CurrentHealth <= 0)
            {
                targets.Remove(e.GetComponent<Debuffs>());
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
