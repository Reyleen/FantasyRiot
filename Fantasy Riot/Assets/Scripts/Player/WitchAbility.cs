﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WitchAbility : MonoBehaviour
{
    public GameObject lightning;

    public float cooldown;
    public Image fill;
    public bool ability;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(ability)
        {
            fill.fillAmount += 1 / cooldown * Time.deltaTime;

            if(fill.fillAmount >= 1)
            {
                fill.fillAmount = 0;
                ability = false;
            }
        }
    }

    public void Ability()
    {
        if (timer > cooldown)
        {
            Instantiate(lightning, transform.position, Quaternion.identity);
            ability = true;
            timer = 0;
        }
    }
}
