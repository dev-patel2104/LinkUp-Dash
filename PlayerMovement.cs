using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* private Joystick Joystick;

     public PhotonView PV;

     public Rigidbody Rigidbody;

     public Animator an1;

     public float MoveSpeed = 5f;




     //public static PlayerMovement playerMovement;
     void Start()
     {
         Joystick = FindObjectOfType<Joystick>();
         lastCheckPoint = transform.position;
         PV = GetComponent<PhotonView>();
     }

     // Update is called once per frame
     void Update()
     {
         if (PV.IsMine)
         {
             if (Rigidbody != null)
             {
                 Rigidbody.velocity = new Vector3(Joystick.Horizontal * MoveSpeed, Rigidbody.velocity.y, Joystick.Vertical * MoveSpeed);
                 if (Rigidbody.velocity != Vector3.zero)
                 {
                     an1.SetBool("Running", true);
                 }
                 else
                 {
                     an1.SetBool("Running", false);
                 }
             }  
         }
     }*/

    // change player speed when rope is destroyed from this script otherwise problem making wrong call 
    [SerializeField] FixedJoystick joystick;
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] public float moveSpeed = 3f;
    [SerializeField] public Vector3 lastCheckPoint;

    public float Hinput;
    public float Vinput;
    public Transform m_Cam;
    private Vector3 m_CamForward;
    private bool m_Jump;
    private Vector3 m_Move;
    private float temp;
    PhotonView PV;

    Rigidbody rg;
    Animator an1;

    float m_TurnAmount;
    float m_ForwardAmount;
    Vector3 m_GroundNormal;

    void Start()
    {
        temp = moveSpeed;
        rg = GetComponent<Rigidbody>();
        an1 = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
        joystick = FindObjectOfType<FixedJoystick>();
        rg.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    void Update()
    {
        // rg.velocity = new Vector3(joystick.Horizontal * 5f, 0f, joystick.Vertical * 5f);

    }
    private void FixedUpdate()
    {
        if(PV.IsMine)
        {
            if(rg != null)
            {
                Hinput = joystick.Horizontal;
                Vinput = joystick.Vertical;
                bool crouch = Input.GetKey(KeyCode.C);

                if (m_Cam != null)
                {

                    m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                    m_Move = Vinput * m_CamForward + Hinput * m_Cam.right;
                }
                else
                {
                    m_Move = Vinput * Vector3.forward + Hinput * Vector3.right;
                }
#if !MOBILE_INPUT

                if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif


                Move(m_Move);
                m_Jump = false;
                if (Hinput != 0 || Vinput != 0)
                {
                    moveSpeed = temp;
                }
                else
                {
                    temp = moveSpeed;
                    moveSpeed = 0f;
                }
            }
        }
        
    }


    public void Move(Vector3 move)
    {
        if(PV.IsMine)
        {
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            ApplyExtraTurnRotation();
            if (Hinput != 0 || Vinput != 0)
            {
                rg.velocity = new Vector3(m_Move.x * moveSpeed, m_Move.y * moveSpeed, m_Move.z * moveSpeed);
                an1.SetBool("Running", true);
            }
            else
            {
                rg.velocity = new Vector3(m_Move.x * moveSpeed, m_Move.y * moveSpeed, m_Move.z * moveSpeed);
                an1.SetBool("Running", false);
            }
        }
        

    }
    void ApplyExtraTurnRotation()
    {
        if(PV.IsMine)
        {
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(PV.IsMine)
        {
            if (other.CompareTag("Quit"))
            {
                StartCoroutine(winCeleb());
            }
        }
    }

    IEnumerator winCeleb()
    {
        an1.SetBool("Winning", true);
        an1.SetBool("Running", false);
        rg.velocity = Vector3.zero;
        rg = null;
        var electricity = transform.Find("spark");
        electricity.GetComponent<ParticleSystem>().Play(true);
        yield return new WaitForSeconds(7f);
        an1.SetBool("Winning", false);

    }
}
