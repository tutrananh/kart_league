using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class OnCollisionBall : MonoBehaviour
{
    public GameManager gameManager;

    private Rigidbody rigidbody;
    private float kickForce = 10;
    private Vector3 defaultBallPosition;
    public Transform ballPosition;
    public void Awake()
    {
        defaultBallPosition = new Vector3(-8.62f, 2.13f, 0.48f);
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (transform.position.y > 2.5)
        {
            rigidbody.AddForce(Vector3.up * -2f);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                kickForce = 17;

            }
            Vector3 direction = (other.transform.position - transform.position).normalized;
            rigidbody.AddForce(-direction * kickForce * 0.5f, ForceMode.Impulse);
        }
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.transform.CompareTag("Goal"))
            {
                bool side;
                if (other.gameObject.name.Equals("BlueGoal"))
                {
                    side = true;
                }
                else
                {
                    side = false;
                }
                rigidbody.AddForce(Vector3.zero);
                ballPosition.position = defaultBallPosition;
                gameManager.UpdateGoal(side);
            }
        }
    }

}
