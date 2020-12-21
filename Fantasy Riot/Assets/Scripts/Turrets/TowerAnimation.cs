using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAnimation : MonoBehaviour
{
    [SerializeField]
    private TowerReturn status;
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
            anim.Play("InfernalTowerIdle");
        }

        else
        {
            anim.Play("FileInfernalTower");
        }
    }
}
