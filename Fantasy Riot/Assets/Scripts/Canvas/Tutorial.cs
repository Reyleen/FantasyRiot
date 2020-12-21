using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Joystick joystick;
    public Joystick aimStick;

    public GameObject stick;

    public GameObject[] popUps;
    private int popUpIndex;
    Vector2 move;
    Vector2 aim;
    // Start is called before the first frame update
    void Start()
    {
        stick.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        aim.x = aimStick.Horizontal;
        aim.y = aimStick.Vertical;

        for (int i = 0; i < popUps.Length; i++)
        {
            if(i == popUpIndex)
            {
                popUps[i].SetActive(true);
            } else
            {
                popUps[i].SetActive(false);
            }
        }

        if (popUpIndex == 0)
        {
            if (move.x > 0.5f || move.x < -0.5f || move.y > 0.5f || move.y < -0.5f)
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            stick.SetActive(true);
            //aimStick.enabled = true;
            if (aim.x > 0.5f || aim.x < -0.5f || aim.y > 0.5f || aim.y < -0.5f)
            {
                popUpIndex++;
            }
        }
    }
}
