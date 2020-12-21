using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZZZ : MonoBehaviour
{
    public GemsManager gemsMan;
    public int value;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GiveGems()
    {
        gemsMan.AddGems(value);
    }
}
