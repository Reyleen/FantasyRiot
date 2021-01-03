using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*this script load new level Async, in this way the game don't need to load in run*/
public class LoadLevel : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void LoadScreen(string stringa)
    {
        
        StartCoroutine(LoadAsynchronously(stringa));
    }

    IEnumerator LoadAsynchronously(string stringa)
    {
        if (stringa != "Random")
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(stringa);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                yield return null;
            }
        } else
        {
            int index = Random.Range(5, 6);
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                yield return null;
            }
        }

        loadingScreen.SetActive(true);



    }
}
