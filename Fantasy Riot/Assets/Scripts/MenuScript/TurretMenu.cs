using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Change the Tourret on screen every time the scene is loaded*/
public class TurretMenu : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1;
        int num = UnityEngine.Random.Range(0, 3);
        anim.SetInteger("num", num);
    }
}
