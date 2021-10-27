using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPun
{
    private int redGoal = 0, blueGoal = 0;
    private bool timerIsRunning;
    private float timeRemaining = 180;

    public SpawnPlayers spawnPlayers;
    public GameObject endGame;

    public Text blueGoals;
    public Text redGoals;
    public Text timeText;
    public Text winner;


    // Start is called before the first frame update
    private void Awake()
    {

        endGame.SetActive(false);
        GameObject themeSong = GameObject.FindGameObjectWithTag("ThemeSong");
        Destroy(themeSong.gameObject);
        timerIsRunning = true;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (timerIsRunning)
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    timeRemaining = 0;
                    timerIsRunning = false;
                }
                DisplayTime(timeRemaining);
            }
            else
            {
                if (redGoal > blueGoal)
                {
                    winner.text = "Red team has won the match!!!";
                }
                else if (redGoal == blueGoal)
                {
                    winner.text = "Draw match!!! What a game!";
                }
                else
                {
                    winner.text = "Blue team has won the match!!!";
                }
                endGame.SetActive(true);
            }
            PhotonView PV = GetComponent<PhotonView>();
            PV.RPC("SendTimeToClient", RpcTarget.Others, timerIsRunning, timeRemaining);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateGoal(bool side)
    {
        if (!endGame.activeSelf)
        {
            spawnPlayers.ReSpawnPlayer();
            bool team = side;
            if (team)
            {
                redGoal++;
                redGoals.text = $"{redGoal} Red";
            }
            else
            {
                blueGoal++;
                blueGoals.text = $"Blue {blueGoal}";
            }
            PhotonView PV = GetComponent<PhotonView>();
            PV.RPC("Setting", RpcTarget.Others, team);
        }
    }

    [PunRPC]
    void Setting( bool side)
    {
        if (side)
        {
            redGoal++;
            redGoals.text = $"{redGoal} Red";
        }
        else
        {
            blueGoal++;
            blueGoals.text = $"Blue {blueGoal}";
        }
        spawnPlayers.ReSpawnPlayer();
    }

    [PunRPC]
    void SendTimeToClient(bool isRunning, float timeRev)
    {
        timerIsRunning = isRunning;
        timeRemaining = timeRev;
        if (timerIsRunning)
        {
                DisplayTime(timeRemaining - 0.1f);
        }
        else
        {
            DisplayTime(0);
            if (redGoal > blueGoal)
            {
                winner.text = "Red team has won the match!!!";
            }
            else if (redGoal == blueGoal)
            {
                winner.text = "Draw match!!! What a game!";
            }
            else
            {
                winner.text = "Blue team has won the match!!!";
            }
            endGame.SetActive(true);
        }
    }


}

