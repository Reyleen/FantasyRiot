using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerHealthManager : MonoBehaviour
{
    public int playerMaxHealth;
    public int playerCurrentHealth;
    public Animator anim;
    public Animator anim1;
    private DeathMenu deathMenu;
    public PlayerStatus plaSta;

    // Start is called before the first frame update
    void Start()
    {
        deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();
        playerMaxHealth = plaSta.maxHp;
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();

        if (playerCurrentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
            anim1.SetBool("IsDead", true);
            deathMenu.ToggleEndMenu ();
            Destroy(gameObject, 0.8f);
        }
    }

    public void HurtPlayer(int damageToGive)
    {
        if (plaSta.currentHp - damageToGive > playerMaxHealth)
        {
            plaSta.currentHp = playerMaxHealth;
        }
        else
        {
            plaSta.currentHp -= damageToGive;
        }
    }

    public void setMaxHealth()
    {
        plaSta.currentHp = plaSta.maxHp;
    }
}
