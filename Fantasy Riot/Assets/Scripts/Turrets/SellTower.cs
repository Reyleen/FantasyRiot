using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTower : MonoBehaviour
{
    private GoldManager gold;

    [SerializeField]
    private TowerHealth hp;

    [SerializeField]
    private TowerReturn tower;

    private CountTower nTower;

    // Start is called before the first frame update
    void Start()
    {
        gold = FindObjectOfType<GoldManager>();
        nTower = FindObjectOfType<CountTower>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSell()
    {
        gameObject.SetActive(true);
    }

    public void SellFire()
    {
        if (hp.CurrentTowerHp == 20)
        {
            gold.AddMoney(+10);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 20 && hp.CurrentTowerHp >= 15)
        {
            gold.AddMoney(+8);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 15 && hp.CurrentTowerHp >= 10)
        {
            gold.AddMoney(+6);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 10 && hp.CurrentTowerHp >= 5)
        {
            gold.AddMoney(+4);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 5 && hp.CurrentTowerHp >= 1)
        {
            gold.AddMoney(+2);
            tower.Sold();
        }

        nTower.Count(-1);
    }

    public void SellGolem()
    {
        if (hp.CurrentTowerHp == 50)
        {
            gold.AddMoney(+10);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 50 && hp.CurrentTowerHp >= 37)
        {
            gold.AddMoney(+8);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 37 && hp.CurrentTowerHp >= 21)
        {
            gold.AddMoney(+6);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 21 && hp.CurrentTowerHp >= 10)
        {
            gold.AddMoney(+4);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 10 && hp.CurrentTowerHp >= 1)
        {
            gold.AddMoney(+2);
            tower.Sold();
        }

        nTower.Count(-1);
    }

    public void SellIce()
    {
        if (hp.CurrentTowerHp == 35)
        {
            gold.AddMoney(+7);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 35 && hp.CurrentTowerHp >= 22)
        {
            gold.AddMoney(+6);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 22 && hp.CurrentTowerHp >= 10)
        {
            gold.AddMoney(+5);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 10 && hp.CurrentTowerHp >= 5)
        {
            gold.AddMoney(+4);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 5 && hp.CurrentTowerHp >= 1)
        {
            gold.AddMoney(+2);
            tower.Sold();
        }

        nTower.Count(-1);
    }

    public void SellAir()
    {
        if (hp.CurrentTowerHp == 35)
        {
            gold.AddMoney(+12);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 35 && hp.CurrentTowerHp >= 22)
        {
            gold.AddMoney(+10);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 22 && hp.CurrentTowerHp >= 10)
        {
            gold.AddMoney(+7);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 10 && hp.CurrentTowerHp >= 5)
        {
            gold.AddMoney(+4);
            tower.Sold();
        }

        if (hp.CurrentTowerHp < 5 && hp.CurrentTowerHp >= 1)
        {
            gold.AddMoney(+2);
            tower.Sold();
        }

        nTower.Count(-1);
    }
}
