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
    public GameObject aim, walk;

    public DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        playerCurrentHealth = playerMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
            anim1.SetBool("IsDead", true);
            //canvas.SetActive(false);
            aim.SetActive(false);
            walk.SetActive(false);
            deathMenu.ToggleEndMenu ();
            Destroy(gameObject, 5f);

        }
    }

    public void HurtPlayer(int damageToGive)
    {
        playerCurrentHealth -= damageToGive;
    }

    public void setMaxHealth()
    {
        playerCurrentHealth = playerMaxHealth;
    }
}
