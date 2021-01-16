using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemsManager : MonoBehaviour
{
    public int curGems;

    // Start is called before the first frame update
    public int GetAndSetGems()
    {
        Debug.Log("GEMMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        if(PlayerPrefs.HasKey("CurrentGems"))
        {
            return curGems = PlayerPrefs.GetInt("CurrentGems");
        } else
        {
            PlayerPrefs.SetInt("CurrentGems", 0);
            return curGems = 0;
        }
    }
    public void SetGems()
    {
        Debug.Log("GEMMEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
        if (PlayerPrefs.HasKey("CurrentGems"))
        {
            curGems = PlayerPrefs.GetInt("CurrentGems");
        }
        else
        {
            curGems = 0;
            PlayerPrefs.SetInt("CurrentGems", 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddGems(int gemsToAdd)
    {
        curGems += gemsToAdd;
        PlayerPrefs.SetInt("CurrentGems", curGems);
    }

    public void RemoveGems(int gemsToRemove)
    {
        curGems -= gemsToRemove;
        PlayerPrefs.SetInt("CurrentGems", curGems);
    }
}
