using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class WindNPCAITeam4 : MonoBehaviour
{

    public Rigidbody meleObject;
    public Transform MeleSource;

    public float cooldown;
    float lastShot;

    private Transform target;

    private float dist;



    public float atkSpeed;
    public float howclose;



    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Team4").transform;
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
            Instantiate(meleObject, MeleSource.position, MeleSource.rotation);
        }
    }
}
