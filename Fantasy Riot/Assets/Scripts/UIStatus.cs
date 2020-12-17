using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public Text nameText;
    public Text levelText, hpText, attackText;
    public Text nextLevelGems;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerStatus();
    }

    public void UpdatePlayerStatus()
    {
        nameText.text = playerStatus.playerName;
        hpText.text = "" + playerStatus.currentHp;
        levelText.text = playerStatus.playerLevel.ToString();
        nextLevelGems.text = "" + playerStatus.nextLevelGems[playerStatus.playerLevel + 1];
        attackText.text = "" + playerStatus.attack;
    }
}
