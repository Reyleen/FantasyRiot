using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    [SerializeField]
    private TowerReturn status;
    
    [SerializeField]
    private Animator anim;
    
    void Start()
    {
        status = gameObject.GetComponent<TowerReturn>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.Locked == true)
        {
            anim.SetBool("Locked", true);
        }
    }
}
