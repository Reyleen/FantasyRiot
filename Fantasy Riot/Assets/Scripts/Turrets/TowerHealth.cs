using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField]
    private int TowerHp;
    public int CurrentTowerHp;

    // Start is called before the first frame update
    void Start()
    {
        CurrentTowerHp = TowerHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTowerHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void HurtTower(int damageToGive)
    {
        CurrentTowerHp -= damageToGive;
    }
}
