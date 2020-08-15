using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using System.Linq;

public class PhotonTrial : MonoBehaviour
{
    public PhotonView PV;
    public int id;
    public GameObject x;
    void Start()
    {
        if(PV.IsMine)
        {
            id = PV.ViewID;
            Debug.Log(id);
         
            var player1 = PhotonNetwork.CurrentRoom.Players.ElementAt(0);
            Debug.Log(player1);

            x = PV.gameObject;
        }
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
