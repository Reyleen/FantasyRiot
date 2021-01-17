using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    public AudioSource death;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.isAlive)
        {
            if (!death.isPlaying)
                death.Play();
        }
        else
        {
            death.Stop();
        }
    }
}
