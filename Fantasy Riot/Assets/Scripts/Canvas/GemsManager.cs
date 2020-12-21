using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsManager : MonoBehaviour
{
    public Text gemsText;
    public int curGems;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("CurrentGems"))
        {
            curGems = PlayerPrefs.GetInt("CurrentGems");
        } else
        {
            curGems = 0;
            PlayerPrefs.SetInt("CurrentGems", 0);
        }

        gemsText.text = "" + curGems;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGems(int gemsToAdd)
    {
        curGems += gemsToAdd;
        PlayerPrefs.SetInt("CurrentGems", curGems);
        gemsText.text = "" + curGems;
    }

    public void RemoveGems(int gemsToRemove)
    {
        curGems -= gemsToRemove;
        PlayerPrefs.SetInt("CurrentGems", curGems);
        gemsText.text = "" + curGems;
    }
}
