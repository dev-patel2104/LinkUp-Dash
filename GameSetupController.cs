using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetupController : MonoBehaviour
{
    // Start is called before the first frame update
    //public static GameSetupController GS;

    //public Transform[] spawnPoints;

    /*private void OnEnable()
    {
        if(GameSetupController.GS == null)
        {
            GameSetupController.GS = this;
        }
    }*/

    private void Start()
    {
        CreatePlayer();
    }


    private void CreatePlayer()
    {
        Debug.Log("Creating Player...");
        Vector3 position = new Vector3(6, 3, 16);
        //yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Instantiate(Path.Combine("prefabs", "PhotonNetworkPlayer"), position, Quaternion.identity);
    }
}
