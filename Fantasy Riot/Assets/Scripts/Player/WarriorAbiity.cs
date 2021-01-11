using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAbiity : MonoBehaviour
{
    public GameObject clone;
    public GameObject pla;
    private Transform PlayerPosition;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPosition = pla.GetComponent<Transform>();
        PlayerPosition.position = new Vector3(PlayerPosition.position.x, PlayerPosition.position.y, PlayerPosition.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Ability()
    {
        Instantiate(clone, new Vector3(PlayerPosition.position.x + 1f, PlayerPosition.position.y, 0), transform.rotation /*transform.position, Quaternion.identity*/);
    }
}
