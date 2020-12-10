using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageToGive;
    public float hitDelay;
    private float nextHitAllowed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            if (Time.time > nextHitAllowed + hitDelay)
            {
                nextHitAllowed = Time.time + hitDelay;
                other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);

            }
        }
    }

}
