using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class WaitingRoomController : MonoBehaviour
{
    private bool full;

    public GameObject waitingUI;
    public Text waitingText;
    // Start is called before the first frame update
    void Start()
    {
        full = false;
        PlayerPrefs.SetInt("Player_ID", PhotonNetwork.CurrentRoom.PlayerCount);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && full == false)
        {
            PhotonNetwork.LoadLevel(3);
            full = true;
        }
        else
        {
            waitingText.text = $"Waiting for player... {PhotonNetwork.CurrentRoom.PlayerCount}/2";
        }
    }

    public void OnClickReturnWaitingBtn()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(1);
    }
}
