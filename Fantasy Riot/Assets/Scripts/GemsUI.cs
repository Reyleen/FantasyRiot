using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsUI : MonoBehaviour
{
    public GemsManager gemsMan;
    public Text gems;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        gems.text = "" + gemsMan.curGems;
    }
}
