using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Presenter : MonoBehaviour
{
    private int playerIndex;
    private GameObject prefabPlayer;
    private GameObject player;
    private const string PLAYER_KEY = "Key";

    public Button prevBtn;
    public Button nextBtn;
    private void Awake()
    {
        prevBtn.onClick.AddListener(OnPrev);
        nextBtn.onClick.AddListener(OnNext);
        if (PlayerPrefs.HasKey(PLAYER_KEY) == false) 
        {
            playerIndex = 1;
        }
        else
        {
            playerIndex = PlayerPrefs.GetInt(PLAYER_KEY);
        }
    }

    private void OnPrev()
    {
        if (playerIndex == 1)
        {
            playerIndex = 3;
        }
        else playerIndex--;
        Destroy(player);
        StartCoroutine(InItPlayer());
    }
    private void OnNext()
    {
        if (playerIndex == 3)
        {
            playerIndex = 1;
        }
        else playerIndex++;
        Destroy(player);
        StartCoroutine(InItPlayer());
    }

    private IEnumerator Start()
    {
        yield return InItPlayer();
    }

    private IEnumerator InItPlayer()
    {
        var request = Resources.LoadAsync<GameObject>($"PrefabsUI/{playerIndex}");

        while (!request.isDone)
        {
            yield return null;
        }

        prefabPlayer = (GameObject)request.asset;

        SetPlayer();
        SetPlayerState(true);
    }


    private void SetPlayer()
    {
        player = GameObject.Instantiate(prefabPlayer);
        player.transform.localPosition = new Vector3(-19f, -5f, -37.4f);
    }

    public void SetPlayerState(bool isActive)
    {
        player.gameObject.SetActive(isActive);
        PlayerPrefs.SetInt(PLAYER_KEY, playerIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
