using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WindNPCAITeam3 : MonoBehaviour
{

    public Rigidbody meleObject;
    public Transform MeleSource;
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
            transform.LookAt(target);
            GetComponent<Rigidbody>().AddForce(transform.forward * atkSpeed);
        }
        if (dist <= 3f)
        {
            if (Time.time - lastShot < cooldown)
            {
                return;
            }
            lastShot = Time.time;
            Instantiate(magicCircle, MeleSource.position, MeleSource.rotation);
            Instantiate(meleObject, MeleSource.position, MeleSource.rotation);
        }
    }
}
