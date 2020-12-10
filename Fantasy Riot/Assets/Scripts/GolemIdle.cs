using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIdle : MonoBehaviour
{
    [SerializeField]
    private TowerReturn status;
    private Animator anim;
   void Start()
    {
        status = GameObject.FindObjectOfType<TowerReturn>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status.Locked == true)
        {
            anim.Play("GolemTowerIdle");
        }

        else
        {
            anim.Play("FileGolemTower");
        }
    }
}
