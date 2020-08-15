using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] float downForce = -800f;
    public PhotonView PV;
    public Rigidbody Rigidbody;
    //public GameObject player;
    public Animator an2;

    public bool canJump;

    /*public void OnJumpButton()
    {
        if(PV.IsMine)
        {
            
            Rigidbody.AddForce(0, 10000, 0);
        }
    }*/

    private void Update()
    {
        if (PV.IsMine && canJump == true)
        {


            if (Input.touchCount == 1)
            {
                Touch touch1 = Input.GetTouch(0);
                //Touch touch2 = Input.GetTouch(1);
                //Debug.Log("Touch detected");

                //Vector3 touch1_world = Camera.main.ScreenToWorldPoint(touch1.position);

                if (touch1.phase == TouchPhase.Began)
                {
                    //Debug.Log("Phase begin@@@");
                    
                    if (touch1.position.x > Screen.width / 2 && touch1.position.y > Screen.height / 2)
                    {
                        //Debug.Log("Jump detected");
                        //Debug.Log("PV is mine");
                        Rigidbody.AddForce(0, 10000, 0);
                        an2.SetBool("Jumping", true);
                    }
                }
            }
            else if (Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                //Vector3 touch1_world = Camera.main.ScreenToWorldPoint(touch1.position);
                //Vector3 touch2_world = Camera.main.ScreenToWorldPoint(touch2.position);
                //Debug.Log("Touch detected");


                if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)

                {
                    //Debug.Log("Touch Begin@@@");

                    if ((touch2.position.x > Screen.width / 2 && touch2.position.y > Screen.height / 2) || (touch1.position.x > Screen.width / 2 && touch1.position.y > Screen.height / 2))
                    {
                        //Debug.Log("Jump detected");
                        Rigidbody.AddForce(0, 10000, 0);
                        an2.SetBool("Jumping", true);
                    }
                }
            }
        }

        if(PV.IsMine)
        {
            if(!canJump)
            {
                Rigidbody.AddForce(0f, downForce, 0f, ForceMode.Acceleration);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (PV.IsMine)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                canJump = true;
                an2.SetBool("Jumping", false);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (PV.IsMine)
        {
            if (collision.gameObject.CompareTag("Platform"))
            {
                canJump = false;
            }
        }
    }


}
