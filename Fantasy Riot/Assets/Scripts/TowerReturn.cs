using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime;
using System.Data.SqlTypes;

public class TowerReturn : MonoBehaviour
{
    [SerializeField]
    private bool locked;
    private float deltaX, deltaY;
    private Vector3 touchPos;
    private Rigidbody2D rb;
    private Vector3 direction;
    private float moveSpeed = 10f;

    public bool Locked { get => locked; set => locked = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.touchCount>0 && !Locked)
        {
            Touch touch = Input.GetTouch(0);
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            direction = (touchPos - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if(touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                foreach (Transform place in Placement.Placements)
                {
                    if (Mathf.Abs(transform.position.x - place.position.x) <= 3f && Mathf.Abs(transform.position.y - place.position.y) <= 7.4f)
                    {
                        Locked = true;
                        Debug.Log("PLACED");
                    }
                    else if (Locked == false)
                    {
                        Debug.Log("DESTROYED");
                        Destroy(gameObject);
                    }
                }
                
            }        
        }
    }

}







