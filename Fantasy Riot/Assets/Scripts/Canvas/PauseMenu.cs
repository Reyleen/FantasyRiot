using UnityEngine;
using System.Collections;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI, UHD, MoveJoystick, AimJoystik, Torrette, map;


    public void Resume()
    {
        map.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        UHD.SetActive(true);
        MoveJoystick.SetActive(true);
        AimJoystik.SetActive(true);
        Torrette.SetActive(true);
    }

    public void Pause ()
    {
        map.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        UHD.SetActive(false);
        MoveJoystick.SetActive(false);
        AimJoystik.SetActive(false);
        Torrette.SetActive(false);
    }
}