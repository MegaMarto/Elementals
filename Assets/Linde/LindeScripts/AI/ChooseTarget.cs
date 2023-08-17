using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseTarget : MonoBehaviour
{
    private List<GameObject> Targets = new List<GameObject>();
    private float captureTimer = 10;
    [SerializeField] private GameObject target;
    private int myTeam = 5;

    void Start()
    {
        Targets.AddRange(GameObject.FindGameObjectsWithTag("Capture"));
        pickTarget();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < 15f)
        {
            //if(Vector3.Distance(transform.position, target.transform.position) < 9f)
            //{
            //    captureTimer -= Time.deltaTime;
            //} else
            //{
            //    captureTimer = 10;
            //}

            if (target.GetComponent<CaptureScr>().ReturnTeam() == myTeam)
            {
                pickTarget();
            } /*else  if (captureTimer < 0)
            {
                pickTarget();
            }*/
        } 
    }

    private void pickTarget()
    {
        List<float> TargetsDistanceWeights = new List<float>();
        float lowestWeight = 0;
        GameObject maybeTarget = null;

        foreach (GameObject tar in Targets)
        {
            if (Vector3.Distance(transform.position, tar.transform.position) > 30f)
            {
                float additionalFactor = 1;

                if (tar.GetComponent<CaptureScr>().ReturnTeam() == myTeam)
                {
                    additionalFactor *= 100;
                }
                float distance = Vector3.Distance(tar.transform.position, transform.position);
                float distanceWeight = (1 / distance) * 1000 / additionalFactor;

                if (lowestWeight < distanceWeight)
                {
                    lowestWeight = distanceWeight;
                    maybeTarget = tar;
                }
            }
            
        }

        target = maybeTarget;
        captureTimer = 10;
    }


    public GameObject ChosenTarget()
    {
        return target;
    }

}