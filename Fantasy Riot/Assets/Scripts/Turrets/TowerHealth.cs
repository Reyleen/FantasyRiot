using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField]
    private int TowerHp;
    public int CurrentTowerHp;
    private CountTower nTower;

    public bool water;
    public bool air;

    [SerializeField]
    private IceTower ice;

    [SerializeField]
    private Air cloud;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTowerHp = TowerHp;
        nTower = FindObjectOfType<CountTower>();
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentTowerHp <= 0)
        {
            if(cloud)
            {
                cloud.Dying();
            }

            if (ice)
            {
                ice.Dying();
            }
                
            Destroy(gameObject);
            nTower.Count(-1);
            
        }
    }

    public void HurtTower(int damageToGive)
    {
        CurrentTowerHp -= damageToGive;
    }
}
