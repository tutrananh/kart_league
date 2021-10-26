using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Kart : MonoBehaviourPun
{
    [Range(-20, 20)]
    [SerializeField]
    private float speed = 15;
    [Range(0, 2000)]
    [SerializeField]
    private float rotationSpeed = 150;

    public float forwardAccel = 8f, reverseAccel = 4f, gravityForce = 10f;
    private float vertical, horizontal;
    private Rigidbody rigidbody;

    private bool grounded;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;
    public Transform whellFrontLeft, whellFrontRight, whellRearLeft, whellRearRight;

    Vector3 networkPosition;
    Quaternion networkRotation;
    // Start is called before the first frame update
    private void Awake()
    {
        if (photonView.IsMine)
        {
            rigidbody = GetComponent<Rigidbody>();
        }

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (Input.GetMouseButtonDown(2) && grounded)
            {
                rigidbody.AddForce(Vector3.up * 5000, ForceMode.Impulse);
            }

        }
    }
    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            LocalFixedUpdate();
        }

    }
    private void LocalFixedUpdate()
    {

        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
        }
        horizontal = Input.GetAxis("Horizontal");
        Vector3 angle = new Vector3(0, 0, 0);
        if (Input.GetAxis("Vertical") > 0)
        {
            vertical = Input.GetAxis("Vertical") * forwardAccel * speed * 100f;
            angle = horizontal * rotationSpeed * Vector3.up * 10f;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            vertical = Input.GetAxis("Vertical") * reverseAccel * speed * 100f;
            angle = horizontal * rotationSpeed * Vector3.up * -10f;
        }
        else vertical = 0;

        if (grounded)
        {
            rigidbody.AddForce(transform.forward * vertical);
            rigidbody.AddTorque(angle);

        }
        else
        {
            rigidbody.AddForce(Vector3.up * -gravityForce * 100f);

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                Vector3 rotateBack = new Vector3(0, 0, 0);
                rotateBack.Set(-transform.localEulerAngles.x, 0, -transform.localEulerAngles.z);
                transform.localEulerAngles += rotateBack;
            }
        }

        whellFrontLeft.localRotation = Quaternion.Euler(whellFrontLeft.localPosition.x + 20, horizontal * 25 - 180, whellFrontLeft.localPosition.z);
        whellFrontRight.localRotation = Quaternion.Euler(whellFrontRight.localPosition.x + 20, horizontal * 25, whellFrontRight.localPosition.z);
        whellRearLeft.localRotation = Quaternion.Euler(whellRearLeft.localPosition.x + 20, whellRearLeft.localPosition.y, whellRearLeft.localPosition.z);
        whellRearRight.localRotation = Quaternion.Euler(whellRearRight.localPosition.x + 20, whellRearRight.localPosition.y, whellRearRight.localPosition.z);
    }
}
