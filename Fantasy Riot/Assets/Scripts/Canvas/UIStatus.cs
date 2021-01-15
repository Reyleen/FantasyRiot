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
        hpText.text = "" + playerStatus.currentHp;
        levelText.text = playerStatus.playerLevel.ToString();
        nextLevelGems.text = "" + playerStatus.nextLevelGems[playerStatus.playerLevel];
        attackText.text = "" + playerStatus.attack;
    }
}
