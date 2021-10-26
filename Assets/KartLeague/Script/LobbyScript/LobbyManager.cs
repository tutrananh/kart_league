using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEngine.SceneManagement;
public class LobbyManager : MonoBehaviourPunCallbacks
{

    [Header("----UI Screens----")]
    public GameObject roomUI;
    public GameObject connectUI;

    [Header("----UI Text----")]
    public Text statusText;
    public Text connectingText;

    [Header("----UI Input Fields----")]
    public InputField createRoom;
    public InputField joinRoom;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, -9.81f, 0);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectingText.text = "Joining Lobby...";
        PhotonNetwork.JoinLobby(TypedLobby.Default);

    }
    public override void OnJoinedLobby()
    {
        statusText.text = " Joined Lobby";
        roomUI.SetActive(true);
        connectUI.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        roomUI.SetActive(false);
        connectingText.text = "Disconnected.." + cause.ToString();
        connectUI.SetActive(true);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        int roomName = Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName.ToString(), roomOptions, TypedLobby.Default, null);
    }

    //btn click
    public void OnClickCreateBtn()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createRoom.text, roomOptions, TypedLobby.Default, null);
    }
    public void OnClickJoinBtn()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom(joinRoom.text, roomOptions, TypedLobby.Default);
    }
    public void OnClickPlayNow()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnClickReturnBtn()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
