using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetter : MonoBehaviour
{
    public Player p;
    public GemsManager gemm;
    void Awake()
    {
        gemm.curGems = 0;
        p.SetFirstPlayer();
    }
}
