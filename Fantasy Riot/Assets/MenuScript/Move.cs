using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Animator anim;
    private bool isClicked;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        isClicked = false;
        if (true)
        {
            isClicked = true;
        }
        anim.SetBool("palClick", isClicked);
    }
}

