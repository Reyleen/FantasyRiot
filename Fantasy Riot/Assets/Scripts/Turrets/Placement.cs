using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public  Transform[] Placements;
    public  float[] sizeX;
    public  float[] sizeY;
    Rigidbody2D rb;
    Vector2 position;

    void Awake()
    {
        Placements = new Transform[transform.childCount];
        
        sizeX = new float[Placements.Length];
        sizeY = new float[Placements.Length];
        for (int i = 0; i < Placements.Length; i++)
        {
            Placements[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        for (int i = 0; i < Placements.Length; i++)
        {
            sizeX[i] = Mathf.Abs(Placements[i].GetComponent<Rigidbody2D>().position.x - Placements[i].GetComponent<SpriteRenderer>().bounds.max.x);
            sizeY[i] = Mathf.Abs(Placements[i].GetComponent<Rigidbody2D>().position.y - Placements[i].GetComponent<SpriteRenderer>().bounds.max.y);
        }
    }



}