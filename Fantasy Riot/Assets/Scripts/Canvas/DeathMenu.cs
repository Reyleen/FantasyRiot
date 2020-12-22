﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private bool isShowned = false;
    public Image backgroundImg;
    public GameObject retry, quit;
    private SpawnPoint spwn;
    private float transition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spwn = FindObjectOfType<SpawnPoint>();

        if (!isShowned)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            transition += Time.deltaTime;
            backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
        }
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        isShowned = true;
    }

    public void Retry()
    {
        SceneManager.LoadScene("Tutorial");
        gameObject.SetActive(false);

    }

    public void ToMenu()
    {

    }
}