using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GemsUI : MonoBehaviour
{
    public GemsManager gemsMan;
    public TMP_Text gems;

    // Start is called before the first frame update
    void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        gems.text = gemsMan.GetAndSetGems().ToString();
    }
}
