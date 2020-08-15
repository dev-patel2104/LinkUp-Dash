using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraChange : MonoBehaviour
{

    [SerializeField] private Vector3 camInitPos;
    [SerializeField] private Vector3 camFinPos;
    [Range(0, 1.0f)] [SerializeField] private float smoothness;
    [SerializeField] GameObject playerCam;
    [SerializeField] Camera mainCamera;
    [SerializeField] GameObject toggleButton;
  //  [SerializeField] Vector3 offsetInt;
  //  [SerializeField] Vector3 offsetFin;

    private bool toggle = false;
    public PhotonView PV;
    public GameObject parent;

    private void Start()
    {
        PV = parent.GetComponent<PhotonView>();
        mainCamera = Camera.main;
        if (PV.IsMine)
        {
            camInitPos = playerCam.transform.localPosition;
           // offsetInt = camInitPos;
            camFinPos = mainCamera.gameObject.transform.position;
           // offsetFin = camFinPos - parent.transform.position; // local position considering player as the center
           // At present the value of OFFSET FIN is inputes through inspector manually
            toggleButton = GameObject.FindGameObjectWithTag("ToggleButton");
            toggleButton.GetComponent<Button>().onClick.AddListener(onClickToggle);
        }
    }

    public void onClickToggle()
    {
        if (PV.IsMine)
        {
            if (!toggle)
            {
                // transform.localPosition = Vector3.Slerp(offsetInt, offsetFin, smoothness);
                playerCam.SetActive(false);
                
                toggle = true;
            }
            else
            {
                //  transform.localPosition = Vector3.Slerp(offsetFin, offsetInt, smoothness);
                playerCam.SetActive(true);

                toggle = false;
            }
        }

    }

    private void FixedUpdate()
    {
        if(!PV.IsMine)
        {
            playerCam.GetComponent<Camera>().enabled = false;
        }
        
        if(PV.IsMine)
        {

        }
    }
}
