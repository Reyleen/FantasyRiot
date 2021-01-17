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


    bool TowerClicked;

    Placement placement;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gold = FindObjectOfType<GoldManager>();
        joystick = FindObjectOfType<FixedJoystick>();
        nTower = FindObjectOfType<CountTower>();
        placement = FindObjectOfType<Placement>();
      
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

                for (int i = 0; i < placement.Placements.Length; i++)
                {
                    if (Mathf.Abs(transform.position.x - placement.Placements[i].transform.position.x) <= placement.sizeX[i] &&  Mathf.Abs(transform.position.y - placement.Placements[i].position.y) <= placement.sizeY[i])
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
                }
                
                if (Locked == false)
                {
                    gold.AddMoney(+costTower);
                    spawned = true;
                    Destroy(gameObject, 0.2f);
                }
            }
        }
    }


   

    public void Sold()
    {
        Destroy(gameObject);
    }
}