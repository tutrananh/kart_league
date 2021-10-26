using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnPlayers: MonoBehaviour
{
    private string[] kartList = { "KartClassic_Player", "Muscle_Player", "Roadster_Player" };

    public Transform[] playerSpawnPositionList;

    private GameObject player;
    private void Awake()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        Vector3 rotateAngle = playerSpawnPositionList[PlayerPrefs.GetInt("Player_ID") - 1].localEulerAngles;
        rotateAngle += new Vector3(0, 180, 0);
        //rotateAngle = Quaternion.Euler(rotateAngle.x, rotateAngle.y + 180, rotateAngle.z);
        player = PhotonNetwork.Instantiate(kartList[PlayerPrefs.GetInt("Key") - 1],
                                    playerSpawnPositionList[PlayerPrefs.GetInt("Player_ID") - 1].position, playerSpawnPositionList[PlayerPrefs.GetInt("Player_ID") - 1].rotation);
        if (PlayerPrefs.GetInt("Player_ID") == 1 || PlayerPrefs.GetInt("Player_ID") == 3)
        {
            player.transform.Rotate(rotateAngle);
        }
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
