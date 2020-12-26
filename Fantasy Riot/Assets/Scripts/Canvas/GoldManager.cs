using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public Text moneyText;
    public int currentGold { get; set; }
    private PlayerHealthManager plaHea;

    // Start is called before the first frame update
    void Start()
    {
        /*if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            currentGold = PlayerPrefs.GetInt("CurrentMoney");
        } else*/
        //{
            currentGold = 40;
        plaHea = FindObjectOfType<PlayerHealthManager>();
        //}
        moneyText.text = "Gold: " + currentGold;
    }

    // Update is called once per frame
    void Update()
    {
        

        if(plaHea.playerCurrentHealth <= 0)
        {
            currentGold = 0;
            PlayerPrefs.SetInt("CurrentMoney", 0);
            moneyText.text = "Gold: " + currentGold;
        }
    }

    public void AddMoney(int goldToAdd)
    {
        currentGold += goldToAdd;
        PlayerPrefs.SetInt("CurrentMoney", currentGold);
        moneyText.text = "Gold: " + currentGold;
    }
}
