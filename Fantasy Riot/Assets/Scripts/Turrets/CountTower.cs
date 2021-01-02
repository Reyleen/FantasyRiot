using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountTower : MonoBehaviour
{
    public int Tot = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Count(int num)
    {
        Tot = Tot + num;
    }
}
