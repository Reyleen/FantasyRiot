using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DetectTouchIntro : MonoBehaviour
{
    private GameObject wrap;
    // Update is called once per frame
    private void Start()
    {
        wrap = GameObject.Find("LevelLoader");
    }
    void Update()
    {
        if (Input.touchCount > 0) 
        { 
             Debug.Log("entrato");
             wrap.GetComponent<LoadLevel>().LoadScreen("Menu");
        }
    }
}
