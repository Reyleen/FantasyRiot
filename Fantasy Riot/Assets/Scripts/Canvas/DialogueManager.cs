using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox, talkButton, stick1, stick2;
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
                    stick1.SetActive(true);
                    stick2.SetActive(true);
                    talkButton.SetActive(true);
                    dBox.SetActive(false);
                    dialogActive = false;
                }
            }
        }
    }

   public void ShowBox(string dialogue)
    {
        stick2.SetActive(false);
        stick1.SetActive(false);
        talkButton.SetActive(false);
        dialogActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }
}
