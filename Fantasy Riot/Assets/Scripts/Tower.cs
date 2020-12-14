using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /*[SerializeField]
    private string projectileType; */
    private SpriteRenderer mySpriteRenderer;
    private Enemy target;
    private Queue<Enemy> monsters = new Queue<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       Attack();
       Debug.Log(target);
    }
    public void Select()
    {
        mySpriteRenderer.enabled = !mySpriteRenderer.enabled;
    }

    private void Attack()
    {
        if (target == null && monsters.Count > 0)
        {
            target = monsters.Dequeue();
        }
    }

   /* private void Shoot()
    {
        Projectile projectile = GameManager.
    }*/

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            monsters.Enqueue(other.GetComponent<Enemy>());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            target = null;
            Debug.Log("No Enemy In Range");
        }
    }
}
