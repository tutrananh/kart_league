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

    private Vector3 defaultBallPosition;
    public Transform ballPosition;

    public GameObject spawnPlayers;
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
        defaultBallPosition = new Vector3(-8.62f, 2.13f, 0.48f);

    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
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
            }else if (redGoal == blueGoal)
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
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateGoal(bool side)
    {
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
        ballPosition.position = defaultBallPosition;
        PhotonView PV = GetComponent<PhotonView>();
        PV.RPC("Setting", RpcTarget.Others, team, defaultBallPosition);
    }

    [PunRPC]
    void Setting( bool side, Vector3 defaultBallPosition)
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
        ballPosition.position = defaultBallPosition;
    }


}

