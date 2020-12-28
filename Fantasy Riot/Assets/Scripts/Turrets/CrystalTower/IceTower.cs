using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private bool canAttack = true;
    private float timer;
    private List<Enemy> targets = new List<Enemy>();
    private Enemy target;
    public int damageField;

    [SerializeField]
    private float cooldown;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        TargetFinder();
        Attack();
    }

    public void TargetFinder()
    {
        if (targets.Count > 0)
        {
            foreach (Enemy enemy in targets)
            {
                target = enemy;
                Debug.Log(target);
                Attack();
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
                    target.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageField);
                    Debug.Log("Attacking?");
                    canAttack = false;
                     
                }
            }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
           targets.Add(other.GetComponent<Enemy>());
           other.gameObject.GetComponent<Enemy>().DebuffSlow();
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
           other.gameObject.GetComponent<Enemy>().RemoveBuff();
           targets.Remove(other.GetComponent<Enemy>());
           target = null;
           Debug.Log("No Enemy/ Enemy died");
        }
    }
}
