using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    [SerializeField]
    private int TowerHp;
    public int CurrentTowerHp;
    private CountTower nTower;

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
            Destroy(gameObject);
            nTower.Count(-1);
        }
    }

    public void HurtTower(int damageToGive)
    {
        CurrentTowerHp -= damageToGive;
    }
}
