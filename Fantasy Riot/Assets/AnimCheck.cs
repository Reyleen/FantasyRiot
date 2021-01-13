using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCheck : MonoBehaviour
{
    public Animator anim1;
    public Animator anim2;
    void FixedUpdate()
    {
        if (anim1.GetCurrentAnimatorStateInfo(0).IsName("spaWalk"))
        {
            anim2.SetBool("StartAnimation", true);
        }
        else
        {
            anim2.SetBool("StartAnimation", false);
        }
    }
}
