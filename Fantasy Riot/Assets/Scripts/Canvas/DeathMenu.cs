using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    private bool isShowned = false;
    public Image backgroundImg;
    public GameObject retry, quit;
    private SpawnPoint spwn;
    private float transition = 0.0f;
    public SaveScore Save;
    public bool ded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        ded = true;
    }

    // Update is called once per frame
    void Update()
    {
        spwn = FindObjectOfType<SpawnPoint>();

        if (!isShowned)
        {
            gameObject.SetActive(false);
            return;
        }
        else
        {
            transition += Time.deltaTime;
            backgroundImg.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);
        }
    }

    public void ToggleEndMenu()
    {
        gameObject.SetActive(true);
        isShowned = true;
        if (ded)
        {
            string stringa = SceneManager.GetActiveScene().name;
            if (stringa == "ArcadeCity" || stringa == "ArcadeDungeon" || stringa == "ArcadeForest" || stringa == "ArcadeOutside")
            {
                Save.SavingInfinite();
            }
            else
            {
                Save.PlayerLoseStory();
            }
            ded = false;
        }
    }

    public void Retry()
    {
        if (SceneManager.GetActiveScene().name=="CaveTutorial")
        { 
            SceneManager.LoadScene("Tutorial");
            gameObject.SetActive(false);
            return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }

    public void ToMenu()
    {

    }
}
