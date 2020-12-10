using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string name;
    public string password;

    public PlayerData(Player player)
    {
        name = player.name;
        password = player.password;
    }

}
