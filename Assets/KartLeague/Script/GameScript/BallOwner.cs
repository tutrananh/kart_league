using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class BallOwner : MonoBehaviourPun
{
    private Rigidbody rigidbody;
    private float kickForce = 10;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 direction = (other.transform.position - transform.position).normalized;
            if (PhotonNetwork.IsMasterClient)
            {
                rigidbody.AddForce(-direction * kickForce * 0.5f, ForceMode.Impulse);
            }
            else
            {
                Debug.Log("aaa");
                rigidbody.AddForce(-direction * kickForce * 1.5f, ForceMode.Impulse);
            }
        }
    }

}
