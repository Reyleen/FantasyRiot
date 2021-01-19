using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;
    private Queue<Debuffs> monsters = new Queue<Debuffs>();
    private bool canAttack = true;
    private float timer;
    private Projectile proj;
    private Debuffs target;
    private WaveSpawner s;
    private SpawnATurret arcade;
    private Infinitewaves s1;

    public Debuffs Target
    {
        get { return target; }
    }

    [SerializeField]
    private GameObject ballPre;

    [SerializeField]
    private float cooldown;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    GameObject canvas;

    public float ProjectileSpeed
    {
        get { return projectileSpeed; }
    }
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
        Attack();
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

        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }

        if (target != null)
        {
            if (canAttack)
            {
                DamageAttack();
                canAttack = false;
            }
        }
    }

    private void DamageAttack()
    {
        GameObject fireBall = Instantiate(ballPre, new Vector3(transform.position.x, transform.position.y + 0.8f), Quaternion.identity);
        Projectile proj = fireBall.GetComponent<Projectile>();
        proj.Initialize(this);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            monsters.Enqueue(other.GetComponent<Debuffs>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
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
