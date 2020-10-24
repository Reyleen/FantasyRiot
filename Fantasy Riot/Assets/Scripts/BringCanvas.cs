using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringCanvas : MonoBehaviour
{
    private static bool canvasExisting;

    // Start is called before the first frame update
    void Start()
    {
            // If the canvas does not exist in the scene set it active and don't destroy it on load
            if (!canvasExisting)
            {
                canvasExisting = true;
                DontDestroyOnLoad(transform.gameObject);
            } // Otherwhise destroy it
            else
            {
                Destroy(gameObject);
            }
        }
 

    // Update is called once per frame
    void Update()
    {
        
    }
}
