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
    private DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        CurrentMainTowerHp = TowerHp;
        towerHpBar.maxValue = TowerHp;
    }

    // Update is called once per frame
    void Update()
    {
        deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();
        towerHpBar.value = CurrentMainTowerHp;

        if (CurrentMainTowerHp <= 0)
        {
            deathMenu.ToggleEndMenu();
            Destroy(gameObject);
        }
    }

    public void HurtMainTower(int damageToGive)
    {
        CurrentMainTowerHp -= damageToGive;
    }
}
