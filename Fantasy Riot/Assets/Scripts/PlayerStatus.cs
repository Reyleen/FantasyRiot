using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public string playerName, description;

    public int playerLevel, maxLevel;

    public int currentGems;
    public int[] nextLevelGems;

    public int currentHp, maxHp, attack;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelGems = new int[maxLevel + 1];
        nextLevelGems[1] = 100;

        for(int i = 2; i< maxLevel; i++)
        {
            nextLevelGems[i] = Mathf.RoundToInt(nextLevelGems[i - 1] * 1.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelUp()
    {
        if(currentGems >= nextLevelGems[playerLevel] && playerLevel < maxLevel)
        {
            currentGems -= nextLevelGems[playerLevel];
            playerLevel++;

            maxHp = Mathf.RoundToInt(maxHp * 1.2f);
            currentHp = maxHp;
            attack = Mathf.CeilToInt(attack * 1.2f);
            
        }
    }
}
