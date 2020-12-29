using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyplayer : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
