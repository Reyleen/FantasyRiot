using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private bool isShowned = false;
    public Image backgroundImg;
    public GameObject player;
    public GameObject retry, quit;

    private float transition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShowned)
            return;

        transition += Time.deltaTime;
        backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
        
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        isShowned = true;
    }

    public void Retry()
    {
        retry.SetActive(false);
        quit.SetActive(false);
        backgroundImg.enabled = false;
        SceneManager.LoadScene("Tutorial");        
    }

    public void ToMenu()
    {

    }
}
