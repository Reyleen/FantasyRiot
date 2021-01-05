using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    private Transform player;
    private Vector2 target;

    public int damageToGive;
    public bool hit;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
        Vector2 dir = player.position - transform.position;

        hit = false;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.GetComponent<Rigidbody2D>().velocity = dir * 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !hit)
        {
            other.gameObject.GetComponent<PlayerStatus>().HurtPlayer(damageToGive);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            this.transform.parent = other.transform;
            hit = true;
            Destroy(gameObject, 2.0f);
        }

        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Weapon" || other.gameObject.tag == "Coin" || other.gameObject.tag == "NPC" || other.gameObject.tag == "Tower" || other.gameObject.tag == "RangeTower")
        {

        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Destroy(gameObject, 2.0f);
        }


    }
}
