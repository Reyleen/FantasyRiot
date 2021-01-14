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
    public bool HasSpawned = false;
    private TowerReturn status;

    void Start()
    {
        PlayerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
        gold = FindObjectOfType<GoldManager>();
    }

    public void Update()
    {
        status = FindObjectOfType<TowerReturn>();
        if(status!=null)
        if (status.spawned == true)
        {
            Debug.Log("Spawned tower");
            HasSpawned = false;
        }
    } 
    public void PurchaseInfernalTower()
    {
        if (gold.currentGold >= 10 && HasSpawned == false)
        {
            Instantiate(InfernalTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
            HasSpawned = true;
        }
        
    }

    public void PurchaseGolemTower()
    {
        if (gold.currentGold >= 10 && HasSpawned == false)
        {
            Instantiate(GolemTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-10);
            HasSpawned = true;
        }
        
    }

    public void PurchaseIceTower()
    {
        if(gold.currentGold >=7 && HasSpawned == false)
        {
            Instantiate(IceTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-7);
            HasSpawned = true;
        }
        
    }

    public void PurchaseAirTower()
    {
        if (gold.currentGold >= 12 && HasSpawned == false)
        {
            Instantiate(AirTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
            gold.AddMoney(-12);
            HasSpawned = true;
        }
    }
}
