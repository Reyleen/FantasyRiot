using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReset : MonoBehaviour
{
    public EnemyHealthManager[] e;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            e[i].MaxHealth = 50;
        }
        for(int i = 4; i < 8; i++)
        {
            e[i].MaxHealth = 25;
        }
        for (int i = 8; i < 12; i++)
        {
            e[i].MaxHealth = 20;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
