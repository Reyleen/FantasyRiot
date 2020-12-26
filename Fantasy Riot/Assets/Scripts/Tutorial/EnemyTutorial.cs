using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : Tutorial
{
    public EnemyHealthManager HitEnemy;
    public override void CheckIfHappening()
    {
        if (HitEnemy.CurrentHealth<=0)
        {
            Debug.Log("Enemy");
            TutorialManager.Instance.CompleteTutorial();
        }
    }
    
}
