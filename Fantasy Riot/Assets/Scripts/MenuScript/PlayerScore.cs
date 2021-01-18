using System;
using UnityEngine;
//struct for saving score data in the db
[Serializable]
public struct PlayerScore
{
    public string Username;
    public int UserScore;
}