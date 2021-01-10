using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchAbility : MonoBehaviour
{
    public GameObject lightning;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spell()
    {
        Instantiate(lightning, transform.position, Quaternion.identity);
    }
}
