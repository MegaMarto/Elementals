using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovementController : MonoBehaviour
{

    public GameObject player;
    public float speed;
    public float jumpForce;
    public float playerHeight;

    private Rigidbody rb;
    private float jumpCalibration = 300;
    private bool isGrounded;
    private float activeSpeed;
    

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }


    void Update()
    {
        isGroundedCheck();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpCalibration * jumpForce);
            }
        }



        float LRMovement = Input.GetAxis("Horizontal");
        float ForwardMovement = Input.GetAxis("Vertical");

        float hMove = LRMovement * activeSpeed;
        float vMove = ForwardMovement * activeSpeed;

        rb.AddRelativeForce(hMove, 0, vMove);

        

    }

    public void isGroundedCheck()
    {
        Ray rayDown = new Ray(rb.position, transform.up * -1);
        RaycastHit hitData;

        if(Physics.Raycast(rayDown, out hitData))
        {
            if (Vector3.Distance(hitData.point, rb.transform.position) > playerHeight)
            {
                isGrounded = false;
                activeSpeed = speed * 0.1f;
            } else
            {
                isGrounded = true;
                activeSpeed = speed;
            }
        }
    }
}
