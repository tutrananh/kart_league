using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void FixedUpdate()
    {
        player.transform.rotation = Quaternion.Euler(player.transform.position.x, player.transform.position.y * 25 - 180, player.transform.position.z);
        if (player.transform.position.y < -30 )
        {
           Physics.gravity = new Vector3(0,10,0);

        }
        if(player.transform.position.y > 25)
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }


    }
}
