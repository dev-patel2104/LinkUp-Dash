using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;

    public GameObject player;

    public int myTeam;

    public static PhotonPlayer photonPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
        {
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
        
    }

    private void Update()
    {
        if (player == null && myTeam != 0)
        {



            if (myTeam == 1)
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamOne.Length);
                if (PV.IsMine)
                {
                    player = PhotonNetwork.Instantiate(Path.Combine("prefabs", "player"),
                        GameSetup.GS.spawnPointsTeamOne[spawnPicker].position, GameSetup.GS.spawnPointsTeamOne[spawnPicker].rotation, 0);
                }
            }

            else
            {
                int spawnPicker = Random.Range(0, GameSetup.GS.spawnPointsTeamTwo.Length);
                if (PV.IsMine)
                {
                    player = PhotonNetwork.Instantiate(Path.Combine("prefabs", "player"),
                        GameSetup.GS.spawnPointsTeamTwo[spawnPicker].position, GameSetup.GS.spawnPointsTeamTwo[spawnPicker].rotation, 0);
                }
            }
        }
    }

    [PunRPC]
    void RPC_GetTeam()
    {
        myTeam = GameSetup.GS.nextPlayersTeam;
        GameSetup.GS.UpdateTeam();
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, myTeam);
    }

    [PunRPC]
    void RPC_SentTeam(int whichTeam)
    {
        myTeam = whichTeam;
    }

}
