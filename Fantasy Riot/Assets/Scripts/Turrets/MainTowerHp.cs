using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainTowerHp : MonoBehaviour
{
    public int TowerHp;
    public int CurrentMainTowerHp;
    public Slider towerHpBar;
    public DeathMenu deathMenu;
    public AudioSource destroy;
    public GameObject tow;
    // Start is called before the first frame update
    void Awake()
    {
        tow = GameObject.Find("Canvas/UHD/TowerWarningtext");
        tow.SetActive(false);
    }
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
        StartCoroutine(TextWarn());
    }
    private IEnumerator TextWarn()
    {
        tow.SetActive(true);
        yield return new WaitForSeconds(2);
        tow.SetActive(false);
    }
}
