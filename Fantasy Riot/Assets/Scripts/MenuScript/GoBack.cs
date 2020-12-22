using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBack : MonoBehaviour
{
    public void GoBackMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
