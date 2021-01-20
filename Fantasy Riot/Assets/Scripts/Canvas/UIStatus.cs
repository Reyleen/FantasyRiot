using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIStatus : MonoBehaviour
{
    public PlayerStatus playerStatus;

    public TMP_Text nameText;
    public TMP_Text levelText, hpText, attackText;
    public TMP_Text nextLevelGems;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayerStatus();
    }

    public void UpdatePlayerStatus()
    {
        nameText.text = playerStatus.playerName;
        hpText.text =playerStatus.currentHp.ToString();
        levelText.text = playerStatus.playerLevel.ToString();
        if (playerStatus.playerLevel != 10)
        {
            nextLevelGems.text =playerStatus.nextLevelGems[playerStatus.playerLevel].ToString();
        }
        else
        {
            nextLevelGems.text = "MAX LVL";
        }
        attackText.text =playerStatus.attack.ToString();
    }
}
