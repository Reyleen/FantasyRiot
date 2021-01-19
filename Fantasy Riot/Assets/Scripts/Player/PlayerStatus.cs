using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public string playerName, description;

    public int playerLevel, maxLevel;

    private int currentGems;
    public int[] nextLevelGems;

    public int currentHp, maxHp, attack;
    public GemsManager gemMan;

    private DeathMenu deathMenu;

    public Animator anim;
    public Animator anim1;
    public AuthManager a;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        nextLevelGems = new int[maxLevel + 1];
        nextLevelGems[1] = 100;
        deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();

        for (int i = 2; i< maxLevel; i++)
        {
            nextLevelGems[i] = Mathf.RoundToInt(nextLevelGems[i - 1] * 1.5f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
            //deathMenu = GameObject.Find("Canvas").transform.Find("DeathMenu").GetComponent<DeathMenu>();

            if (currentHp <= 0)
            {
                anim.SetBool("IsDead", true);
                anim1.SetBool("IsDead", true);
                deathMenu.ToggleEndMenu();
                Destroy(gameObject, 0.8f);
            }
    }

    public void HurtPlayer(int damageToGive)
    {
        if (currentHp - damageToGive > maxHp)
        {
            currentHp = maxHp;
        }
        else
        {
            currentHp -= damageToGive;
        }
    }

    public void setMaxHealth()
    {
        currentHp = maxHp;
    }

    public void LevelUp()
    {
        currentGems = gemMan.curGems;
        if (currentGems >= nextLevelGems[playerLevel] && playerLevel < maxLevel)
        {
            gemMan.RemoveGems(nextLevelGems[playerLevel]);
            playerLevel++;

            maxHp = Mathf.RoundToInt(maxHp * 1.2f);
            currentHp = maxHp;
            attack = Mathf.CeilToInt(attack * 1.2f);

            a = GameObject.Find("AuthManager").GetComponent<AuthManager>();
            a.UpdatePlayerOnServer(gemMan.curGems, playerLevel, maxHp, attack, this.gameObject.name);
        }
    }
}
