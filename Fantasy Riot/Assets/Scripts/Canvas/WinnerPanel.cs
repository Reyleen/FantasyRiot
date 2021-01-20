using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinnerPanel : MonoBehaviour
{
    private bool isShowned = false;
    public Image backgroundImg;
    private string FIRST_TUTORIAL = "firstTime";
    private bool firstTime = true;
    public GemsManager gemsMan;
    public AudioSource win;
    public bool game;
    public bool wi = true;
    [SerializeField]
    private Text messageText;
    public SaveScore Save;

    [SerializeField]
    private TextWriter textWriter;

    void Awake()
    {
        Application.targetFrameRate = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        if (game)
        {
            textWriter.AddWriter(messageText, "Congratulations!\n You defeated every enemy and\n thanks to you the reign is again safe.", .05f);
        }
        else
        {
            textWriter.AddWriter(messageText, "Congratulations!\n You have completed the tutorial!\n The reign is in danger, and you are now ready to defend it!", .05f);
        }
        wi = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleWinPan()
    {
        
        gameObject.SetActive(true);
        isShowned = true;
        win.Play();
        if (wi)
        {
            Save.SavingNormal();
            wi = false;
        }
    }

}
