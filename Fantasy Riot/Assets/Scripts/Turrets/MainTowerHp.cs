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
    public DeathMenu deathMenu;
    public AudioSource destroy;

    // Start is called before the first frame update
    void Start()
    {
        CurrentMainTowerHp = TowerHp;
        towerHpBar.maxValue = TowerHp;
        deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();
    }

    // Update is called once per frame
    void Update()
    {
       
        towerHpBar.value = CurrentMainTowerHp;

        if (CurrentMainTowerHp <= 0)
        {
            destroy.Play();
            deathMenu.ToggleEndMenu();
            Destroy(gameObject);
        }
    }

    public void HurtMainTower(int damageToGive)
    {
        CurrentMainTowerHp -= damageToGive;
    }
}
