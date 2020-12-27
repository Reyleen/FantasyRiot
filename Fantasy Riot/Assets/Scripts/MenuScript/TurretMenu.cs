using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMenu : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        int num = UnityEngine.Random.Range(0, 3);
        anim.SetInteger("num", num);
    }
}
