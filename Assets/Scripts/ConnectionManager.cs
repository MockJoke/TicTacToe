using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject roomScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject connectionScreen;
    [SerializeField] private TMP_InputField RoomID;
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected");
        
        roomScreen.SetActive(true);
        loadingScreen.SetActive(false);
        
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("DisConnected " + cause);
    }

    public override void OnJoinedLobby()
    {
        print("Joined...");
    }

    public void onCreateRoomClick()
    {
        PhotonNetwork.CreateRoom(RoomID.text, new RoomOptions { MaxPlayers = 2 });
    }

    public void onJoinRoomClick()
    {
        PhotonNetwork.JoinRoom(RoomID.text);
    }

    public override void OnCreatedRoom()
    {
        print("Room Created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Room Create Failed");
    }

    public override void OnJoinedRoom()
    {
        print("Room Joined");
        print(PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            PhotonNetwork.NickName = "0";
        }
        else
        {
            PhotonNetwork.NickName = "X";
        }

        // PhotonNetwork.LoadLevel("Play");
        connectionScreen.SetActive(false);
        gameScreen.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        print("Room Not Joined");
    }
}

  
