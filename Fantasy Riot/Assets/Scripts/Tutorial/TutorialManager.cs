using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> Tutorials = new List<Tutorial>();

    public TMP_Text expText;
    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TutorialManager>();
            if (instance == null)
                Debug.Log("There is no TutorialManager");

            return instance;
        }
    }

    private Tutorial currentTutorial;
    // Start is called before the first frame update
    void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentTutorial);
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }
    public void CompleteTutorial()
    {
        SetNextTutorial(currentTutorial.Order + 1);
    }

    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);
        if (!currentTutorial)
        {
            CompltedAllTutorials();
            return;
        }
        expText.text = currentTutorial.Explanation;
    }

    public void CompltedAllTutorials()
    {
        expText.text = "";
        //Back to menu
    }
    public Tutorial GetTutorialByOrder(int Order)
    {
        for(int i=0;i<Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == Order)
                return Tutorials[i];
        }
        return null;
    }
}
