using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FireObjective : StateMachineBehaviour
{
    private GameObject[] capturePoints;
    private GameObject closestCapturePoint;
    private bool capturePointContact;
    private NavMeshAgent agent;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        closestCapturePoint = null;
        capturePointContact = false;

        closestCapturePoint = GetClosestCapturePoint(animator);
        agent.destination = closestCapturePoint.transform.position;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SeeEnemy(animator))
        {
            animator.SetInteger("FireState", 1);
        }
    }

    private bool SeeEnemy(Animator animator)
    {
        return false;
    }

    private GameObject GetClosestCapturePoint(Animator animator)
    {
        capturePoints = GameObject.FindGameObjectsWithTag("CapturePoint"); // find objects tagged CapturePoint        
        Debug.Log(capturePoints.Length);

        float closestDistance = 10000;
        GameObject trans = null;

        foreach (GameObject go in capturePoints) // for every capturepoint object, calculate each distance from player and choose the closest one
        {
            float currentDistance;
            currentDistance = Vector3.Distance(animator.transform.position, go.transform.position);
            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                trans = go;
            }

        }
        Debug.Log(trans);
        //agent.destination = trans.position; // this tells player's NavMeshAgent to move to trans (closest capturepoint)
        return trans; // return the value of trans
    }
}
