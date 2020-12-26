using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : Tutorial
{
    private bool isCurrentTutorial = false;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!isCurrentTutorial) {
            return; }
        if (other.gameObject.tag == "Player")
        {
            TutorialManager.Instance.CompleteTutorial();
            isCurrentTutorial = false;
        }
    }
}
