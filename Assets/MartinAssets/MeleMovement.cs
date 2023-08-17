using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleMovement : MonoBehaviour
{


    public GameObject explosion;
    public Transform meleLocation;
    private float speed = 10;

    void Start()
    {

    }

    //making the mele slash move forward

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    

    private void OnTriggerEnter(Collider other)
    {
        
            Instantiate(explosion, meleLocation.position, meleLocation.rotation);
    }




}


