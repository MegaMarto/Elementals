using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementScript : MonoBehaviour
{

    [SerializeField] private float sightRange; //length of sight raycasts
    [SerializeField] private float minDistance; //Used in the function to calculate weight curve of turning away from object right in front
    [SerializeField] private float turnFactor; //How much player turns away form object
    [SerializeField] private float sideStepFactor; //How far away the new goal factor is from the player
    [SerializeField] private float wallTurnFactor; //How far away the new goal factor is from the player
    [SerializeField] float desiredSeparation; //Maximun distance flockmembers want to be apart
    [SerializeField] private Rigidbody rb; //This object
    [SerializeField] private float speed = 1; //Speed (Automatically multiplied by 10)

    private GameObject target; //The destination (taken from the ChooseTarget script on flock leader)
    private GameObject leader; //The flockmember containing the ChooseTarget script
    private Vector3 direction; //Current direction of travel
    private Vector3 destination; //Current destination
    private Vector3 goal; //Goal vector that brings the player closer to the destination
    private Vector3 FlockCenter; //Center of the flock
    private List<GameObject> Allies = new List<GameObject>(); //List of members of the flock



    //private IEnumerator LoopForever()
    //{
    //    while (true)
    //    {
    //        RaycastHit hit;
    //        Vector3 dir = Quaternion.Euler(0, 0, 0) * transform.forward;
    //        Ray sightRay = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.red);

    //        dir = Quaternion.Euler(0, -90, 0) * transform.forward;
    //        RaycastHit sideHitL;
    //        Ray sideRayL = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

    //        dir = Quaternion.Euler(0, 90, 0) * transform.forward;
    //        RaycastHit sideHitR;
    //        Ray sideRayR = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

    //        dir = Quaternion.Euler(0, -45, 0) * transform.forward;
    //        RaycastHit angleHitL;
    //        Ray angleRayL = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

    //        dir = Quaternion.Euler(0, 45, 0) * transform.forward;
    //        RaycastHit angleHitR;
    //        Ray angleRayR = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

    //        dir = -transform.up + transform.forward;
    //        RaycastHit angleDownHit;
    //        Ray angleDownRay = new Ray(transform.position, dir * sightRange);
    //        Debug.DrawRay(transform.position, dir * sightRange, Color.blue);


    //        if (Physics.Raycast(angleRayR, out angleHitR, sightRange))
    //        {
    //            //turn Left

    //            //while forward ray hits
    //            //keep rotating until it doesn't
    //            while (Physics.Raycast(sideRayR, out sideHitR, sightRange))
    //            {
                    
                    
    //                yield return new WaitForSeconds(0.5f);
    //            }
    //        }

    //        yield return new WaitForSeconds(Time.deltaTime);
    //    }


    //}

    void Start()
    {
        //Add all players with tag EarthPlayer to list of FlockMembers
        Allies.AddRange(GameObject.FindGameObjectsWithTag("EarthPlayer"));

        for (int i = Allies.Count - 1; i > -1; i--) //Go through Allies to find the Leader based on the ChooseTarget script
        {
            if (Allies[i].GetComponent<ChooseTarget>() != null)
            {
                leader = Allies[i]; //Assign leader as object with ChooseTarget script
            }
        }

        for (int i = Allies.Count - 1; i > - 1; i--) //Find self in list of Allies and remove
        {
            if(Allies[i] == gameObject)
            {
                Allies.Remove(Allies[i]);
            }
        }

        //print(Allies.Count);

    }

    

    private void FixedUpdate()
    {
        //Define the current target based on the leader
        target = leader.GetComponent<ChooseTarget>().ChosenTarget();

        //Set that as the destination
        destination = target.transform.position;

        //Find the center of the flock
        FlockCenter = GetMidVector(Allies[0].transform.position, Allies[1].transform.position) / 2;

        //Find direct direction of travel based on Raycasts
        GetGoal();

        if(Vector3.Distance(transform.position, FlockCenter) > desiredSeparation) //Check if self is too far from flock, if so move closer
        {
            direction = GetMidVector(Vector3.Normalize(goal - transform.position) * 10, GetFlockPosition());
        } else
        {
            //direction = Vector3.Normalize(goal - transform.position) * 10;
            direction = Vector3.Normalize(goal - transform.position) * 10;
        }

        direction = Vector3.Normalize(direction) * 10; //Make sure direction vector is 10 units long


        rb.AddForce(direction * speed *  Time.deltaTime, ForceMode.Impulse); //Add force to self
        transform.LookAt(goal); //Look to where we're going

    }


    private Vector3 GetFlockPosition()
    {
        Vector3 FlockPosition;
 
        FlockPosition = FlockCenter - transform.position; //Find the direction to the desired position in the flock



        return FlockPosition;
    }




    private Vector3 GetGoal()
    {

        RaycastHit hit;
        Vector3 dir = Quaternion.Euler(0, 0, 0) * transform.forward;
        Ray sightRay = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.red);

        dir = Quaternion.Euler(0, -90, 0) * transform.forward;
        RaycastHit sideHitL;
        Ray sideRayL = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

        dir = Quaternion.Euler(0, 90, 0) * transform.forward;
        RaycastHit sideHitR;
        Ray sideRayR = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

        dir = Quaternion.Euler(0, -45, 0) * transform.forward;
        RaycastHit angleHitL;
        Ray angleRayL = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

        dir = Quaternion.Euler(0, 45, 0) * transform.forward;
        RaycastHit angleHitR;
        Ray angleRayR = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.yellow);

        dir = -transform.up + transform.forward; 
        RaycastHit angleDownHit;
        Ray angleDownRay = new Ray(transform.position, dir * sightRange);
        Debug.DrawRay(transform.position, dir * sightRange, Color.blue);



        if (Physics.Raycast(angleRayL, out angleHitL, sightRange) && Physics.Raycast(angleRayR, out angleHitR, sightRange) && Physics.Raycast(sightRay, out hit, sightRange) || !Physics.Raycast(angleDownRay, out angleDownHit, sightRange))
        {
            if (Physics.Raycast(sideRayL, out sideHitL, sightRange))
            {
                goal = Quaternion.Euler(0, 135, 0) * goal * wallTurnFactor;
            } else if (Physics.Raycast(sideRayL, out sideHitL, sightRange))
            {
                goal = Quaternion.Euler(0, -135, 0) * goal * wallTurnFactor;
            } else
            {
                goal = Quaternion.Euler(0, 135, 0) * goal * wallTurnFactor;
            }
        }
        else if (Physics.Raycast(sightRay, out hit, sightRange))
        {
            Vector3 newPointA = Vector3.Reflect(sightRay.direction, hit.normal);
            Vector3 newPointB = destination;

            float dist = Vector3.Distance(hit.point, transform.position);

            float factPart1 = ((sightRange - minDistance) - dist) / (sightRange - minDistance);
            float factor = Mathf.Abs(factPart1) * turnFactor;
            //print(factor);
            goal = GetMidVector(newPointA * factor, newPointB);

        }
        else if (Physics.Raycast(sideRayL, out sideHitL, sightRange) && Physics.Raycast(sideRayR, out sideHitR, sightRange))
        {
            goal = destination;
        }
        else if (Physics.Raycast(angleRayL, out angleHitL, sightRange))
        {
            Vector3 opositeSide = Quaternion.Euler(0, 45, 0) * transform.forward;
            goal = GetMidVector(opositeSide * sideStepFactor, destination);

        }
        else if (Physics.Raycast(angleRayR, out angleHitR, sightRange))
        {
            Vector3 opositeSide = Quaternion.Euler(0, -45, 0) * transform.forward;
            goal = GetMidVector(opositeSide * sideStepFactor, destination);
        }
        else if (Physics.Raycast(sideRayL, out sideHitL, sightRange) || Physics.Raycast(sideRayR, out sideHitR, sightRange))
        {
            
        }
        else
        {
            goal = destination;
        }


        return goal;
    }

    private Vector3 GetMidVector(Vector3 first, Vector3 second)
    {
        return first + second;
    }




}
