using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [SerializeField] float crouchSpeed = 3f;
    private float temp;
    public PhotonView PV;
    public Animator an3;
    public bool canCrouch = true;

    PlayerMovement pm;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            if (Input.touchCount == 1)
            {
                Touch touch1 = Input.GetTouch(0);

                if (touch1.phase == TouchPhase.Began)
                {

                    if (touch1.position.x > Screen.width / 2 && touch1.position.y < Screen.height / 2)
                    {
                        if (canCrouch == true)
                        {
                            CrouchDown();
                        }
                        else if (canCrouch == false)
                        {
                            CrouchUp();
                        }
                    }
                }
            }
            else if(Input.touchCount == 2)
            {
                Touch touch1 = Input.GetTouch(0);
                Touch touch2 = Input.GetTouch(1);

                if(touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
                {
                    if((touch1.position.x > Screen.width / 2 && touch1.position.y < Screen.height / 2) || (touch2.position.x > Screen.width / 2 && touch2.position.y < Screen.height / 2))
                    {
                        if (canCrouch == true)
                        {
                            CrouchDown();
                        }
                        else if (canCrouch == false)
                        {
                            CrouchUp();
                        }
                    }
                }

            }
        }
    }

    public void CrouchUp()
    { 
        pm.moveSpeed = temp;
        an3.SetBool("Crouch", false);
        canCrouch = true;
    }

    public void CrouchDown()
    {
        temp = pm.moveSpeed;
        pm.moveSpeed = crouchSpeed;
        an3.SetBool("Crouch", true);
        canCrouch = false;
    }

}
