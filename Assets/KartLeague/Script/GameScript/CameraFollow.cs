using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CameraFollow : MonoBehaviour
{
    public PhotonView playerPhotonView;
    public Transform car;
    //private Transform car;
    private Vector3 carCameraVector3;
    // Start is called before the first frame update
    void Start()
    {
        if (playerPhotonView.IsMine)
        {
            //carCameraVector3 = car.transform.position - transform.position;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
/*        transform.position = car.transform.position - car.transform.rotation * carCameraVector3;*/
        transform.LookAt(car);
    }
}
