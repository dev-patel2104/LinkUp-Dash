using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public Vector3 offset;
    public float smooth = 1.125f;


    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").gameObject;
        offset.x = transform.position.x - Player.transform.position.x;
        offset.y = transform.position.y - Player.transform.position.y;
        offset.z = transform.position.z - Player.transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").gameObject;
            offset.x = transform.position.x - Player.transform.position.x;
            offset.y = transform.position.y - Player.transform.position.y;
            offset.z = transform.position.z - Player.transform.position.z;
            Debug.Log("Inside null");
        }
        Vector3 desiredPosition = Player.transform.position + offset;
        Vector3 SmoothPosition = Vector3.Lerp(transform.position, desiredPosition, smooth);
        transform.position = SmoothPosition;
        //transform.LookAt(Player);
    }

}
