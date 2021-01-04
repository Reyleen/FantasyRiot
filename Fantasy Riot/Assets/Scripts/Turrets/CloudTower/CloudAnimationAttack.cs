using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimationAttack : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private Air status;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("CloudAttack", status.Attacking);
    }
}
