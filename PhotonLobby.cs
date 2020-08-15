using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UIElements;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    
    public GameObject RaceButton;
    public GameObject CancelButton;
    public GameObject OfflineButton;
    public GameObject GameModeText;
    public GameObject TutorialButton;
    public GameObject OnlineButton;
    public int MaxPlayers;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        RaceButton.SetActive(true);

        OfflineButton.SetActive(false);
        OnlineButton.SetActive(true);
        TutorialButton.SetActive(true);
        GameModeText.SetActive(true);
        CancelButton.SetActive(false);
    }

    public void OnRaceButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
        RaceButton.SetActive(false);
        CancelButton.SetActive(true);
        Debug.Log("Joining room");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join random room");
        CreateRoom();
    }

    public void CreateRoom()
    {
        Debug.Log("Creating new room");
        int randomRoomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)MaxPlayers };
        PhotonNetwork.CreateRoom("Room" + randomRoomName , roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Creating new room Failed. Trying again.");
        CreateRoom();
    }

    public void OnCancelButtonClicked()
    {
        //PhotonNetwork.LeaveLobby();
        PhotonNetwork.LeaveRoom();
        RaceButton.SetActive(true);
        CancelButton.SetActive(false);
    }
}
