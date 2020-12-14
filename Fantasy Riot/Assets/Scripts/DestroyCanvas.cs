using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DestroyCanvas : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Destroy()
    {
        Destroy(gameObject);
        Destroy(camera);
        Destroy(player);
    }
}
