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
    public GoldManager gold;

    void Start()
    {
        gold = FindObjectOfType<GoldManager>();
    }

    public void Update()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
    } 
    public void PurchaseInfernalTower()
    {
        if (gold.currentGold >= 10)
        {
            Instantiate(InfernalTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
        }
        
    }

    public void PurchaseGolemTower()
    {
        if (gold.currentGold >= 10)
        {
            Instantiate(GolemTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
        }
        
    }

    public void PurchaseIceTower()
    {
        if(gold.currentGold >=10)
        {
            Instantiate(IceTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
        }
        
    }

    public void PurchaseAirTower()
    {
        if (gold.currentGold >= 10)
        {
            Instantiate(AirTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
        }
    }

}
