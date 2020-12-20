using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsManager : MonoBehaviour
{
    public Text gemsText;
    private int currentGems;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("CurrentGems"))
        {
            currentGems = PlayerPrefs.GetInt("CurrentGems");
        } else
        {
            currentGems = 0;
            PlayerPrefs.SetInt("CurrentGems", 0);
        }

        gemsText.text = "" + currentGems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGems(int gemsToAdd)
    {
        currentGems += gemsToAdd;
        PlayerPrefs.SetInt("CurrentGems", currentGems);
        gemsText.text = "" + currentGems;

    }
}
