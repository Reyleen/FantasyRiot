using System;
using UnityEngine;

/* structure of the player data*/
[Serializable]
public struct PlayerData
{//use info
    public string User;
    public int Gemms;
    //Hero info
    public int lvlA;
    public int HPA;
    public int atkA;
    public int lvlF;
    public int HPF;
    public int atkF;
    public int lvlM;
    public int HPM;
    public int atkM;
    public int lvlL;
    public int HPL;
    public int atkL;
    //level unlocked
    public bool L1;
    public bool L2;
    public bool L3;
    public bool L4;
}
