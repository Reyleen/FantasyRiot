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
    public bool skip = false;
    public GameObject go;

    public GameObject arc, war, wit, pal;
    public GameObject[] tips;

    public void LoadScreen(string stringa)
    {
        
        StartCoroutine(LoadAsynchronously(stringa));
    }

    IEnumerator LoadAsynchronously(string stringa)
    {
        if (stringa == "Story")
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(stringa);

            operation.allowSceneActivation = false;
            loadingScreen.SetActive(true);
            pal.SetActive(true);
            while (!operation.isDone)
            {

                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                if (skip)
                {
                    operation.allowSceneActivation = true;
                }
                skip = false;
                yield return null;
            }
        }
        else if (stringa == "Dungeon")
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(stringa);

            operation.allowSceneActivation = false;
            loadingScreen.SetActive(true);
            wit.SetActive(true);
            while (!operation.isDone)
            {

                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                if (skip)
                {
                    skip = false;
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
        else if (stringa == "Random")
        {
            loadingScreen.SetActive(true);
            int ran = Random.Range(1, 4);
            tips[ran].SetActive(true);
            int index = Random.Range(6, 7);
            go.SetActive(false);
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                if (skip)
                {
                    skip = false;
                    operation.allowSceneActivation = true;
                }
                yield return null;
            }
        }
        else
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(stringa);
            loadingScreen.SetActive(true);
            int ran = Random.Range(1, 4);
            tips[ran].SetActive(true);
            go.SetActive(false);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);
                slider.value = progress;
                progressText.text = progress * 100 + "%";
                yield return null;
            }

        }
    }

    public void Go()
    {
        skip = true;
    }
}
