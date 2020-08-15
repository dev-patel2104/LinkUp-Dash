using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class JoystickMovement : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Joystick joystick;
    [SerializeField] GameObject player;
    [SerializeField] bool jump;
    [SerializeField] bool crouch;
    [SerializeField] float MoveSpeed = 10f;
    [SerializeField] float crouchSpeed = 4f;
    [SerializeField] GameObject jumpButton;
    [SerializeField] GameObject crouchUpButton;
    [SerializeField] GameObject crouchDownButton;
    [Range(1f,7f)][SerializeField] float speedReduceFactor = 6f;


    Rigidbody rg;
    Animator an;

    rope r;
    float temp;

    public PhotonView PV;

    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rg = player.GetComponent<Rigidbody>();
        an = player.GetComponent<Animator>();
        r = FindObjectOfType<rope>();
        temp = MoveSpeed;
        PV = GetComponent<PhotonView>();
        jumpButton.GetComponent<Button>().onClick.AddListener(OnJumpClick);
        crouchUpButton.GetComponent<Button>().onClick.AddListener(GetUp);
        crouchDownButton.GetComponent<Button>().onClick.AddListener(GetDown);
    }
    
    void Update()
    {
        if (PV.IsMine)
        {



            playerMovement();
            if (r.ropePresent == true)
            {
                MoveSpeed = 8f;
            }
            else if (r.ropePresent == false)
            {
                temp = MoveSpeed;
                MoveSpeed = speedReduceFactor;
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (PV.IsMine)
        {

            if (collision.gameObject.CompareTag("Platform"))
            {
                an = player.GetComponent<Animator>();
                jump = true;
                an.SetBool("Jumping", false);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (PV.IsMine)
        {


            if (collision.gameObject.CompareTag("Platform"))
            {
                jump = false;
            }
        }
    }

    public void OnJumpClick()
    {
        if (PV.IsMine)
        {



            if (jump == true)
            {
                var an1 = player.GetComponent<Animator>();
                var rg1 = player.GetComponent<Rigidbody>();
                rg1.AddForce(0, 10000f, 0);
                an1.SetBool("Jumping", true);
            }
        }
    }
    
    public void GetDown()
    {
        if (PV.IsMine)
        {


            an = player.GetComponent<Animator>();
            temp = MoveSpeed;
            MoveSpeed = crouchSpeed;
            an.SetBool("Crouch", true);
        }

    }
    public void GetUp()
    {
        if (PV.IsMine)
        {


            an = player.GetComponent<Animator>();
            MoveSpeed = temp;
            an.SetBool("Crouch", false);
        }

       
    }

    private void playerMovement()
    {
        if (PV.IsMine)
        {

            var rigidbody = GetComponent<Rigidbody>();
            an = player.GetComponent<Animator>();
            rigidbody.velocity = new Vector3(joystick.Horizontal * MoveSpeed, rigidbody.velocity.y, joystick.Vertical * MoveSpeed);
            if (rigidbody.velocity != Vector3.zero)
            {
                an.SetBool("Running", true);
            }
            else
            {
                an.SetBool("Running", false);
            }
        }
    }
}
