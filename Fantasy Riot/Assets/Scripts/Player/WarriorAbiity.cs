﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarriorAbiity : MonoBehaviour
{
    public GameObject clone;
    public GameObject pla;
    private Transform PlayerPosition;

    public float cooldown;
    public Image fill;
    public bool ability;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        pla = GameObject.FindGameObjectWithTag("Player");
        PlayerPosition = pla.GetComponent<Transform>();
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
        fill = GameObject.Find("Canvas").transform.Find("UHD").transform.Find("AbilityButton").transform.Find("Cd").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (ability)
        {
            fill.fillAmount += 1 / cooldown * Time.deltaTime;

            if (fill.fillAmount >= 1)
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
            Instantiate(clone, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            ability = true;
            timer = 0;
        }
        
    }
}
