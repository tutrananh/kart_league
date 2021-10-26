using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamMember : MonoBehaviour
{
    //private Transform character;
    public SkinnedMeshRenderer mySkin;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTeam(bool side, GameObject player)
    {
        Debug.Log(mySkin.ToString());
        //mySkin.material.color = Color.black;
        /*character = player.transform.Find("Template_Character");
        Renderer rend = character.gameObject.GetComponent<Renderer>();
        rend.material.color = Color.black; */
    }
}
