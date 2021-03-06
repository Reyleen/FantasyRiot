﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour
{
    private string dialogue;
    private DialogueManager dMan;
    public bool click = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = "MEOW!!!\n \nHi human, welcome in the tutorial. You know how to interact and move!!\nI- I hear strange noises in the cave, I feel there is something creepy in there..";
    }

    // Update is called once per frame
    void Update()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && click)
        {
            dMan.ShowBox(dialogue);
            click = false;
        }
    }

    public void Talk()
    {
        click = true;
    }
}

