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
   // public GameObject test;
    //public bool[] PlacementBool;

    // Start is called before the first frame update
    //void Awake()
    //{
    //    Placements = new Transform[transform.childCount];

    //    for (int i = 0; i < Placements.Length; i++)
    //    {
    //        Placements[i] = transform.GetChild(i);
           
    //    }
    ////    Placements[0].gameObject = test.gameObject;
    //}

    void Awake()
    {
        Placements = new Transform[transform.childCount];
        
        sizeX = new float[Placements.Length];
        sizeY = new float[Placements.Length];
        //Debug.Log(sizeX.Length);
        for (int i = 0; i < Placements.Length; i++)
        {
            Placements[i] = transform.GetChild(i);
            Debug.Log(Placements[i].position);
          
           //Vector3 minSize = Placements[i].GetComponent<SpriteRenderer>().bounds.min;
           //Vector3 MaxSize  = Placements[i].GetComponent<SpriteRenderer>().bounds.max;
           
        
           
           
           
            
        }
    }

    private void Start()
    {
        for (int i = 0; i < Placements.Length; i++)
        {

           // Debug.Log("try");
          
           // Debug.Log(Mathf.Abs(Placements[i].GetComponent<Rigidbody2D>().position.x - Placements[i].GetComponent<SpriteRenderer>().bounds.min.x));
            sizeX[i] = Mathf.Abs(Placements[i].GetComponent<Rigidbody2D>().position.x - Placements[i].GetComponent<SpriteRenderer>().bounds.max.x);
            sizeY[i] = Mathf.Abs(Placements[i].GetComponent<Rigidbody2D>().position.y - Placements[i].GetComponent<SpriteRenderer>().bounds.max.y);
        }
    }



}