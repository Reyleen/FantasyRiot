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
    private float moveSpeed = 50f;
    private GoldManager gold;
    
    [SerializeField]
    private int costTower;

    private Joystick joystick;
    Vector2 move;
    private Touch touch;
    public bool mov = false;
    public bool Locked { get => locked; set => locked = value; }
    
    public bool spawned;
    public bool Fire;
    public bool Earth;
    public bool Water;
    public bool Air;

    [SerializeField]
    private Tower fireTw;

    [SerializeField]
    private Golem earthTw;

    [SerializeField]
    private IceTower waterTw;

    [SerializeField]
    private Air airTw;

    private CountTower nTower;

    private Vector2 spr;


    bool TowerClicked;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gold = FindObjectOfType<GoldManager>();
        joystick = FindObjectOfType<FixedJoystick>();
        nTower = FindObjectOfType<CountTower>();
        spr = GameObject.FindGameObjectWithTag("NPC").GetComponent<Renderer>().bounds.size;
    }

    private void Update()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;

        if (Input.touchCount > 0 && !Locked)
        {
            if ((move.x > 0 || move.x < 0 || move.y > 0 || move.y < 0) && !mov)
            {
                touch = Input.GetTouch(1);
            }
            else if ((move.x == 0 && move.y == 0) || mov)
            {
                touch = Input.GetTouch(0);
                mov = true;
            }
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            direction = (touchPos - transform.position);
            rb.velocity = new Vector2(direction.x, direction.y) * moveSpeed;

            if (touch.phase == TouchPhase.Ended)
            {
                rb.velocity = Vector2.zero;
                foreach (Transform place in Placement.Placements)
                {
                    if (Mathf.Abs(transform.position.x - place.position.x) <= spr.x && Mathf.Abs(transform.position.y - place.position.y) <= spr.y)
                    {
                        Locked = true;
                        spawned = true;

                        if (Fire == true)
                        {
                            fireTw.Select();
                        }

                        if (Earth == true)
                        {
                            earthTw.Select();
                        }

                        if (Air == true)
                        {
                            airTw.Select();
                        }

                        if (Water == true)
                        {
                            waterTw.Select();
                        }

                            

                        nTower.Count(1);
                    }
                    
                    else if (Locked == false)
                    {
                        Destroy(gameObject);
                        gold.AddMoney(+costTower);
                        spawned = true;
                    }
                }

            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && Locked)
        {
            touch = Input.GetTouch(0);
           // Debug.Log(touch);
            touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            touchPos.z = 0;
            RaycastHit2D hit = Physics2D.Raycast(touchPos, (touch.position));
            

            if (touch.phase == TouchPhase.Began)
            { 
                if (hit.collider != null && hit.collider.tag == "Tower")
                Debug.Log(hit.collider.name);
                {
                    if (Fire == true)
                    {
                        Debug.Log("fire tower touched");
                        hit.collider.GetComponentInChildren<Tower>().Select();
                    }

                    if (Earth == true)
                    {
                        Debug.Log("earth tower touched");
                        hit.collider.GetComponentInChildren<Golem>().Select();
                    }

                    if (Air == true)
                    {
                        Debug.Log("air tower touched");
                        hit.collider.GetComponentInChildren<Air>().Select();
                      
                    }

                    if (Water == true)
                    {
                        Debug.Log("ice tower touched");
                        hit.collider.GetComponentInChildren<IceTower>().Select();
                    }
                
                }
            }
       
        }
    }
}