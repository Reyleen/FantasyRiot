using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerPanel : MonoBehaviour
{
    private bool isShowned = false;
    public Image backgroundImg;
    private float transition = 0.0f;
    private string FIRST_TUTORIAL = "firstTime";
    private bool firstTime = true;
    public GemsManager gemsMan;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowned)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void ToggleWinPan()
    {
        gameObject.SetActive(true);
        isShowned = true;
    }

    public void AddGems()
    {
        if(firstTime)
        {
            gemsMan.AddGems(100);
            this.firstTime = false;
            PlayerPrefs.SetInt(FIRST_TUTORIAL, this.firstTime ? 1 : 0);
        }
    }
}
