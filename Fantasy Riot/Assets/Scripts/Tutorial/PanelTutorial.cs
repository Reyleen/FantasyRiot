using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelTutorial : Tutorial
{
    public bool clicked;
    public GameObject panel;
    public GameObject go;
    public GameObject tower;
    bool open = true;
    public override void CheckIfHappening()
    {
        if (open)
        {
            panel.SetActive(true);
            open = false;
        }
        if (clicked)
        {
            TutorialManager.Instance.CompleteTutorial();
        }
    }
    public void Click()
    {
        tower.SetActive(true);
        panel.SetActive(false);
        go.SetActive(true);
        clicked = true;
    }
}
