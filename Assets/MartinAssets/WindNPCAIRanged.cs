using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WindNPCAIRanged : MonoBehaviour
{
    //There is a script for each team because we are supposed to have our elements counter each other, wind has higher cooldowns against fire and lower cooldowns against death

    public Rigidbody rangedObject;
    public Transform rangedSource;
    public GameObject magicCircle;

    public float cooldown;
    float lastShot;

    private int team;
    float timer;
    public Transform camTransform;
    //
    private Transform target;
    private float dist;
    public float atkSpeed;
    public float howclose;
    //
    NavMeshAgent navMeshAgent;
    public float timeForNewPath;
    bool inCoRoutine;
    //private List<GameObject> Targets = new List<GameObject>();
    void Start()
    {

        //Targets.AddRange(GameObject.FindGameObjectsWithTag("Team1"));
        //Targets.AddRange(GameObject.FindGameObjectsWithTag("Team2"));
        //Targets.AddRange(GameObject.FindGameObjectsWithTag("Team3"));
        //Targets.AddRange(GameObject.FindGameObjectsWithTag("Team4"));
        //Targets.AddRange(GameObject.FindGameObjectsWithTag("Team6"));

        timer = 1;
        navMeshAgent = GetComponent<NavMeshAgent>();
        //
        target = GameObject.FindGameObjectWithTag("Team1").transform;
        //
    }

    void Update()
    {
        //
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= howclose)
        {
        //target = Targets.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - transform.position)).First().transform;
        transform.LookAt(target);
            GetComponent<Rigidbody>().AddForce(transform.forward * atkSpeed); //rush towards

        }
        if (dist <= 15f)
        {
            if(Time.time-lastShot<cooldown)
            {
                return;
            }
            lastShot = Time.time;
            Instantiate(magicCircle, rangedSource.position, rangedSource.rotation);
            Instantiate(rangedObject, rangedSource.position, rangedSource.rotation);
        }

        //

        if (!inCoRoutine)
            StartCoroutine(DoSomething());
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        inCoRoutine = false;
    }

    void GetNewPath()
    {
        navMeshAgent.SetDestination(getNewRandomPosition());
    }

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
