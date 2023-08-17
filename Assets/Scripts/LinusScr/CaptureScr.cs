using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureScr : MonoBehaviour
{
    protected float timer;
    protected float captureTimer;
    protected bool captured;
    protected int team = 10;
    private int currentTeam = 10;
    private Material newMaterial;
    // Start is called before the first frame update
    void Start()
    {
        captured = false;
        timer = 10;
        captureTimer = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if(captured == true)
        { //logic
            currentTeam = team;
            transform.parent.gameObject.GetComponent<Renderer>().material = newMaterial;
            captured = false;
        }

        if(currentTeam != 10)
        {

            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                CanvasScoreScr.instance.GivePoints(currentTeam);
                timer = 10;
            }

        } else
        {
            timer = 10;

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        captureTimer = 10;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Team1")) { team = 0; }
        if (collision.CompareTag("Team2")) { team = 1; }
        if (collision.CompareTag("Team3")) { team = 2; }
        if (collision.CompareTag("Team4")) { team = 3; }
        if (collision.CompareTag("Team5")) { team = 4; }
        if (collision.CompareTag("Team6")) { team = 5; }


        captureTimer -= Time.deltaTime;

        if (captureTimer <= 0)
        {
            newMaterial = collision.GetComponent<Renderer>().material;

            captureTimer = 10;
            captured = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        captureTimer = 10;
    }

    public int ReturnTeam()
    {
        return currentTeam;
    }
}
