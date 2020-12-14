using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    /*[SerializeField]
    private string projectileType; */
    private SpriteRenderer mySpriteRenderer;
    private Monster target;
    private Queue<Monster> monsters = new Queue<Monster>();

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
            monsters.Enqueue(other.GetComponent<Monster>());
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
