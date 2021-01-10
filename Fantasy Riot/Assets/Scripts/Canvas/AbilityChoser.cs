using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityChoser : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AbilityActivation()
    {
        if(player.name == "arcPlayer(Clone)")
        {
            ArcAbility a = player.GetComponent<ArcAbility>();
            a.Ability();
        }
        if(player.name == "FighterPlayer(Clone)")
        {
            WarriorAbiity w = player.GetComponent<WarriorAbiity>();
            w.Ability();
        }
        if(player.name == "LancPlayer(Clone)")
        {
            LancherAbility l = player.GetComponent<LancherAbility>();
            l.Ability();
        }
        if(player.name == "witchPlayer(Clone)")
        {
            WitchAbility wi = player.GetComponent<WitchAbility>();
            wi.Ability();
        }
    }
}
