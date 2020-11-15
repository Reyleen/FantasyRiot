using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox, talkButton;
    public Text dText;

    public bool dialogActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogActive) {
            foreach (Touch touch in Input.touches)
            {
                if (touch.fingerId == 0)
                {
                    talkButton.SetActive(true);
                    dBox.SetActive(false);
                    dialogActive = false;
                }
            }
        }
    }

   public void ShowBox(string dialogue)
    {
        talkButton.SetActive(false);
        dialogActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }
}
