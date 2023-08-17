using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class L_MovementController : MonoBehaviour
{

    [SerializeField] private float sightRange; //length of sight raycasts
    [SerializeField] private float minDistance; //Used in the function to calculate weight curve of turning away from object right in front
    [SerializeField] private float turnFactor; //How much player turns away form object
    [SerializeField] private float sideStepFactor; //How far away the new goal factor is from the player
    [SerializeField] private float wallTurnFactor; //How far away the new goal factor is from the player
    [SerializeField] float desiredSeparation; //Maximun distance flockmembers want to be apart
    [SerializeField] private Rigidbody rb; //This object
    [SerializeField] private float speed = 1; //Speed (Automatically multiplied by 10)
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject bulletPrefab;

    private GameObject target; //The destination (taken from the ChooseTarget script on flock leader)
    private GameObject leader; //The flockmember containing the ChooseTarget script
    private Vector3 direction; //Current direction of travel
    private Vector3 FlockCenter; //Center of the flock
    private bool enemyNear = false;
    private bool inCombat = false;
    private float lastWallTime;
    private float lastAttackTime;
    private float initSight; //The set sight at the start of the level
    private float cooldownTime = 2;
    private float bulletForce = 50;
    private List<GameObject> Allies = new List<GameObject>(); //List of members of the flock
    private Collider[] Enemies;





    private IEnumerator TargetMovement()
    {

        while (true)
        {
            target = leader.GetComponent<ChooseTarget>().ChosenTarget();

            //Lower the origin of the ray a fraction to improve rock and wall detection
            Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z);


            RaycastHit hit;
            Vector3 dir = Quaternion.Euler(0, 0, 0) * rb.transform.forward;
            Ray sightRay = new Ray(rayOrigin, dir * sightRange);
            Debug.DrawRay(rayOrigin, dir * sightRange/2, Color.red);

            dir = Quaternion.Euler(0, -90, 0) * rb.transform.forward;
            RaycastHit sideHitL;
            Ray sideRayL = new Ray(rayOrigin, dir * sightRange);
            Debug.DrawRay(rayOrigin, dir * sightRange, Color.yellow);

            dir = Quaternion.Euler(0, 90, 0) * rb.transform.forward;
            RaycastHit sideHitR;
            Ray sideRayR = new Ray(rayOrigin, dir * sightRange);
            Debug.DrawRay(rayOrigin, dir * sightRange, Color.yellow);

            dir = Quaternion.Euler(0, -45, 0) * rb.transform.forward;
            RaycastHit angleHitL;
            Ray angleRayL = new Ray(rayOrigin, dir * sightRange);
            Debug.DrawRay(rayOrigin, dir * sightRange, Color.yellow);

            dir = Quaternion.Euler(0, 45, 0) * rb.transform.forward;
            RaycastHit angleHitR;
            Ray angleRayR = new Ray(rayOrigin, dir * sightRange);
            Debug.DrawRay(rayOrigin, dir * sightRange, Color.yellow);





            if (Physics.Raycast(sightRay, out hit, sightRange / 2, ~LayerMask.GetMask("EarthTeam", "PointArea")))
            {
                //If the front and one of the angle rays are hit, rotate away from that object
                if (Physics.Raycast(angleRayR, out angleHitR, sightRange / 2, ~LayerMask.GetMask("EarthTeam", "PointArea")))
                {
                    //Turn 90 degrees Left
                    transform.LookAt(Quaternion.Euler(0, 90, 0) * rb.transform.forward * 10);
                    yield return new WaitForSeconds(10 * Time.deltaTime);

                } else if (Physics.Raycast(angleRayL, out angleHitL, sightRange / 2, ~LayerMask.GetMask("EarthTeam", "PointArea")))
                {
                    //Turn 90 degrees Right
                    transform.LookAt(Quaternion.Euler(0, -90, 0) * rb.transform.forward);
                    yield return new WaitForSeconds(10 * Time.deltaTime);
                } else
                {
                    //If you're still stuck, turn around a bit in any direction
                    LookToTarget(-transform.forward, 30);
                }
            }

            if (Physics.Raycast(angleRayR, out angleHitR, sightRange, ~LayerMask.GetMask("EarthTeam", "PointArea")))
            {
                //turn Left sharply
                rb.AddTorque(new Vector3(0, -4f, 0));

            } else if (Physics.Raycast(angleRayL, out angleHitL, sightRange, ~LayerMask.GetMask("EarthTeam", "PointArea")))
            {
                //turn Right sharply
                rb.AddTorque(new Vector3(0, 4f, 0));

            } else if (Physics.Raycast(sideRayR, out sideHitR, sightRange, ~LayerMask.GetMask("EarthTeam", "PointArea")))
            {
                //turn Left
                rb.AddTorque(new Vector3(0, -1f, 0));
                //LookToTarget(Quaternion.Euler(0, -90, 0) * rb.transform.forward, 15);
                //Not sure which of these two solutions is better, would require further testing
            } else if (Physics.Raycast(sideRayL, out sideHitL, sightRange, ~LayerMask.GetMask("EarthTeam", "PointArea")))
            {
                //turn Right
                rb.AddTorque(new Vector3(0, 1f, 0));
                //LookToTarget(Quaternion.Euler(0, 90, 0) * rb.transform.forward, 15);
                //Not sure which of these two solutions is better, would require further testing
            }
            else //If no obstacles around, move directly towards the target
            {
                LookToTarget(target.transform.position, 30);
            }
            //Run once every frame
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    private IEnumerator CombatMovement()
    {
        float nearestEnemyDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(Collider e in Enemies)
        {
            float enemyDistance = Vector3.Distance(transform.position, e.transform.position);
            if(nearestEnemyDistance > enemyDistance)
            {
                nearestEnemyDistance = enemyDistance;
                nearestEnemy = e.gameObject;
            }
        }

        transform.LookAt(nearestEnemy.transform.position);
        Vector3 wallSpawnPos = transform.position + transform.forward * 10;
        SpawnWall(wallSpawnPos);

        LaunchBullet();

        yield return new WaitForSeconds(Time.deltaTime);
    }

    private void Start()
    {
        //Add all players with tag EarthPlayer to list of FlockMembers
        Allies.AddRange(GameObject.FindGameObjectsWithTag("Team6"));

        for (int i = Allies.Count - 1; i > -1; i--) //Go through Allies to find the Leader based on the ChooseTarget script
        {
            if (Allies[i].GetComponent<ChooseTarget>() != null)
            {
                leader = Allies[i]; //Assign leader as object with ChooseTarget script
            }
        }

        for (int i = Allies.Count - 1; i > -1; i--) //Find self in list of Allies and remove
        {
            if (Allies[i] == gameObject)
            {
                Allies.Remove(Allies[i]);
            }
        }

        //Set initial sight so it can be reverted later
        initSight = sightRange;

        //Initiate wall timer
        lastWallTime = Time.time;

        //Start the targetMovement iEnumerator
        StartCoroutine(TargetMovement());

    }

    private void Update()
    {
        //Get the current target from the leader
        target = leader.GetComponent<ChooseTarget>().ChosenTarget();

        
        //Find the center of the flock
        FlockCenter = (Allies[0].transform.position + Allies[1].transform.position) / 2;

        if (Vector3.Distance(transform.position, FlockCenter) > desiredSeparation) //Check if self is too far from flock, if so move closer
        {
            LookToTarget(FlockCenter, 20);
        }

        //Slow down and make sight range smaller for more precise movement when entering a capture point
        if(Vector3.Distance(transform.position, target.transform.position) < 15f)
        {
            speed = 0.8f;
            sightRange = 3;
        } else
        {
            speed = 2;
            sightRange = initSight;
        }

        
    
        //Normalize the vector to make sure it is consistent throughtout
        direction = Vector3.Normalize(transform.forward) * 10;
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode.Impulse);


        
        Enemies = Physics.OverlapSphere(transform.position, 10f, ~LayerMask.GetMask("Water", "EarthTeam"));
        foreach(Collider c in Enemies)
        {
            if (c.gameObject.tag != "Capture" || c.gameObject.tag != "CapturePoint")
            {
                enemyNear = true;
                continue;
            } else
            {
                enemyNear = false;
            }
        }
        

        if (enemyNear)
        {
            if (!inCombat)
            {
                inCombat = true;
                StopCoroutine(TargetMovement());
                StartCoroutine(CombatMovement());
            }
        } else
        {
            StopCoroutine(CombatMovement());
            StartCoroutine(TargetMovement());
            inCombat = false;
        }
        
    }



    private void LookToTarget(Vector3 goal, float rotSpeed)
    {
        Vector3 dir = goal - transform.position;
        dir.y = 0; // keep the direction horizontal
        Quaternion rot = Quaternion.LookRotation(dir);
        // gradual rotation towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotSpeed * Time.deltaTime);
    }

    private void SpawnWall(Vector3 wallPosition)
    {
        if (Time.time - lastWallTime > cooldownTime)
        {
            wallPosition.y += 3;
            GameObject wall = Instantiate(wallPrefab, wallPosition, transform.rotation);
        }

        lastWallTime = Time.time;
        
    }

    private void LaunchBullet()
    {
        if (Time.time - lastAttackTime > cooldownTime)
        {
            Vector3 spawnPos = transform.position;
            Vector3 bulletOrigin = transform.position + transform.forward;
            spawnPos = bulletOrigin;

            Rigidbody bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity).GetComponent<Rigidbody>();

            float randomFactor = Random.Range(-10f, 10f);

            bullet.AddForce((transform.forward + transform.up) * (bulletForce + randomFactor), ForceMode.Impulse);
            lastAttackTime = Time.time;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
