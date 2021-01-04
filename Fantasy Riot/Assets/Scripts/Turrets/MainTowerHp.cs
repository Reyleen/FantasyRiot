using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MainTowerHp : MonoBehaviour
{
    public int TowerHp;
    public int CurrentMainTowerHp;
    public Slider towerHpBar;

    // Start is called before the first frame update
    void Start()
    {
        CurrentMainTowerHp = TowerHp;
        towerHpBar.maxValue = TowerHp;
    }

    // Update is called once per frame
    void Update()
    {
        towerHpBar.value = CurrentMainTowerHp;

        if (CurrentMainTowerHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtMainTower(int damageToGive)
    {
        CurrentMainTowerHp -= damageToGive;
    }
}
