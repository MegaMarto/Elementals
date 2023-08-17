using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private int team;
    float timer;
    public Transform camTransform;
    public float charSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

            transform.position = transform.position + Vector3.left * Time.deltaTime * charSpeed;
        
        }

        else if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Vector3.right * Time.deltaTime * charSpeed;
         
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + Vector3.forward * Time.deltaTime * charSpeed;
          
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + Vector3.back * Time.deltaTime * charSpeed;
           
        }
        //FollowCam();

    }
    //private void FollowCam()
    //{
    //    camTransform.position = transform.position;

    //    //v3 Campos
    //    Vector3 newCamPos;
    //    //Making camPos same as players
    //    newCamPos = new Vector3(transform.position.x, transform.position.y + 5, -10);
    ////    camTransform.position = newCamPos;

    //}
    private void OnTriggerStay(Collider collision)
    {
        timer -= Time.deltaTime;
        if (collision.CompareTag("Capture") && timer <= 0)
        {
            if (collision.CompareTag("Team1")) { team = 0; }
            if (collision.CompareTag("Team2")) { team = 1; }
            if (collision.CompareTag("Team3")) { team = 2; }
            if (collision.CompareTag("Team4")) { team = 3; }
            if (collision.CompareTag("Team5")) { team = 4; }
            if (collision.CompareTag("Team6")) { team = 5; }
            CanvasScoreScr.instance.GivePoints(team);
            timer = 10;
        }
    }
}
