using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTowerHit : MonoBehaviour
{
    public int damageToGive;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Tower" || other.gameObject.tag == "RangeTower")
        {

        }
    }
}
