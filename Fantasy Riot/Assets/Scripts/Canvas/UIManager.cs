using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthBar;
    public TMP_Text HPText;
    private PlayerStatus playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<PlayerStatus>();
        healthBar.maxValue = playerHealth.maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerHealth.currentHp;
        HPText.text = "HP: " + playerHealth.currentHp + "/" + playerHealth.maxHp;        
    }

}
