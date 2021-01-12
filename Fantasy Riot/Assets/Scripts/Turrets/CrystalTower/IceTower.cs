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
    }

    public void TargetFinder()
    {
        if (targets.Count > 0)
        {
            foreach (Debuffs enemy in targets)
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
                    Debug.Log("Attacking");
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
           Debug.Log("No Enemy/ Enemy died");
        }
    }
}
