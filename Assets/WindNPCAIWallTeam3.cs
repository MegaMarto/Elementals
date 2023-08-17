using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WindNPCAIWallTeam3 : MonoBehaviour
{
    public Rigidbody wallObject;
    public Transform wallSource;
    public GameObject magicCircle;

    public float cooldown;
    float lastShot;

    private Transform target;
    private float dist;
    public float atkSpeed;
    public float howclose;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Team3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(target.position, transform.position);

        if (dist <= howclose)
        {
            //target = Targets.OrderBy(x => Vector3.SqrMagnitude(x.transform.position - transform.position)).First().transform;
            transform.LookAt(target);
            //GetComponent<Rigidbody>().AddForce(transform.forward * atkSpeed); //rush towards

        }
        if (dist <= 15f)
        {
            if (Time.time - lastShot < cooldown)
            {
                return;
            }
            lastShot = Time.time;
            Instantiate(magicCircle, wallSource.position, wallSource.rotation);
            Instantiate(wallObject, wallSource.position, wallSource.rotation);
        }
    }
}