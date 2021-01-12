using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    public Player p;
    void Awake()
    {
        p.SetFirstPlayer();
    }
}
