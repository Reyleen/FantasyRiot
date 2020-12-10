using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placement : MonoBehaviour
{
    public static Transform[] Placements;
    //public bool[] PlacementBool;

    // Start is called before the first frame update
    void Awake()
    {
        Placements = new Transform[transform.childCount];

        for (int i = 0; i < Placements.Length; i++)
        {
            Placements[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}