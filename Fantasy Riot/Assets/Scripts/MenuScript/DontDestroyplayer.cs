using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyplayer : MonoBehaviour
{
    private static DontDestroyplayer playerInstance;
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (playerInstance == null)
        {
            playerInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
}
