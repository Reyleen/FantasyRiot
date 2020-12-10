using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnATurret : MonoBehaviour
{
    public Transform PlayerPosition;
    public GameObject InfernalTower;
    public GameObject GolemTower;
    public GameObject IceTower;
    public GameObject AirTower;

   public void Update()
    {
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
    } 
    public void PurchaseInfernalTower()
    {
        Instantiate(InfernalTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
    }

    public void PurchaseGolemTower()
    {
        Instantiate(GolemTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
    }

    public void PurchaseIceTower()
    {
        Instantiate(IceTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
    }

    public void PurchaseAirTower()
    {
        Instantiate(AirTower, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation);
    }

}
