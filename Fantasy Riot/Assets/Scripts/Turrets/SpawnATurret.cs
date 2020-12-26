using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnATurret : MonoBehaviour
{
    private Transform PlayerPosition;
    public GameObject InfernalTower;
    public GameObject GolemTower;
    public GameObject IceTower;
    public GameObject AirTower;
    private GoldManager gold;
    void Start()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
        gold = FindObjectOfType<GoldManager>();
    }

    public void Update()
    {
        
    } 
    public void PurchaseInfernalTower()
    {
        if (gold.currentGold >= 3)
        {
            Instantiate(InfernalTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-3);
        }
        
    }

    public void PurchaseGolemTower()
    {
        if (gold.currentGold >= 2)
        {
            Instantiate(GolemTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-2);
        }
        
    }

    public void PurchaseIceTower()
    {
        if(gold.currentGold >=5)
        {
            Instantiate(IceTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-5);
        }
        
    }

    public void PurchaseAirTower()
    {
        if (gold.currentGold >= 3)
        {
            Instantiate(AirTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-3);
        }
    }

}
