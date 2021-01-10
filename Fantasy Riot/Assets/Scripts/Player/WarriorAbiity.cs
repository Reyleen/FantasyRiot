using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAbiity : MonoBehaviour
{
    public GameObject clone;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability()
    {
        Instantiate(clone, transform.position, Quaternion.identity);
    }
}
