using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackAnimation : MonoBehaviour
{
    private Animator anim;
    
    [SerializeField]
    private Golem status;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("TowerAttacking", status.Attacking);
    }
}
